using Ma.CustomMapper.Abstract;
using System;

namespace Ma.CustomMapper.Models
{
    /// <summary>
    /// Configuration for mapping.
    /// </summary>
    internal class MappingConfiguration
    {
        internal Type SourceType { get; set; }
        internal Type TargetType { get; set; }
        internal IMapperBase Mapper { get; set; }
    }
}
