// Type: MonolithEngine.FieldInstance
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Newtonsoft.Json;

#nullable disable
namespace MonolithEngine
{
  public class FieldInstance
  {
    [JsonProperty("__identifier")]
    public string Identifier { get; set; }

    [JsonProperty("__type")]
    public string Type { get; set; }

    [JsonProperty("__value")]
    public object Value { get; set; }

    [JsonProperty("defUid")]
    public long DefUid { get; set; }

    [JsonProperty("realEditorValues")]
    public object[] RealEditorValues { get; set; }
  }
}
