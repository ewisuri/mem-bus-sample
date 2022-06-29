using MemBus;
using Microsoft.Extensions.Hosting;

namespace MemBusDemo;
internal class SampleService : BackgroundService
{
    private readonly IBus _bus;

    public SampleService(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var message = new SampleMessage();

        while (stoppingToken.IsCancellationRequested is false)
        {
            _bus.Publish(message);

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}