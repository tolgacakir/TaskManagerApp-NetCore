# TaskManagerApp-NetCore
The task manager sample application with Asp.NetCore MVC

### Changing the **ConnectionString**
- Open the TaskManagerApp.DataAccessLayer.Concrete.EntityFramework.TaskManagerDbContext.cs file
- Change the OnConfiguring() -> optionsBuilder.UseSqlServer(@"**myConnectionString**");

### Adding the new Entity and Its EF Core based Repositories
- **Create new entity;**
```C#
namespace TaskManagerApp.Entities.Concrete
{
  public class MyEntity : IEntity
  {
    public int X {get; set;}
  }
}
```
__
- **Create technology-independent repository interface;**
```C#
namespace TaskManagerApp.DataAccessLayer.Abstract
{
  public interface IMyEntityDal : IEntityRepository<MyEntity>
  {
  }
}
```
IEntityRepository<T> interface has include CRUD operations.
If you want to add a different operation than CRUD;
```C#
namespace TaskManagerApp.DataAccessLayer.Abstract
{
  public interface IMyEntityDal : IEntityRepository<Entity>
  {
    public int GetCount();
  }
}
```
__
- **Create technology-specific repository class;**
```C#
namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
  public class EfMyEntityDal : EfEntityRepositoryBase<MyEntity,MyDbContext>, IMyEntityDal
  {
  }
}
```
EfEntityRepositoryBase<T> has include CRUD implementations for T using EF Core.
If you added a different operation than CRUD to IMyEntityDal. You have to implement the method like this;
```C#
namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
  public class EfMyEntityDal : EfEntityRepositoryBase<MyEntity,MyDbContext>, IMyEntityDal
  {
    public int GetCount()
    {
      //...
    }
  }
}
```

### Adding the new Service/Manager Class for MyEntity on BusinessLogicLayer

- **Create an service interface;**
```C#
namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public interface IMyEntityService
    {
        List<MyEntity> GetAll();
        void Add(MyEntity myEntity);
        void Update(MyEntity myEntity);
        void Delete(MyEntity myEntity);
        List<MyEntity> GetWithMyCondition(P parameter);
        
        // And/Or custom methods...
        //... 
    }
}
```
__
- **Create an manager class as an implementation the service interface;**
```C#
namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public class MyEntityManager : IMyEntityService
    {
        private readonly IMyEntityDal _myEntityDal;
        
        public MyEntityManager(IMyEntityDal myEntityDal)
        {
            _myEntityDal = myEntityDal;
        }
        
        List<MyEntity> GetAll()
        {
            //...
        }
        
        void Add(MyEntity myEntity)
        {
            //...
        }
        
        void Update(MyEntity myEntity)
        {
            //...
        }
        
        void Delete(MyEntity myEntity)
        {
            //...
        }
        
        List<MyEntity> GetWithMyCondition(P parameter)
        {
            //...
        }
    }
}
```
MyEntityManager doesn't know the IMyEntityService implementation or any ORM, DB technologies. This class only works with repository (dal object) interface.

### Using MyEntity Service Object In Client-Side
- Registration for dependency injection;
```C#
namespace TaskManagerApp.WebUi
{
    public class Startup
    {
        //...
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMyEntityService, MyEntityManager>();
            //...
        }
        //...
    }
}
```
__
- Inject with constructor and use it;
```C#
namespace TaskManagerApp.WebUi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMyEntityService _myEntityManager;
        //...
        
        public HomeController(IMyEntityService myEntityManager, /*...*/)
        {
            _myEntityManager = myEntityManager;
            //...
        }
        
        public IActionResult Index()
        {
            try
            {
                var myEntities = _myEntityManager.GetAll();
                //...
            }
            catch(Exception)
            {
                //...
            }
        }
        //...
    }
}
```

### Adding Validation For MyEntity

- Create MyEntityValidator;
TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation -> MyEntityValidator.cs
```C#
namespace TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation
{
    public class MyEntityValidator : AbstractValidator<MyEntity>
    {
        public MyEntityValidator()
        {
            RuleFor(m => m.X).NotEqual(0).WithMessage("X can not be zero.");
        }
    }
}
```
__
- Use MyEntityValidator in MyEntityManger;
```C#
namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public class MyEntityManager : IMyEntityService
    {
        private readonly IMyEntityDal _myEntityDal;
        private readonly IValidator _validator;
        
        public MyEntityManager(IMyEntityDal myEntityDal)
        {
            _myEntityDal = myEntityDal;
            _validator = new MyEntityValidator();
        }
        
        List<MyEntity> GetAll()
        {
            //...
        }
        
        void Add(MyEntity myEntity)
        {
            ValidatorTool.Validate(_validator, myEntity);
            //...
        }
        ...
    }
}
```

### Adding CustomLogger
- Create CustomLogger; 
TaskManagerApp.Core.CrossCuttingConcerns.Logging.Loggers -> CustomLogger.cs
```C#
namespace TaskManagerApp.Core.CrossCuttingConcerns.Logging.Loggers
{
    public class CustomLogger : ILogger
    {
        public CustomLogger()
        {
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string logMessage = $"...**write log message**...";
            //...
            //write to file, database etc.
            //...
        }
    }
}

```
__
- Create CustomLoggerProvider;

```C#
namespace TaskManagerApp.Core.CrossCuttingConcerns.Logging.LoggerProviders
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        public CustomLoggerProvider()
        {
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
        }
    }
}

```
__
- Use CustomLoggerProvider;

```C#
namespace TaskManagerApp.WebUi
{
    public class Startup
    {
        //...
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new CustomLoggerProvider());
            //...
        }
        //...
    }
}
```
___
- Inject CustomLogger with constructor and use it;
```C#
namespace TaskManagerApp.WebUi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //...
        
        public HomeController(ILogger<HomeController> logger, /*...*/)
        {
            _logger = logger;
            //...
        }
        
        public IActionResult Index()
        {
            try
            {
                //...
            }
            catch(Exception)
            {
                //...
               _logger.LogError(/* **log message** */);
            }
        }
        //...
    }
}
```
This controller is agnostic about logger implementation.
