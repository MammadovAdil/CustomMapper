using System.Threading.Tasks;

namespace Ma.CustomMapper.Abstract
{
  /// <summary>Asynchronous mapper interface with only Target type.</summary>
  /// <typeparam name="TTarget">Type of target</typeparam>
  public interface IAsyncMapper<TTarget> : IMapperBase
      where TTarget : class
  {
    /// <summary>Map source object from type of source to TTarget type asynchronously</summary>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <returns>Task to get mapped object of TTarget type</returns>
    Task<TTarget> MapAsync(object source, bool mapChildEntities);
  }

  /// <summary>
  /// Interface which custom mappers must implement
  /// in order to be able to map object from one type
  /// to other asynchronously.
  /// </summary>
  /// <typeparam name="TSource">Thpe of source.</typeparam>
  /// <typeparam name="TTarget">Type of target.</typeparam>
  public interface IAsyncMapper<TSource, TTarget> : IAsyncMapper<TTarget>
      where TSource : class
      where TTarget : class
  {
  }
}
