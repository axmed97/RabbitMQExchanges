using RabbitMQ.Client;
using System.Text;

// Connection Create
ConnectionFactory factory = new();
factory.Uri = new("amqps://egzzskzx:XX6dYEZMSwHR_dj4OZUvMkPBBtVK14us@hawk.rmq.cloudamqp.com/egzzskzx");

// Connection Active & Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);


for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Hello: {i}");
    Console.WriteLine("Topic: ");
    string topic = Console.ReadLine();

    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic, body: message);
}

Console.ReadLine();