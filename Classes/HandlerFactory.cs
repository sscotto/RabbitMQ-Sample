using Classes.Commands;
using Classes.Handlers;
using Classes.Logger;
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

        public IHandler<GenericCommand> CreateGenericHandler()
        {
            return new GenericHandler(_logger);
        }

        public IHandler<DoorOpenCommand> CreateDoorOpenHandler()
        {
            return new DoorOpenHandler(_logger);
        }

        public IHandler<TempAlertCommand> CreateTempAlarmHandler()
        {
            return new TempAlertHandler(_logger);
        }

        public IHandler<NullCommand> CreateNullHandler()
        {
            return new NullHandler(_logger);
        }

        //public IHandler<ICommand> CreateCommand(string commandType)
        //{
        //    if (commandType == typeof(GenericCommand).Name)               
        //        return (IHandler<ICommand>)new GenericHandler(_logger) ;

        //    if (commandType == typeof(DoorOpenCommand).Name)
        //        return (IHandler<ICommand>)new DoorOpenHandler(_logger);

        //    if (commandType == typeof(TempAlertCommand).Name)   
        //        return (IHandler<ICommand>)new TempAlertHandler(_logger);

        //    return (IHandler<ICommand>)new NullHandler(_logger);    
        //}
    }
}
