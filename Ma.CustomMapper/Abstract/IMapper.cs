namespace Ma.CustomMapper.Abstract
{
    /// <summary>
    /// Base mapper interface
    /// </summary>
    public interface IMapperBase
    {
    }

    /// <summary>
    /// Mapper interface with only Target type
    /// </summary>
    /// <typeparam name="TTarget">Type of target</typeparam>
    public interface IMapper<TTarget> : IMapperBase
        where TTarget : class
    {
        /// <summary>
        /// Map source object from type of source to TTarget type
        /// </summary>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <returns>Mapped object of TTarget type</returns>
        TTarget Map(object source, bool mapChildEntities);
    }

    /// <summary>
    /// Interface which custom mappers must implement
    /// in order to be able to map object from one type
    /// to other.
    /// </summary>
    /// <typeparam name="TSource">Thpe of source.</typeparam>
    /// <typeparam name="TTarget">Type of target.</typeparam>
    public interface IMapper<TSource, TTarget> : IMapper<TTarget>
        where TSource : class
        where TTarget : class
    {
    }
}
