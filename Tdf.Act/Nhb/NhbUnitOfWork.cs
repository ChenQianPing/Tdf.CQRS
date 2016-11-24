using NHibernate;
using Tdf.CQRS.Domain.Uow;

namespace Tdf.Act.Nhb
{
    public class NhbUnitOfWork : IUnitOfWork
    {
        public ISession Session { get; private set; }

        public NhbUnitOfWork(ISession session)
        {
            Session = session;
            Session.BeginTransaction();
        }

        public void Commit()
        {
            Session.Transaction.Commit();
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
