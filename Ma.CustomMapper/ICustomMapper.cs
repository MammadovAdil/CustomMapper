using Ma.CustomMapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ma.CustomMapper
{
  /// <summary>Mapper to map one type to another</summary>
  public interface ICustomMapper
  {
    /// <summary>Mapping configurations</summary>
    Configurations Configurations { get; }

    /// <summary>Map source object from type of source type to type of TTarget</summary>
    /// <typeparam name="TTarget">Target type to map source to</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Mapped object</returns>
    TTarget Map<TTarget>(object source, bool mapChildEntities = true, object additionalData = null) 
      where TTarget : class;

    /// <summary>Map source object from type of source type to type of TTarget asynchronously</summary>
    /// <typeparam name="TTarget">Target type to map source to</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Task to get mapped object</returns>
    Task<TTarget> MapAsync<TTarget>(object source, bool mapChildEntities = true, object additionalData = null)
      where TTarget : class;



    /// <summary>Map source from type of TSource to type of TTarget</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Mapped object</returns>
    TTarget Map<TSource, TTarget>(TSource source, bool mapChildEntities = true, object additionalData = null)
        where TSource : class
        where TTarget : class;

    /// <summary>Map source from type of TSource to type of TTarget asynchronously</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Task to get mapped object</returns>
    Task<TTarget> MapAsync<TSource, TTarget>(TSource source, bool mapChildEntities = true, object additionalData = null)
        where TSource : class
        where TTarget : class;



    /// <summary>Map list of entities from TSource type to list of TTarget type.</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Mapped list.</returns>
    List<TTarget> Map<TSource, TTarget>(ICollection<TSource> sourceList, bool mapChildEntities = true, object additionalData = null)
        where TTarget : class
        where TSource : class;

    /// <summary>Map list of entities from TSource type to list of TTarget type asynchronously</summary>
    /// <typeparam name="TSource">Type of source</typeparam>
    /// <typeparam name="TTarget">Type of target</typeparam>
    /// <param name="source">Source to map</param>
    /// <param name="mapChildEntities">Also Map child entities in this source</param>
    /// <param name="additionalData">Additional data to map to model.</param>
    /// <returns>Task to get mapped list</returns>
    Task<List<TTarget>> MapAsync<TSource, TTarget>(ICollection<TSource> sourceList, bool mapChildEntities = true, object additionalData = null)
        where TTarget : class
        where TSource : class;
  }
}
