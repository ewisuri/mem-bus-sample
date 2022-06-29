namespace MemBusDemo;

public interface IConsume<T>
{
    void Consume(T message);
}