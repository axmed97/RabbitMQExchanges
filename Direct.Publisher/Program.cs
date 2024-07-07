using RabbitMQ.Client;
using System.Text;

// Connection Create
ConnectionFactory factory = new();
factory.Uri = new("amqps://egzzskzx:XX6dYEZMSwHR_dj4OZUvMkPBBtVK14us@hawk.rmq.cloudamqp.com/egzzskzx");

// Connection Active & Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
while (true)
{
    Console.WriteLine("Message: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
}
Console.ReadLine();
