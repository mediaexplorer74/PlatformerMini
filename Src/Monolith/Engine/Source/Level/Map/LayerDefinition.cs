// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LayerDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class LayerDefinition
  {
    [JsonProperty("__type")]
    public string Type { get; set; }

    [JsonProperty("autoRuleGroups")]
    public Dictionary<string, object>[] AutoRuleGroups { get; set; }

    [JsonProperty("autoSourceLayerDefUid")]
    public long? AutoSourceLayerDefUid { get; set; }

    [JsonProperty("autoTilesetDefUid")]
    public long? AutoTilesetDefUid { get; set; }

    [JsonProperty("displayOpacity")]
    public double DisplayOpacity { get; set; }

    [JsonProperty("excludedTags")]
    public string[] ExcludedTags { get; set; }

    [JsonProperty("gridSize")]
    public long GridSize { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("intGridValues")]
    public IntGridValueDefinition[] IntGridValues { get; set; }

    [JsonProperty("pxOffsetX")]
    public long PxOffsetX { get; set; }

    [JsonProperty("pxOffsetY")]
    public long PxOffsetY { get; set; }

    [JsonProperty("requiredTags")]
    public string[] RequiredTags { get; set; }

    [JsonProperty("tilePivotX")]
    public double TilePivotX { get; set; }

    [JsonProperty("tilePivotY")]
    public double TilePivotY { get; set; }

    [JsonProperty("tilesetDefUid")]
    public long? TilesetDefUid { get; set; }

    [JsonProperty("type")]
    public TypeEnum LayerDefinitionType { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }
  }
}
