using System;

namespace Classes.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {           
            Console.WriteLine(message);
        }
    }
}
