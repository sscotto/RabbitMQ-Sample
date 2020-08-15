using Classes.Commands;
using Classes.Handlers;
using Classes.Logger;
using Classes.RetryPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class HandlerFactory
    {
        private ILogger _logger;

        public HandlerFactory(ILogger logger)
        {
            _logger = logger;
        }        

        public IHandler<GenericCommand> CreateGenericHandler(IRetryPolicy retryPolicy)
        {
            return new GenericHandler(_logger, retryPolicy);
        }

        public IHandler<DoorOpenCommand> CreateDoorOpenHandler(IRetryPolicy retryPolicy)
        {
            return new DoorOpenHandler(_logger, retryPolicy);
        }

        public IHandler<TempAlertCommand> CreateTempAlarmHandler(IRetryPolicy retryPolicy)
        {
            return new TempAlertHandler(_logger, retryPolicy);
        }

        public IHandler<NullCommand> CreateNullHandler()
        {
            return new NullHandler(_logger);
        }
    }
}
