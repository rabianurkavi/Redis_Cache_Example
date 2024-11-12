using StackExchange.Redis;

ConnectionMultiplexer redis= await ConnectionMultiplexer.ConnectAsync("localhost:9015");

ISubscriber subscriber= redis.GetSubscriber();
while(true)
{
    Console.WriteLine("Mesaj: ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel",message);
}
