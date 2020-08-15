using Classes.Commands;
using Classes.Logger;
using Classes.RetryPolicy;
using System;

namespace Classes.Handlers
{
    public class GenericHandler : IHandler<GenericCommand>
    {
        private ILogger _logger;
        private IRetryPolicy _retryPolicy;
        public GenericHandler(ILogger logger, IRetryPolicy retryPolicy)
        {
            _logger = logger;
            _retryPolicy = retryPolicy;
        }
        public void Handle(GenericCommand command)
        {
            _retryPolicy.Execute(() => {
                _logger.Log(command.BuildMessage());
                throw new Exception("Testing Simple Policy");
            });            
        }
    }
}
