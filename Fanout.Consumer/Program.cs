using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Connection Create
ConnectionFactory factory = new();
factory.Uri = new("amqps://egzzskzx:XX6dYEZMSwHR_dj4OZUvMkPBBtVK14us@hawk.rmq.cloudamqp.com/egzzskzx");

// Connection Active & Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

Console.WriteLine("Queue Name: ");
string queueName = Console.ReadLine();
channel.QueueDeclare(queue: queueName, exclusive: false);

channel.QueueBind(queue: queueName, exchange: "fanout-exchange-example", routingKey: string.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.ReadLine();  