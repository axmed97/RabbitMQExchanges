using RabbitMQ.Client;
using System.Text;

// Connection Create
ConnectionFactory factory = new();
factory.Uri = new("amqps://egzzskzx:XX6dYEZMSwHR_dj4OZUvMkPBBtVK14us@hawk.rmq.cloudamqp.com/egzzskzx");

// Connection Active & Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Hello: {i}");
    Console.WriteLine("Header value: ");
    string value = Console.ReadLine();
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.Headers = new Dictionary<string, object>()
    {
        ["no"] = value
    };

    channel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: message, basicProperties: properties);
}
Console.ReadLine();