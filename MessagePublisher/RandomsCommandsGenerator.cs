using Classes.Commands;
using System;

namespace MessagePublisher
{
    public class RandomsCommandsGenerator
    {
        private int _lastId;

        public RandomsCommandsGenerator()
        {
            _lastId = 0;
        }

        public ICommand CreateRandomCommand()
        {
            Random random = new Random();
            int randomInt = random.Next(1, 4);

            ICommand createdCommand = null;
            switch (randomInt)
            {
                case 1:
                    createdCommand = new GenericCommand(_lastId, DateTime.Now, "Just a random generic command");
                    break;
                case 2:
                    createdCommand = new DoorOpenCommand(_lastId, DateTime.Now, "Just a random door open command", 100);
                    break;
                case 3:
                    createdCommand = new TempAlertCommand(_lastId, DateTime.Now, "Just a random temp alertcommand", 35, 30);
                    break;                
            }
            _lastId++;
            return createdCommand;
        }
    }
}
