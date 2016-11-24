using System.Linq;

namespace Tdf.CQRS.Domain.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(object id);

        IQueryable<T> Query();

        void Add(T entity);

        void Delete(T entity);
    }
}
