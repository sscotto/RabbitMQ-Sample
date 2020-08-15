using Classes.Commands;
using Classes.Logger;
using System;

namespace Classes.Handlers
{
    public class GenericHandler : IHandler<GenericCommand>
    {
        private ILogger _logger;
        public GenericHandler(ILogger logger)
        {
            _logger = logger;
        }
        public void Handle(GenericCommand command)
        {
            _logger.Log(command.BuildMessage());
        }
    }
}
