// Decompiled with JetBrains decompiler
// Type: MonolithEngine.TileInstance
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class TileInstance
  {
    [JsonProperty("d")]
    public long[] D { get; set; }

    [JsonProperty("f")]
    public long F { get; set; }

    [JsonProperty("px")]
    public long[] Px { get; set; }

    [JsonProperty("src")]
    public long[] Src { get; set; }

    [JsonProperty("t")]
    public long T { get; set; }
  }
}
