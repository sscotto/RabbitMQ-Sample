
using System;

namespace Classes.Commands
{
    public class NullCommand : GenericCommand
    {
        public NullCommand(int id, DateTime date, string message): base(id, date, message) { }
        public override string BuildMessage()
        {
            return "Command Not Found";
        }
    }
}
