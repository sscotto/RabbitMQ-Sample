using Newtonsoft.Json;
using System;

namespace Classes.Commands
{
    public class GenericCommand : ICommand
    {
        public int _id { get; set; }
        public DateTime _date { get; set; }
        public string _message { get; set; }

        public GenericCommand(int id, DateTime date, string message)
        {
            _id = id;
            _date = date;
            _message = message;
        }
        public virtual string BuildMessage()
        {
            return $"EventId: {_id}, EventDate: {_date.ToString()}, Message: {_message}";
        }
    }
}
