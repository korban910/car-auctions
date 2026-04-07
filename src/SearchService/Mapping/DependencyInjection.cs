namespace SearchService.Mapping;

public static class DependencyInjection
{
    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {}, typeof(DependencyInjection).Assembly);
    }
}