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
  public class EfTaskDal : EfEntityRepositoryBase<MyEntity,MyDbContext>, IMyEntityDal
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
      //TODO: implementation
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
        IMyEntityDal _myEntityDal;
        
        public MyEntityManager(IMyEntityDal myEntityDal)
        {
            _myEntityDal = myEntityDal;
        }
        
        List<MyEntity> GetAll()
        {
            //some code
        }
        
        void Add(MyEntity myEntity)
        {
            //some code
        }
        
        void Update(MyEntity myEntity)
        {
            //some code
        }
        
        void Delete(MyEntity myEntity)
        {
            //some code
        }
        
        List<MyEntity> GetWithMyCondition(P parameter)
        {
            //some code
        }
    }
}
```
MyEntityManager doesn't know the IMyEntityService implementation or any ORM, DB technologies. This class only works with repository (dal object) interface.
