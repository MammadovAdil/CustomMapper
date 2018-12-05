using Ma.CustomMapper.Models;
using Ma.ExtensionMethods.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Ma.CustomMapper
{
  /// <inheritdoc />
  public class CustomMapper : ICustomMapper
  {
    public Configurations Configurations { get; private set; }

    public CustomMapper()
    {
      Configurations = new Configurations();
    }

    public TTarget Map<TTarget>(object source, bool mapChildEntities = true, object additionalData = null) 
      where TTarget : class
    {
      if (source == null)
        return null;

      var mapper = Configurations.GetMapper<TTarget>(GetSourceType(source));

      TTarget model = mapper != null
        ? mapper.Map(source, mapChildEntities)
        : MapSyncWithAsyncMapper<TTarget>(source, mapChildEntities);

      return MapAdditionalData(model, additionalData);
    }

    private TTarget MapSyncWithAsyncMapper<TTarget>(object source, bool mapChildEntities)
      where TTarget : class
    {
      var asyncMapper = Configurations.GetAsyncMapper<TTarget>(GetSourceType(source));
      return asyncMapper?.MapAsync(source, mapChildEntities).Result;
    }

    public async Task<TTarget> MapAsync<TTarget>(object source, bool mapChildEntities = true, object additionalData = null) 
      where TTarget : class
    {
      if (source == null)
        return null;

      var asyncMapper = Configurations.GetAsyncMapper<TTarget>(GetSourceType(source));

      TTarget model = asyncMapper != null
        ? await asyncMapper.MapAsync(source, mapChildEntities)
        : await MapAsyncWithMapper<TTarget>(source, mapChildEntities);

      return MapAdditionalData(model, additionalData);
    }

    private Task<TTarget> MapAsyncWithMapper<TTarget>(object source, bool mapChildEntities)
      where TTarget : class
    {
      var mapper = Configurations.GetMapper<TTarget>(GetSourceType(source));
      return Task.Run(() =>  mapper?.Map(source, mapChildEntities));
    }
       
    /// <summary>Get true type of source</summary>
    /// <remarks>
    /// If type of source is dynamic proxy type
    /// then get base type as actual type.
    /// Dynamic proxy types are created by EntityFramework
    /// when LazyLoading is enabled.
    /// </remarks>
    /// <param name="source">Source to get type of.</param>
    /// <returns>Type of source</returns>
    private Type GetSourceType(object source)
    {
      var sourceType = source.GetType();

      if (sourceType.IsDynamicProxyType())
        sourceType = sourceType.BaseType;

      return sourceType;
    }



    public TTarget Map<TSource, TTarget>(TSource source, bool mapChildEntities = true, object additionalData = null)
      where TSource : class
      where TTarget : class
    {
      if (source == null)
        return null;

      var mapper = Configurations.GetMapper<TSource, TTarget>();

      TTarget model = mapper != null
        ? mapper.Map(source, mapChildEntities)
        : MapSyncWithAsyncMapper<TSource, TTarget>(source, mapChildEntities);

      return MapAdditionalData(model, additionalData);
    }

    private TTarget MapSyncWithAsyncMapper<TSource, TTarget>(TSource source, bool mapChildEntities)
      where TSource : class
      where TTarget : class
    {
      var asyncMapper = Configurations.GetAsyncMapper<TSource, TTarget>();
      return asyncMapper?.MapAsync(source, mapChildEntities).Result;
    }

    public async Task<TTarget> MapAsync<TSource, TTarget>(TSource source, bool mapChildEntities = true, object additionalData = null)
      where TSource : class
      where TTarget : class
    {
      if (source == null)
        return null;

      var asyncMapper = Configurations.GetAsyncMapper<TSource, TTarget>();

      TTarget model = asyncMapper != null
        ? await asyncMapper.MapAsync(source, mapChildEntities)
        : await MapAsyncWithMapper<TSource, TTarget>(source, mapChildEntities);

      return MapAdditionalData(model, additionalData);
    }

    private Task<TTarget> MapAsyncWithMapper<TSource, TTarget>(TSource source, bool mapChildEntities)
      where TSource : class
      where TTarget : class
    {
      var mapper = Configurations.GetMapper<TSource, TTarget>();
      return Task.Run(() => mapper?.Map(source, mapChildEntities));
    }



    public List<TTarget> Map<TSource, TTarget>(ICollection<TSource> sourceList, bool mapChildEntities = true, object additionalData = null)
      where TSource : class
      where TTarget : class
    {
      List<TTarget> targetList = new List<TTarget>();

      if (sourceList == null || sourceList.Count == 0)
        return targetList;

      foreach (TSource source in sourceList)
      {
        var target = Map<TSource, TTarget>(source, mapChildEntities, additionalData);

        if (target != null)
          targetList.Add(target);
      }

      return targetList;
    }    

    public async Task<List<TTarget>> MapAsync<TSource, TTarget>(ICollection<TSource> sourceList, bool mapChildEntities = true, object additionalData = null)
      where TSource : class
      where TTarget : class
    {
      List<TTarget> targetList = new List<TTarget>();

      if (sourceList == null || sourceList.Count == 0)
        return targetList;

      foreach (TSource source in sourceList)
      {
        var target = await MapAsync<TSource, TTarget>(source, mapChildEntities, additionalData);

        if (target != null)
          targetList.Add(target);
      }

      return targetList;
    }

    /// <summary>Map additional data from anonymous object to model.</summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <param name="model">Model to set additional data.</param>
    /// <param name="additionalData">Additional data as anonymous object.</param>
    /// <returns>Model with mapped additional data.</returns>
    private TModel MapAdditionalData<TModel>(
        TModel model,
        object additionalData)
        where TModel : class
    {
      if (model == null || additionalData == null)
        return model;

      foreach (PropertyInfo property
          in additionalData.GetType().GetProperties())
      {
        model.SetPropertyValue(
            property.Name,
            additionalData.GetPropertyValue(property));
      }

      return model;
    }
  }
}
