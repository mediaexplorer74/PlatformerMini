
// Type: MonolithEngine.EntityInstance
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class EntityInstance
  {
    [JsonProperty("__grid")]
    public long[] Grid { get; set; }

    [JsonProperty("__identifier")]
    public string Identifier { get; set; }

    [JsonProperty("__pivot")]
    public double[] Pivot { get; set; }

    [JsonProperty("__tile")]
    public EntityInstanceTile Tile { get; set; }

    [JsonProperty("defUid")]
    public long DefUid { get; set; }

    [JsonProperty("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("px")]
    public long[] Px { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }
  }
}
