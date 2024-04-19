// Type: MonolithEngine.LDTKJsonMapParser
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System.IO;


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
