// Decompiled with JetBrains decompiler
// Type: MonolithEngine.EnumValueDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class EnumValueDefinition
  {
    [JsonProperty("__tileSrcRect")]
    public long[] TileSrcRect { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("tileId")]
    public long? TileId { get; set; }
  }
}
