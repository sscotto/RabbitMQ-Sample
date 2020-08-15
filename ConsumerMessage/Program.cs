using Classes;
using Classes.Commands;
using Classes.Exceptions;
using Classes.Handlers;
using Classes.Logger;
using Classes.RetryPolicy;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace ConsumerMessage
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";

        private static ILogger _logger;
        private static ILogger Logger
        {
            get
            {
                return _logger ?? (_logger = new ConsoleLogger());
            }
        }

        private static HandlerFactory _handlerFactory;
        private static HandlerFactory HandlerFactory
        {
            get
            {
                return _handlerFactory ?? (_handlerFactory = new HandlerFactory(Logger));
            }
        }

        private static IRetryPolicy _simpleRetryPolicy;
        private static IRetryPolicy SimpleRetryPolicy
        {
            get
            {
                return _simpleRetryPolicy ?? (_simpleRetryPolicy = new SimpleRetryPolicy(Logger));
            }
        }

        private static IRetryPolicy _exponentialRetryPolicy;
        private static IRetryPolicy ExponentialRetryPolicy
        {
            get
            {
                return _exponentialRetryPolicy ?? (_exponentialRetryPolicy = new ExponentialRetryPolicy(Logger));
            }
        }

        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {               
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var messageJson = Encoding.UTF8.GetString(body);

                    try
                    {
                        HandleCommand(messageJson);
                        Logger.Log($"Success Handle {messageJson}");
                        Logger.Log("");
                        channel.BasicAck(ea.DeliveryTag, false);
                    } 
                    catch(HandleMessageException ex)
                    {
                        Logger.Log($"Failed Handle {ex.Message} - send to dead letter exchange");
                        Logger.Log("");
                        channel.BasicNack(ea.DeliveryTag, false, false);
                    }                   
                };
                Console.WriteLine("Listening on events_queue....");
                channel.BasicConsume(queue: "events_queue",
                                     autoAck: false,
                                     consumer: consumer);                            
                Console.ReadLine();
            }


        }

        private static void HandleCommand(string messageJson)
        {
            Message message = JsonConvert.DeserializeObject<Message>(messageJson);            

            ICommand command = null;
            if (message.Type == typeof(GenericCommand).Name)
            {
                command = JsonConvert.DeserializeObject<GenericCommand>(message.JsonMessage);
                HandlerFactory.CreateGenericHandler(SimpleRetryPolicy).Handle((GenericCommand)command);
            }

            if (message.Type == typeof(DoorOpenCommand).Name)
            {
                command = JsonConvert.DeserializeObject<DoorOpenCommand>(message.JsonMessage);
                HandlerFactory.CreateDoorOpenHandler(ExponentialRetryPolicy).Handle((DoorOpenCommand)command);
            }

            if (message.Type == typeof(TempAlertCommand).Name)
            {
                command = JsonConvert.DeserializeObject<TempAlertCommand>(message.JsonMessage);
                HandlerFactory.CreateTempAlarmHandler(ExponentialRetryPolicy).Handle((TempAlertCommand)command);
            }

            if (message.Type == typeof(NullCommand).Name)
            {
                command = JsonConvert.DeserializeObject<NullCommand>(message.JsonMessage);
                HandlerFactory.CreateNullHandler().Handle((NullCommand)command);
            }
        }
    }
}
