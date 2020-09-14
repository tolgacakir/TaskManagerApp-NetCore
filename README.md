# TaskManagerApp-NetCore
The task manager sample application with Asp.NetCore MVC

## The Solution Architecture
- **Core:**
The Core layer is a portable class library for every project. It doesn't depend on any project in this solution but it can depend on some nuget libraries. For exp.: FluentValidation. It includes technology-specific project/entity-independent codes like Logger, Validator, ORM interfaces and generic implementations and utilities. For exp.: FileManager, Entity Framework Core GenericRepository, FileLogger...

- **Entities:**
The Entities layer is a modelling the DB objects. It includes entity objects that usable in all of the layers like **User**.

- **DataAccessLayer:**
The Data Access Layer is layer that connecting to the data. It can includes different data access technologies for exp.: EF, Nhibernate, Ado.Net etc.
On the other hand, it includes technology-independent repository interfaces. For exp.: IUserDal.
The client (It is BLL in this solution) that using DAL uses only interfaces. However, the client can choose which concrete class (for exp.: EfUserDal or AdoNetUserDal) to implement by constructor injection.

- **BusinessLogicLayer:**
This layer acts as a bridge between the UI and DataAccess layers. It includes service/manager classes, business codes etc.

- **WebUi:**
This layer is an Asp.Net Core Mvc project. It includes Models, Views, Controllers and the other ready-made classes from Asp.Net Core Mvc

## How to Develop

### Example DB Backup
You can access database backup from here: https://drive.google.com/file/d/1-jMfdVZM7dU3qZ-CI2f5tiNkwuoB1u3o/view?usp=sharing
There are 3 users in User.dbo; username:password
- tolga:00000000
- FirstUser:11111111
- test_user:22222222

#### Creating New User
There is no user creating function on WebUi. You can edit and execute this test method: TaskManagerApp.BusinessLogicLayer.Tests.UserManagerTests.Should_Create_User()

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
- **Create Mapping, Edit DbContext and Apply Mapping**
Creating Map:
```C#
namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework.Mappings
{
    public class MyEntityMap : IEntityTypeConfiguration<MyEntity>
    {
        public void Configure(EntityTypeBuilder<MyEntity> builder)
        {
            builder.ToTable(@"MyEntities", "dbo");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("Id");
            builder.Property(m => m.X).HasColumnName("X");
        }
    }
}
```
Adding DbSet to DbContext:
```C#
namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
    public class TaskManagerDbContext : DbContext
    {
        //...
        public DbSet<MyEntity> MyEntities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //...
            modelBuilder.ApplyConfiguration(new MyEntityMap());
        }
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

### Adding New User Interface

- Create new user interface;
For exp.: Console, Winform, Wpf etc.

- Use the service layer objects (in BLL);
For exp.: IUserService

- Configure the dependency injection;
For ILogger, IUserService etc.


### For More Details
- Autofac (5.2.0), Autofac.Extras.DynamicProxy (5.0.0) and Castle.Core (4.4.1) packages are using for AOP mechanism. [For more details about interception and DI](https://autofaccn.readthedocs.io/en/latest/advanced/interceptors.html)

- FluentValidation (9.2.0) package are using for validation mechanism. [For more details about validation](https://docs.fluentvalidation.net/en/latest/start.html)
