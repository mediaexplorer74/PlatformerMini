// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IntGridValueDefinition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class IntGridValueDefinition
  {
    [JsonProperty("color")]
    public string Color { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("value")]
    public long Value { get; set; }
  }
}
