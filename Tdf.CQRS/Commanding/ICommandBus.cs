namespace Tdf.CQRS.Commanding
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand cmd) where TCommand : ICommand;
    }
}
