
// Type: MonolithEngine.EditorDisplayModeConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class EditorDisplayModeConverter : JsonConverter
  {
    public static readonly EditorDisplayModeConverter Singleton = new EditorDisplayModeConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (EditorDisplayMode) || t == typeof (EditorDisplayMode?);
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
        case "EntityTile":
          return (object) EditorDisplayMode.EntityTile;
        case "Hidden":
          return (object) EditorDisplayMode.Hidden;
        case "NameAndValue":
          return (object) EditorDisplayMode.NameAndValue;
        case "PointPath":
          return (object) EditorDisplayMode.PointPath;
        case "PointStar":
          return (object) EditorDisplayMode.PointStar;
        case "RadiusGrid":
          return (object) EditorDisplayMode.RadiusGrid;
        case "RadiusPx":
          return (object) EditorDisplayMode.RadiusPx;
        case "ValueOnly":
          return (object) EditorDisplayMode.ValueOnly;
        default:
          throw new Exception("Cannot unmarshal type EditorDisplayMode");
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
        switch ((EditorDisplayMode) untypedValue)
        {
          case EditorDisplayMode.EntityTile:
            serializer.Serialize(writer, (object) "EntityTile");
            break;
          case EditorDisplayMode.Hidden:
            serializer.Serialize(writer, (object) "Hidden");
            break;
          case EditorDisplayMode.NameAndValue:
            serializer.Serialize(writer, (object) "NameAndValue");
            break;
          case EditorDisplayMode.PointPath:
            serializer.Serialize(writer, (object) "PointPath");
            break;
          case EditorDisplayMode.PointStar:
            serializer.Serialize(writer, (object) "PointStar");
            break;
          case EditorDisplayMode.RadiusGrid:
            serializer.Serialize(writer, (object) "RadiusGrid");
            break;
          case EditorDisplayMode.RadiusPx:
            serializer.Serialize(writer, (object) "RadiusPx");
            break;
          case EditorDisplayMode.ValueOnly:
            serializer.Serialize(writer, (object) "ValueOnly");
            break;
          default:
            throw new Exception("Cannot marshal type EditorDisplayMode");
        }
      }
    }
  }
}
