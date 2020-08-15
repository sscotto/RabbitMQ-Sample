using Classes.Commands;
using Classes.Logger;

namespace Classes.Handlers
{
    class TempAlertHandler : IHandler<TempAlertCommand>
    {
        private ILogger _logger;
        public TempAlertHandler(ILogger logger)
        {
            _logger = logger;
        }
        public void Handle(TempAlertCommand command)
        {
            _logger.Log(command.BuildMessage());
        }
    }
}
