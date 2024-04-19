
// Type: MonolithEngine.WorldLayoutConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class WorldLayoutConverter : JsonConverter
  {
    public static readonly WorldLayoutConverter Singleton = new WorldLayoutConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (WorldLayout) || t == typeof (WorldLayout?);
    }

    public override object ReadJson(
      JsonReader reader,
      Type t,
      object existingValue,
      JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
        return (object) null;
      switch (serializer.Deserialize<string>(reader))
      {
        case "Free":
          return (object) WorldLayout.Free;
        case "GridVania":
          return (object) WorldLayout.GridVania;
        case "LinearHorizontal":
          return (object) WorldLayout.LinearHorizontal;
        case "LinearVertical":
          return (object) WorldLayout.LinearVertical;
        default:
          throw new Exception("Cannot unmarshal type WorldLayout");
      }
    }

    public override void WriteJson(
      JsonWriter writer,
      object untypedValue,
      JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, (object) null);
      }
      else
      {
        switch ((WorldLayout) untypedValue)
        {
          case WorldLayout.Free:
            serializer.Serialize(writer, (object) "Free");
            break;
          case WorldLayout.GridVania:
            serializer.Serialize(writer, (object) "GridVania");
            break;
          case WorldLayout.LinearHorizontal:
            serializer.Serialize(writer, (object) "LinearHorizontal");
            break;
          case WorldLayout.LinearVertical:
            serializer.Serialize(writer, (object) "LinearVertical");
            break;
          default:
            throw new Exception("Cannot marshal type WorldLayout");
        }
      }
    }
  }
}
