// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LDTKJsonMapParser
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System.IO;

#nullable disable
namespace MonolithEngine
{
  public class LDTKJsonMapParser : MapSerializer
  {
    public LDTKMap Load(string filePath)
    {
      return new LDTKMap(LDTKJson.FromJson(File.ReadAllText(filePath)));
    }

    LDTKMap MapSerializer.Load(Stream stream)
    {
      return new LDTKMap(LDTKJson.FromJson(new StreamReader(stream).ReadToEnd()));
    }
  }
}
