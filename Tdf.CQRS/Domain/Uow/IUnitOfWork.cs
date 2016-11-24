using System;

namespace Tdf.CQRS.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
