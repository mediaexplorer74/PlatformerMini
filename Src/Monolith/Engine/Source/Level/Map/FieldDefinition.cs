// Decompiled with JetBrains decompiler
// Type: MonolithEngine.FieldDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;

#nullable disable
namespace MonolithEngine
{
  public class FieldDefinition
  {
    [JsonProperty("__type")]
    public string Type { get; set; }

    [JsonProperty("acceptFileTypes")]
    public string[] AcceptFileTypes { get; set; }

    [JsonProperty("arrayMaxLength")]
    public long? ArrayMaxLength { get; set; }

    [JsonProperty("arrayMinLength")]
    public long? ArrayMinLength { get; set; }

    [JsonProperty("canBeNull")]
    public bool CanBeNull { get; set; }

    [JsonProperty("defaultOverride")]
    public object DefaultOverride { get; set; }

    [JsonProperty("editorAlwaysShow")]
    public bool EditorAlwaysShow { get; set; }

    [JsonProperty("editorCutLongValues")]
    public bool EditorCutLongValues { get; set; }

    [JsonProperty("editorDisplayMode")]
    public EditorDisplayMode EditorDisplayMode { get; set; }

    [JsonProperty("editorDisplayPos")]
    public EditorDisplayPos EditorDisplayPos { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("isArray")]
    public bool IsArray { get; set; }

    [JsonProperty("max")]
    public double? Max { get; set; }

    [JsonProperty("min")]
    public double? Min { get; set; }

    [JsonProperty("regex")]
    public string Regex { get; set; }

    [JsonProperty("textLangageMode")]
    public MonolithEngine.TextLangageMode? TextLangageMode { get; set; }

    [JsonProperty("type")]
    public object FieldDefinitionType { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }
  }
}
