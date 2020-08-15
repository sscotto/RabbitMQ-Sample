using Classes.Commands;
using Classes.Logger;

namespace Classes.Handlers
{
    public class NullHandler: IHandler<NullCommand>
    {
        private ILogger _logger;
        public NullHandler(ILogger logger)
        {
            _logger = logger;
        }
        public void Handle(NullCommand command)
        {
            _logger.Log(command.BuildMessage());
        }
    }
}
