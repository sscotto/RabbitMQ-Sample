using RabbitMQ.Client;
using System;
using System.Text;
using Classes;
using Classes.Commands;
using Newtonsoft.Json;

namespace MessagePublisher
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";

        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ - Message Publisher");
            
            var exchangeName = "test";

            var connectionFactory = new ConnectionFactory()
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            RandomsCommandsGenerator randomsCommands = new RandomsCommandsGenerator();
            int i = 0;
            while (1 == 1)
            {                                
                ICommand command = randomsCommands.CreateRandomCommand();
                Message message = new Message()
                {
                    JsonMessage = JsonConvert.SerializeObject(command),
                    Type = command.GetType().Name
                };
                

                byte[] messageBuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(message));
                model.BasicPublish(exchangeName, "events", properties, messageBuffer);
                
                i++;
                Console.WriteLine($"Message published - CommandType: {message.Type}");
                System.Threading.Thread.Sleep(1000);                
            }

        }
    }
}
