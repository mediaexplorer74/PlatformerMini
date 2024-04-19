// Type: MonolithEngine.Level
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Newtonsoft.Json;

// Level "model"

namespace MonolithEngine
{
  public class Level
  {
    [JsonProperty("__bgColor")]
    public string BgColor { get; set; }

    [JsonProperty("__bgPos")]
    public LevelBackgroundPosition BgPos { get; set; }

    [JsonProperty("__neighbours")]
    public NeighbourLevel[] Neighbours { get; set; }

    [JsonProperty("bgColor")]
    public string LevelBgColor { get; set; }

    [JsonProperty("bgPivotX")]
    public double BgPivotX { get; set; }

    [JsonProperty("bgPivotY")]
    public double BgPivotY { get; set; }

    [JsonProperty("bgPos")]
    public MonolithEngine.BgPos? LevelBgPos { get; set; }

    [JsonProperty("bgRelPath")]
    public string BgRelPath { get; set; }

    [JsonProperty("externalRelPath")]
    public string ExternalRelPath { get; set; }

    [JsonProperty("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("layerInstances")]
    public LayerInstance[] LayerInstances { get; set; }

    [JsonProperty("pxHei")]
    public long PxHei { get; set; }

    [JsonProperty("pxWid")]
    public long PxWid { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }

    [JsonProperty("worldX")]
    public long WorldX { get; set; }

    [JsonProperty("worldY")]
    public long WorldY { get; set; }
  }
}
