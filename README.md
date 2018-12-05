# CustomMapper

Implement your mapper classes from `IMapper<,>` or from `IAsyncMapper<,>` interface.

Add following method to the project where you have mapper classes,
and call it when program starts to register all mappers:

```cs
/// <summary>
/// Register all custom mapper classes.
/// </summary>
public static void RegisterMappers()
{
    var mapperInterfaceTypes = new[] { typeof(IMapper<,>), typeof(IAsyncMapper<,>) };

    var mapperTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(m => m.GetInterfaces().Any(i => i.IsGenericType
            && mapperInterfaceTypes.Contains(i.GetGenericTypeDefinition())));

    var customMapper = kernel.Get<ICustomMapper>();
    foreach (Type mapperType in mapperTypes)
    {
    dynamic mapper = kernel.Get(mapperType);
    customMapper.Configurations.AddMapper(mapper);
    }
}
```

Then you can inject CustomMapper as an instance of ICustomMapper, or initialize and call one `Map` or `MapAsync` methods which suits you.
Both of these methods should work for normal and async mappers.

You can find **NuGet package** for this project [here](https://www.nuget.org/packages/Ma.CustomMapper/).
