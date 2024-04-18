// Decompiled with JetBrains decompiler
// Type: MonolithEngine.EnumDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class EnumDefinition
  {
    [JsonProperty("externalFileChecksum")]
    public string ExternalFileChecksum { get; set; }

    [JsonProperty("externalRelPath")]
    public string ExternalRelPath { get; set; }

    [JsonProperty("iconTilesetUid")]
    public long? IconTilesetUid { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("uid")]
    public long Uid { get; set; }

    [JsonProperty("values")]
    public EnumValueDefinition[] Values { get; set; }
  }
}
