
// Type: MonolithEngine.TilesetDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class TilesetDefinition
  {
    [JsonProperty("cachedPixelData")]
    public Dictionary<string, object> CachedPixelData { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("padding")]
    public long Padding { get; set; }

    [JsonProperty("pxHei")]
    public long PxHei { get; set; }

    [JsonProperty("pxWid")]
    public long PxWid { get; set; }

    [JsonProperty("relPath")]
    public string RelPath { get; set; }

    [JsonProperty("savedSelections")]
    public Dictionary<string, object>[] SavedSelections { get; set; }

    [JsonProperty("spacing")]
    public long Spacing { get; set; }

    [JsonProperty("tileGridSize")]
    public long TileGridSize { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }
  }
}
