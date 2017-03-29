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
        /// <summary>
        /// Add new mapper to configurations
        /// </summary>
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

            // Get existing configuration
            MappingConfiguration currentConfig = MappingStorage
                .Instance
                .MappingConfigurations
                .Where(c => c.SourceType.Equals(typeof(TSource))
                    && c.TargetType.Equals(typeof(TTarget)))
                .FirstOrDefault();

            // If there is no mathing configuration, initialize it
            if (currentConfig == null)
                currentConfig = new MappingConfiguration
                {
                    SourceType = typeof(TSource),
                    TargetType = typeof(TTarget)
                };

            // Set the mapper
            currentConfig.Mapper = mapper;

            if (!MappingStorage.Instance.MappingConfigurations.Contains(currentConfig))
                MappingStorage.Instance.MappingConfigurations.Add(currentConfig);
        }

        /// <summary>
        /// Get mapper from sourceType to TTarget
        /// </summary>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="sourceType">Type of source</param>
        /// <returns>Mapper interface which maps sourceType to TTarget</returns>
        internal IMapper<TTarget> GetMapper<TTarget>(Type sourceType)
            where TTarget : class
        {
            MappingConfiguration configuration = MappingStorage
                .Instance
                .MappingConfigurations
                .Where(c => c.SourceType.Equals(sourceType)
                    && c.TargetType.Equals(typeof(TTarget)))
                .FirstOrDefault();

            if (configuration == null)
                throw new ArgumentException(string.Format(
                    "No mapping configuration found for {0} => {1}",
                    sourceType.Name,
                    typeof(TTarget).Name));

            IMapper<TTarget> mapper = configuration.Mapper as IMapper<TTarget>;

            return mapper;
        }

        /// <summary>
        /// Get mapper from TSource to TTarget
        /// </summary>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <returns>Mapper from source to target</returns>
        internal IMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
            where TSource : class
            where TTarget : class
        {
            MappingConfiguration configuration = MappingStorage
                .Instance
                .MappingConfigurations
                .Where(c => c.SourceType.Equals(typeof(TSource))
                    && c.TargetType.Equals(typeof(TTarget)))
                .FirstOrDefault();

            if (configuration == null)
                throw new ArgumentException(string.Format(
                    "No mapping configuration found for {0} => {1}",
                    typeof(TSource),
                    typeof(TTarget)));

            IMapper<TSource, TTarget> mapper = configuration.Mapper as IMapper<TSource, TTarget>;

            return mapper;
        }
    }
}
