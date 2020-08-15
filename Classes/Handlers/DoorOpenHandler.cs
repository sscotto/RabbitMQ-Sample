using Classes.Commands;
using Classes.Logger;
using Classes.RetryPolicy;

namespace Classes.Handlers
{
    class DoorOpenHandler : IHandler<DoorOpenCommand>
    {
        private ILogger _logger;
        private IRetryPolicy _retryPolicy;
        public DoorOpenHandler(ILogger logger, IRetryPolicy retryPolicy)
        {
            _logger = logger;
            _retryPolicy = retryPolicy;
        }
        public void Handle(DoorOpenCommand command)
        {
            _retryPolicy.Execute(() => { _logger.Log(command.BuildMessage()); });
        }
    }
}
