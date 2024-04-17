// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Serialize
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;

#nullable disable
namespace MonolithEngine
{
  public static class Serialize
  {
    public static string ToJson(this LDTKJson self)
    {
      return JsonConvert.SerializeObject((object) self, Converter.Settings);
    }
  }
}
