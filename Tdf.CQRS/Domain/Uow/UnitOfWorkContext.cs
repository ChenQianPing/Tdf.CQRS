using System;
using Tdf.CQRS.Dependency;

namespace Tdf.CQRS.Domain.Uow
{
    public class UnitOfWorkContext
    {
        [ThreadStatic]
        private static IUnitOfWork _currentUnitOfWork;

        public static IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                return _currentUnitOfWork;
            }
        }

        public static IUnitOfWork StartUnitOfWork()
        {
            _currentUnitOfWork = ObjectContainer.Resolve<IUnitOfWork>();
            return _currentUnitOfWork;
        }

        public static void Commit()
        {
            if (_currentUnitOfWork == null)
                throw new InvalidOperationException("Unit of work not yet started.");

            _currentUnitOfWork.Commit();
        }

        public static void Close()
        {
            if (_currentUnitOfWork != null)
            {
                _currentUnitOfWork.Dispose();
                _currentUnitOfWork = null;
            }
        }
    }
}
