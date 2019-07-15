namespace Retrospective.Application.API.Common
{
    using System.Linq;

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity Insert(TEntity entity);

        void Update(TEntity entity);

    }
}
