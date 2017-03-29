using Ma.CustomMapper.Abstract;
using Ma.CustomMapper.Models;
using System;
using System.Collections.Generic;
using Ma.ExtensionMethods.Reflection;
using System.Reflection;

namespace Ma.CustomMapper
{
    /// <summary>
    /// Mapper class to map from one type to another
    /// </summary>
    public static class Mapper
    {
        public static Configurations Configurations { get; set; }

        static Mapper()
        {
            Configurations = new Configurations();
        }

        /// <summary>
        /// Map source object from type of source type to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TTarget">Target type to map source to</typeparam>
        /// <param name="source">Source to map</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TTarget>(object source)
            where TTarget : class
        {
            return Map<TTarget>(source, true);
        }

        /// <summary>
        /// Map source object from type of source type to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TTarget">Target type to map source to</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TTarget>(object source, bool mapChildEntities)
            where TTarget : class
        {
            return Map<TTarget>(source, mapChildEntities, null);
        }

        /// <summary>
        /// Map source object from type of source type to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TTarget">Target type to map source to</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TTarget>(object source, object additionalData)
            where TTarget : class
        {
            return Map<TTarget>(source, true, additionalData);
        }

        /// <summary>
        /// Map source object from type of source type to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TTarget">Target type to map source to</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TTarget>(
            object source, 
            bool mapChildEntities, 
            object additionalData)
            where TTarget : class
        {
            if (source == null)
                return null;

            Type sourceType = source.GetType();

            // If type of source is dynamic proxy type
            // then get base type as actual type.
            // Dynamic proxy types are created by EntityFramework
            // when LazyLoading is enabled.
            if (sourceType.IsDynamicProxyType())
                sourceType = sourceType.BaseType;

            IMapper<TTarget> mapper = Configurations
                .GetMapper<TTarget>(sourceType);

            TTarget model = mapper.Map(source, mapChildEntities);

            // If additional data has been passed map additional data
            if (additionalData != null)
                model = MapAdditionalData(model, additionalData);

            return model;
        }

        /// <summary>
        /// Map source from type of TSource to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(source, true);
        }        

        /// <summary>
        /// Map source from type of TSource to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TSource, TTarget>(TSource source, bool mapChildEntities)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(source, mapChildEntities, null);
        }

        /// <summary>
        /// Map source from type of TSource to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TSource, TTarget>(TSource source, object additionalData)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(source, true, additionalData);
        }

        /// <summary>
        /// Map source from type of TSource to type of TTarget
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When source is null
        /// </exception>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped object</returns>
        public static TTarget Map<TSource, TTarget>(
            TSource source, 
            bool mapChildEntities, 
            object additionalData)
            where TSource : class
            where TTarget : class
        {
            if(source == null)
                throw new ArgumentNullException("source");

            IMapper<TSource, TTarget> mapper = Configurations
                .GetMapper<TSource, TTarget>();

            TTarget model = mapper.Map(source, mapChildEntities);

            // If additional data has been passed map additional data
            if (additionalData != null)
                model = MapAdditionalData(model, additionalData);

            return model;
        }

        /// <summary>
        /// Map list of entities from TSource type to list of TTarget type.
        /// </summary>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <returns>Mapped list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(ICollection<TSource> sourceList)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(sourceList, true, null);
        }

        /// <summary>
        /// Map list of entities from TSource type to list of TTarget type.
        /// </summary>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <returns>Mapped list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(
            ICollection<TSource> sourceList, bool mapChildEntities)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(sourceList, mapChildEntities, null);
        }

        /// <summary>
        /// Map list of entities from TSource type to list of TTarget type.
        /// </summary>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(
            ICollection<TSource> sourceList, object additionalData)
            where TSource : class
            where TTarget : class
        {
            return Map<TSource, TTarget>(sourceList, true, additionalData);
        }

        /// <summary>
        /// Map list of entities from TSource type to list of TTarget type.
        /// </summary>
        /// <typeparam name="TSource">Type of source</typeparam>
        /// <typeparam name="TTarget">Type of target</typeparam>
        /// <param name="source">Source to map</param>
        /// <param name="mapChildEntities">Also Map child entities in this source</param>
        /// <param name="additionalData">Additional data to map to model.</param>
        /// <returns>Mapped list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(
            ICollection<TSource> sourceList,
            bool mapChildEntities,
            object additionalData)
            where TTarget : class
            where TSource : class
        {
            List<TTarget> targetList = new List<TTarget>();

            if (sourceList == null
                || sourceList.Count == 0)
                return targetList;

            foreach (TSource source in sourceList)
            {
                TTarget target = Mapper.Map<TSource, TTarget>(
                    source, mapChildEntities, additionalData);

                if(target != null)
                    targetList.Add(target);
            }

            return targetList;
        }

        /// <summary>
        /// Map additional data from anonymous object to model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="model">Model to set additional data.</param>
        /// <param name="additionalData">Additional data as anonymous object.</param>
        /// <returns>Model with mapped additional data.</returns>
        private static TModel MapAdditionalData<TModel>(
            TModel model, 
            object additionalData)
            where TModel : class
        {
            // If there is no data to map return model as is.
            if (model == null || additionalData == null)
                return model;

            // Loop through properties of additional data and set
            // their values at model object.
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
