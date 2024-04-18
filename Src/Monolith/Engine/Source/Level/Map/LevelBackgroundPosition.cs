// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LevelBackgroundPosition
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;


namespace MonolithEngine
{
  public class LevelBackgroundPosition
  {
    [JsonProperty("cropRect")]
    public double[] CropRect { get; set; }

    [JsonProperty("scale")]
    public double[] Scale { get; set; }

    [JsonProperty("topLeftPx")]
    public long[] TopLeftPx { get; set; }
  }
}
