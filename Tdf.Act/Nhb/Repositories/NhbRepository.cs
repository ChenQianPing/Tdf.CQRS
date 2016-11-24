using NHibernate;
using NHibernate.Linq;
using System.Linq;
using Tdf.CQRS.Domain.Repositories;
using Tdf.CQRS.Domain.Uow;

namespace Tdf.Act.Nhb.Repositories
{
    public class NhbRepository<T> : IRepository<T>
        where T : class
    {
        public NhbUnitOfWork UnitOfWork { get; private set; }

        public ISession Session
        {
            get
            {
                return UnitOfWork.Session;
            }
        }

        public NhbRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = (NhbUnitOfWork)unitOfWork;
        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public IQueryable<T> Query()
        {
            return Session.Query<T>();
        }

        public void Add(T entity)
        {
            Session.Save(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}

