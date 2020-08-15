namespace Classes.Handlers
{
    public interface IHandler<TCommand> where TCommand : class
    {
        void Handle(TCommand command);
    }
}
