namespace eis_integration_sample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<IMessageProcessor, EisEventProcessorService>();
        services.AddEISService();
    }

    public static IApplicationBuilder UserEISInfrastructure(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.AddEISProcessor<EisEventProcessorService>();
    }
}