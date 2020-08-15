using RabbitMQ.Client;
using System;

namespace RabbitMQ_Test
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";

        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ - Queue Creator");


            var connectionFactory = new ConnectionFactory()
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            string queueName = "events_queue";
            string exchangeName = "test";
            string routingKey1 = "events";
            DeclareAndBind(model, queueName, exchangeName, routingKey1);
            Console.WriteLine($"{queueName} created on exchange {exchangeName} - routing key {routingKey1}");

            queueName = "positions_queue";
            exchangeName = "test";
            string routingKey2 = "positions";
            DeclareAndBind(model, queueName, exchangeName, routingKey2);
            Console.WriteLine($"{queueName} created on exchange {exchangeName} - routing key {routingKey2}");

            Console.ReadLine();


        }

        private static void DeclareAndBind(IModel model, string queueName, string exchangeName, string routingKey)
        {
            model.QueueDeclare(queueName, true, false, false, null);
            Console.WriteLine("Queue {0} Created", queueName);

            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            Console.WriteLine("Exchange {0} Created", exchangeName);

            model.QueueBind(queueName, exchangeName, routingKey);
            Console.WriteLine("Queue & Exchange Bounded");
        }
    }
}
