using Lamar;
using Lamar.Microsoft.DependencyInjection;
using MemBus;
using MemBus.Configurators;
using MemBusDemo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var app = Host.CreateDefaultBuilder(args)
    .UseLamar(_ =>
     {
         _.Scan(s =>
         {
             var consumerType = typeof(IConsume<>);
             s.AssemblyContainingType(consumerType);
             s.AddAllTypesOf(consumerType);

             s.WithDefaultConventions();
         });

         _.AddSingleton(services => BusSetup
             .StartWith<Conservative>()
             .Apply<IoCSupport>(s => s.SetAdapter(new BusContainer(services)).SetHandlerInterface(typeof(IConsume<>)))
             .Construct());
     })
    .ConfigureServices(_ =>
    {
        _.AddHostedService<SampleService>();
    })
    .Build();

var container = app.Services.GetRequiredService<IContainer>();
container.AssertConfigurationIsValid();

await app.RunAsync();