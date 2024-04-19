
// Type: MonolithEngine.Serialize

using Newtonsoft.Json;


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
