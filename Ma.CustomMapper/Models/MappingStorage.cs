using System;
using System.Collections.Generic;

namespace Ma.CustomMapper.Models
{
  /// <summary>Storage to hold data related to mapping.</summary>
  internal class MappingStorage
  {
    // This is for singleton pattern
    private static Lazy<MappingStorage> lazy =
        new Lazy<MappingStorage>(() => new MappingStorage());

    public static MappingStorage Instance { get { return lazy.Value; } }

    private MappingStorage()
    {
      MappingConfigurations = new List<MappingConfiguration>();
    }

    internal List<MappingConfiguration> MappingConfigurations { get; set; }
  }
}
