using Tdf.CQRS.Dependency;
using Tdf.CQRS.Domain.Uow;

namespace Tdf.CQRS.Commanding
{
    public class DefaultCommandBus : ICommandBus
    {
        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            try
            {
                var unitOfWork = UnitOfWorkContext.StartUnitOfWork();

                var executor = ObjectContainer.Resolve<ICommandExecutor<TCommand>>();
                executor.Execute(cmd);

                UnitOfWorkContext.Commit();
            }
            finally
            {
                UnitOfWorkContext.Close();
            }
        }
    }
}
