namespace MemBusDemo;
public class SampleConsumer : IConsume<SampleMessage>
{
    public void Consume(SampleMessage message)
    {
        Console.WriteLine("Got a message");
    }
}
