namespace Retrospective.Application.API.Common
{
    using System.Data.Entity;
    using System.Linq;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private IDbSet<TEntity> DbSet
        {
            get
            {
                return this.dbContext.Set<TEntity>();
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public virtual TEntity InsertandSave(TEntity entity)
        {
            DbSet.Add(entity);
            this.dbContext.SaveChanges();
            return entity;
        }
        

        public virtual void Update(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;           
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            this.dbContext.SaveChanges();
        }
    }
}