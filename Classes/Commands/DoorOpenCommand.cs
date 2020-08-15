using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Commands
{
    public class DoorOpenCommand : GenericCommand
    {
        public int _houseId { get; set; }

        public DoorOpenCommand(int id, DateTime date, string message, int houseId): base(id, date, message)
        {
            _houseId = houseId;
        }
        public override string BuildMessage()
        {
            string baseMessage = base.BuildMessage();
            return baseMessage + $", HouseId: {_houseId}";
        }
    }
}
