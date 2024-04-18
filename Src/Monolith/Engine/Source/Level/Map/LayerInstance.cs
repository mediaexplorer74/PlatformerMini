// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LayerInstance
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class LayerInstance
  {
    [JsonProperty("__cHei")]
    public long CHei { get; set; }

    [JsonProperty("__cWid")]
    public long CWid { get; set; }

    [JsonProperty("__gridSize")]
    public long GridSize { get; set; }

    [JsonProperty("__identifier")]
    public string Identifier { get; set; }

    [JsonProperty("__opacity")]
    public double Opacity { get; set; }

    [JsonProperty("__pxTotalOffsetX")]
    public long PxTotalOffsetX { get; set; }

    [JsonProperty("__pxTotalOffsetY")]
    public long PxTotalOffsetY { get; set; }

    [JsonProperty("__tilesetDefUid")]
    public long? TilesetDefUid { get; set; }

    [JsonProperty("__tilesetRelPath")]
    public string TilesetRelPath { get; set; }

    [JsonProperty("__type")]
    public string Type { get; set; }

    [JsonProperty("autoLayerTiles")]
    public TileInstance[] AutoLayerTiles { get; set; }

    [JsonProperty("entityInstances")]
    public EntityInstance[] EntityInstances { get; set; }

    [JsonProperty("gridTiles")]
    public TileInstance[] GridTiles { get; set; }

    [JsonProperty("intGrid", NullValueHandling = NullValueHandling.Ignore)]
    public IntGridValueInstance[] IntGrid { get; set; }

    [JsonProperty("intGridCsv")]
    public long[] IntGridCsv { get; set; }

    [JsonProperty("layerDefUid")]
    public long LayerDefUid { get; set; }

    [JsonProperty("levelId")]
    public long LevelId { get; set; }

    [JsonProperty("overrideTilesetUid")]
    public long? OverrideTilesetUid { get; set; }

    [JsonProperty("pxOffsetX")]
    public long PxOffsetX { get; set; }

    [JsonProperty("pxOffsetY")]
    public long PxOffsetY { get; set; }

    [JsonProperty("seed")]
    public long Seed { get; set; }

    [JsonProperty("visible")]
    public bool Visible { get; set; }
  }
}
