using Ma.CustomMapper.Abstract;
using System;

namespace Ma.CustomMapper.Models
{
  /// <summary>Configuration for mapping.</summary>
  public class MappingConfiguration
  {
    public Type SourceType { get; internal set; }
    public Type TargetType { get; internal set; }
    public IMapperBase Mapper { get; internal set; }
  }
}
