
// Type: MonolithEngine.TypeEnumConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class TypeEnumConverter : JsonConverter
  {
    public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();

    public override bool CanConvert(Type t) => t == typeof (TypeEnum) || t == typeof (TypeEnum?);

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
        case "AutoLayer":
          return (object) TypeEnum.AutoLayer;
        case "Entities":
          return (object) TypeEnum.Entities;
        case "IntGrid":
          return (object) TypeEnum.IntGrid;
        case "Tiles":
          return (object) TypeEnum.Tiles;
        default:
          throw new Exception("Cannot unmarshal type TypeEnum");
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
        switch ((TypeEnum) untypedValue)
        {
          case TypeEnum.AutoLayer:
            serializer.Serialize(writer, (object) "AutoLayer");
            break;
          case TypeEnum.Entities:
            serializer.Serialize(writer, (object) "Entities");
            break;
          case TypeEnum.IntGrid:
            serializer.Serialize(writer, (object) "IntGrid");
            break;
          case TypeEnum.Tiles:
            serializer.Serialize(writer, (object) "Tiles");
            break;
          default:
            throw new Exception("Cannot marshal type TypeEnum");
        }
      }
    }
  }
}
