using Ma.CustomMapper.Abstract;
using System;
using System.Linq;

namespace Ma.CustomMapper.Models
{
  /// <summary>
  /// Configurations of Mapper.
  /// </summary>
  public class Configurations
  {
    /// <summary>Add new mapper to configurations</summary>
    /// <exception cref="ArgumentNullException">
    /// When mapper is null
    /// </exception>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="mapper">Mapper which maps source to target type</param>
    public void AddMapper<TSource, TTarget>(IMapper<TSource, TTarget> mapper)
        where TSource : class
        where TTarget : class
    {
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      AddMapper(typeof(TSource), typeof(TTarget), mapper);
    }

    /// <summary>Add new async mapper to configurations</summary>
    /// <exception cref="ArgumentNullException">
    /// When mapper is null
    /// </exception>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="mapper">Mapper which maps source to target type</param>
    public void AddAsyncMapper<TSource, TTarget>(IAsyncMapper<TSource, TTarget> mapper)
        where TSource : class
        where TTarget : class
    {
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      AddMapper(typeof(TSource), typeof(TTarget), mapper);
    }

    private void AddMapper(Type sourceType, Type targetType, IMapperBase mapper)
    {
      var currentConfig = MappingStorage
          .Instance
          .MappingConfigurations
          .Where(c => c.SourceType.Equals(sourceType)
              && c.TargetType.Equals(targetType))
          .FirstOrDefault();

      if (currentConfig == null)
        currentConfig = new MappingConfiguration
        {
          SourceType = sourceType,
          TargetType = targetType
        };

      if (!MappingStorage.Instance.MappingConfigurations.Contains(currentConfig))
        MappingStorage.Instance.MappingConfigurations.Add(currentConfig);
    }

    /// <summary>Get mapper from sourceType to TTarget</summary>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="sourceType">Type of source</param>
    /// <returns>Mapper interface which maps sourceType to TTarget</returns>
    internal IMapper<TTarget> GetMapper<TTarget>(Type sourceType)
        where TTarget : class
    {
      var config = GetMappingConfig(sourceType, typeof(TTarget));
      var mapper = config.Mapper as IMapper<TTarget>;

      return mapper;
    }

    /// <summary>Get mapper from TSource to TTarget</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <returns>Mapper from source to target</returns>
    internal IMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        where TSource : class
        where TTarget : class
    {
      var config = GetMappingConfig(typeof(TSource), typeof(TTarget));
      var mapper = config.Mapper as IMapper<TSource, TTarget>;

      return mapper;
    }

    /// <summary>Get async mapper from sourceType to TTarget</summary>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="sourceType">Type of source</param>
    /// <returns>Async mapper interface which maps sourceType to TTarget</returns>
    internal IAsyncMapper<TTarget> GetAsyncMapper<TTarget>(Type sourceType)
      where TTarget : class
    {
      var config = GetMappingConfig(sourceType, typeof(TTarget));
      var mapper = config.Mapper as IAsyncMapper<TTarget>;

      return mapper;
    }

    /// <summary>Get async mapper from TSource to TTarget</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <returns>Async mapper from source to target</returns>
    internal IAsyncMapper<TSource, TTarget> GetAsyncMapper<TSource, TTarget>()
        where TSource : class
        where TTarget : class
    {
      var config = GetMappingConfig(typeof(TSource), typeof(TTarget));
      var mapper = config.Mapper as IAsyncMapper<TSource, TTarget>;

      return mapper;
    }

    private MappingConfiguration GetMappingConfig(Type source, Type target)
    {
      var config = MappingStorage
          .Instance
          .MappingConfigurations
          .Where(c => c.SourceType.Equals(source)
              && c.TargetType.Equals(target))
          .FirstOrDefault();

      if (config == null)
        throw new ArgumentException(string.Format(
            "No mapping configuration found for {0} => {1}",
            source.Name,
            target.Name));

      return config;
    }
  }
}
