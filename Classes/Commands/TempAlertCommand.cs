using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Commands
{
    public class TempAlertCommand : GenericCommand
    {
        public double _temp { get; set; }
        public double _maxTemp { get; set; }

        public TempAlertCommand(int id, DateTime date, string message, double temp, double maxTemp) : base(id, date, message)
        {
            _temp = temp;
            _maxTemp = maxTemp;
        }
        public override string BuildMessage()
        {
            string baseMessage = base.BuildMessage();
            return baseMessage + $", TempReached: {_temp}, MaxTemp: {_maxTemp}";
        }
    }
}
