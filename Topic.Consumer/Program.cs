using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Connection Create
ConnectionFactory factory = new();
factory.Uri = new("amqps://egzzskzx:XX6dYEZMSwHR_dj4OZUvMkPBBtVK14us@hawk.rmq.cloudamqp.com/egzzskzx");

// Connection Active & Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.WriteLine("Topic:");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.ReadLine();