using Classes.Commands;
using Classes.Logger;

namespace Classes.Handlers
{
    class DoorOpenHandler : IHandler<DoorOpenCommand>
    {
        private ILogger _logger;
        public DoorOpenHandler(ILogger logger)
        {
            _logger = logger;
        }
        public void Handle(DoorOpenCommand command)
        {
            _logger.Log(command.BuildMessage());
        }
    }
}
