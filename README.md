# RabbitMQ-Sample
Simple RabbitMQ Consoles Apps

Requirements
- Net Core 3.1.
- RabbitMQ Installed. (https://www.rabbitmq.com/)

Apps. Descriptions:

RabbitMQ_QueueCreator
- Create queue: events_queue (routing key: events).
- Create queue: positions_queue (routing key: positions).
- Create and bind Direct Exchange Test with queues.

- Create queue: deadletter_events_queue (routing key: events).
- Create queue: deadletter_positions_queue (routing key: events).
- Create and bind Direct Exchange dead_letter_exchange with queues.

MessagePublisher
- Publish Random events to exchange using events routing key.
- Can run multiples instances.

ConsumerMessage
- Consume Messages from events_queue.
- Can run multiples instances.
