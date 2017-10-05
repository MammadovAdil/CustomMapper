#CustomMapper

Implement your mapper classes from `IMapper<,>` interface.

Add following method to the project where you have mapper classes,
and call it when program starts to register all mappers:

```cs
/// <summary>
/// Register all custom mapper classes.
/// </summary>
public static void RegisterMappers()
{
    Type mapperInterfaceType = typeof(IMapper<,>);

    var mapperTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(m => m.GetInterfaces().Any(i => i.IsGenericType
            && i.GetGenericTypeDefinition().Equals(mapperInterfaceType)));

    foreach (Type mapperType in mapperTypes)
    {
        dynamic mapper = Activator.CreateInstance(mapperType);
        Mapper.Configurations.AddMapper(mapper);
    }
}
```

You can find **NuGet package** for this project [here](https://www.nuget.org/packages/Ma.CustomMapper/).