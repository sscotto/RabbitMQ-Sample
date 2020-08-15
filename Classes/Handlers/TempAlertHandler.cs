using Classes.Commands;
using Classes.Logger;
using Classes.RetryPolicy;

namespace Classes.Handlers
{
    class TempAlertHandler : IHandler<TempAlertCommand>
    {
        private ILogger _logger;
        private IRetryPolicy _retryPolicy;
        public TempAlertHandler(ILogger logger, IRetryPolicy retryPolicy)
        {
            _logger = logger;
            _retryPolicy = retryPolicy;
        }
        public void Handle(TempAlertCommand command)
        {
            _retryPolicy.Execute(() => {
                _logger.Log(command.BuildMessage());
                throw new System.Exception("Testing Exponential Retry Policy");
            });
        }
    }
}
