using MemBus;
using Microsoft.Extensions.DependencyInjection;

namespace MemBusDemo;
public class BusContainer : IocAdapter
{
    private static readonly Dictionary<Type, List<object>> _serviceCache = new();
    private static readonly Dictionary<Type, object> _serviceInstanceCache = new();

    private readonly IServiceProvider _services;

    public BusContainer(IServiceProvider services)
    {
        _services = services;
    }

    public IEnumerable<object> GetAllInstances(Type desiredType)
    {
        if (_serviceCache.TryGetValue(desiredType, out var instances))
        {
            return instances;
        }

        var services = _services.GetServices(desiredType)
            .Where(_ => _ != null)
            .Select(_ =>
            {
                var instanceType = _.GetType();
                if (_serviceInstanceCache.TryGetValue(instanceType, out var instance))
                {
                    return instance;
                }

                _serviceInstanceCache[instanceType] = _;
                return _;
            })
            .ToList();

        _serviceCache[desiredType] = services;

        return services;
    }
}