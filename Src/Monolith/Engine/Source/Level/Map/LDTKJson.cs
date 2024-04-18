// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LDTKJson
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class LDTKJson
  {
    [JsonProperty("backupLimit")]
    public long BackupLimit { get; set; }

    [JsonProperty("backupOnSave")]
    public bool BackupOnSave { get; set; }

    [JsonProperty("bgColor")]
    public string BgColor { get; set; }

    [JsonProperty("defaultGridSize")]
    public long DefaultGridSize { get; set; }

    [JsonProperty("defaultLevelBgColor")]
    public string DefaultLevelBgColor { get; set; }

    [JsonProperty("defaultLevelHeight")]
    public long DefaultLevelHeight { get; set; }

    [JsonProperty("defaultLevelWidth")]
    public long DefaultLevelWidth { get; set; }

    [JsonProperty("defaultPivotX")]
    public double DefaultPivotX { get; set; }

    [JsonProperty("defaultPivotY")]
    public double DefaultPivotY { get; set; }

    [JsonProperty("defs")]
    public Definitions Defs { get; set; }

    [JsonProperty("exportPng")]
    public bool ExportPng { get; set; }

    [JsonProperty("exportTiled")]
    public bool ExportTiled { get; set; }

    [JsonProperty("externalLevels")]
    public bool ExternalLevels { get; set; }

    [JsonProperty("flags")]
    public Flag[] Flags { get; set; }

    [JsonProperty("jsonVersion")]
    public string JsonVersion { get; set; }

    [JsonProperty("levels")]
    public Level[] Levels { get; set; }

    [JsonProperty("minifyJson")]
    public bool MinifyJson { get; set; }

    [JsonProperty("nextUid")]
    public long NextUid { get; set; }

    [JsonProperty("pngFilePattern")]
    public string PngFilePattern { get; set; }

    [JsonProperty("worldGridHeight")]
    public long WorldGridHeight { get; set; }

    [JsonProperty("worldGridWidth")]
    public long WorldGridWidth { get; set; }

    [JsonProperty("worldLayout")]
    public WorldLayout WorldLayout { get; set; }

    public static LDTKJson FromJson(string json)
    {
      return JsonConvert.DeserializeObject<LDTKJson>(json, Converter.Settings);
    }
  }
}
