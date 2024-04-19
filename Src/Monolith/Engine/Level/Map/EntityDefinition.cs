
// Type: MonolithEngine.EntityDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class EntityDefinition
  {
    [JsonProperty("color")]
    public string Color { get; set; }

    [JsonProperty("fieldDefs")]
    public FieldDefinition[] FieldDefs { get; set; }

    [JsonProperty("fillOpacity")]
    public double FillOpacity { get; set; }

    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("hollow")]
    public bool Hollow { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("keepAspectRatio")]
    public bool KeepAspectRatio { get; set; }

    [JsonProperty("limitBehavior")]
    public LimitBehavior LimitBehavior { get; set; }

    [JsonProperty("limitScope")]
    public LimitScope LimitScope { get; set; }

    [JsonProperty("lineOpacity")]
    public double LineOpacity { get; set; }

    [JsonProperty("maxCount")]
    public long MaxCount { get; set; }

    [JsonProperty("pivotX")]
    public double PivotX { get; set; }

    [JsonProperty("pivotY")]
    public double PivotY { get; set; }

    [JsonProperty("renderMode")]
    public RenderMode RenderMode { get; set; }

    [JsonProperty("resizableX")]
    public bool ResizableX { get; set; }

    [JsonProperty("resizableY")]
    public bool ResizableY { get; set; }

    [JsonProperty("showName")]
    public bool ShowName { get; set; }

    [JsonProperty("tags")]
    public string[] Tags { get; set; }

    [JsonProperty("tileId")]
    public long? TileId { get; set; }

    [JsonProperty("tileRenderMode")]
    public TileRenderMode TileRenderMode { get; set; }

    [JsonProperty("tilesetId")]
    public long? TilesetId { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }
  }
}
