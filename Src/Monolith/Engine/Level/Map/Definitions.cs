
// Type: MonolithEngine.Definitions
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class Definitions
  {
    [JsonProperty("entities")]
    public EntityDefinition[] Entities { get; set; }

    [JsonProperty("enums")]
    public EnumDefinition[] Enums { get; set; }

    [JsonProperty("externalEnums")]
    public EnumDefinition[] ExternalEnums { get; set; }

    [JsonProperty("layers")]
    public LayerDefinition[] Layers { get; set; }

    [JsonProperty("levelFields")]
    public FieldDefinition[] LevelFields { get; set; }

    [JsonProperty("tilesets")]
    public TilesetDefinition[] Tilesets { get; set; }
  }
}
