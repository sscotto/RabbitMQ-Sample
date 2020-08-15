using RabbitMQ.Client;
using System;
using System.Collections.Generic;

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


            string deadLetterExchange = "dead_letter_exchange";

            EventsQueue(model, deadLetterExchange);
            PositionsQueue(model, deadLetterExchange);
            Console.ReadLine();
        }

        private static void EventsQueue(IModel model, string deadLetterExchange)
        {
            string queueName = "events_queue";
            string exchangeName = "test";
            string routingKey1 = "events";
            Dictionary<string, object> arguments = new Dictionary<string, object>()
            {
               {"x-dead-letter-exchange", deadLetterExchange},
               {"x-dead-letter-routing-key", routingKey1},
            };

            DeclareAndBind(model, "deadletter_" + queueName, deadLetterExchange, routingKey1, null);
            DeclareAndBind(model, queueName, exchangeName, routingKey1, arguments);
            Console.WriteLine($"{queueName} created on exchange {exchangeName} - routing key {routingKey1}");
        }

        private static void PositionsQueue(IModel model, string deadLetterExchange)
        {
            string queueName = "positions_queue";
            string exchangeName = "test";
            string routingKey2 = "positions";
            Dictionary<string, object> arguments = new Dictionary<string, object>()
            {
               {"x-dead-letter-exchange", deadLetterExchange},
               {"x-dead-letter-routing-key", routingKey2},
            };
            DeclareAndBind(model, "deadletter_" + queueName, deadLetterExchange, routingKey2, null);
            DeclareAndBind(model, queueName, exchangeName, routingKey2, arguments);
            Console.WriteLine($"{queueName} created on exchange {exchangeName} - routing key {routingKey2}");
        }

        private static void DeclareAndBind(IModel model, string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments)
        {
            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            Console.WriteLine("Exchange {0} Created", exchangeName);

            model.QueueDeclare(queueName, true, false, false, arguments);
            Console.WriteLine("Queue {0} Created", queueName);

            model.QueueBind(queueName, exchangeName, routingKey);
            Console.WriteLine("Queue & Exchange Bounded");
        }
    }
}
