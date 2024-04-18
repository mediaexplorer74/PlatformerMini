// Decompiled with JetBrains decompiler
// Type: MonolithEngine.RenderModeConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace MonolithEngine
{
  internal class RenderModeConverter : JsonConverter
  {
    public static readonly RenderModeConverter Singleton = new RenderModeConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (RenderMode) || t == typeof (RenderMode?);
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
        case "Cross":
          return (object) RenderMode.Cross;
        case "Ellipse":
          return (object) RenderMode.Ellipse;
        case "Rectangle":
          return (object) RenderMode.Rectangle;
        case "Tile":
          return (object) RenderMode.Tile;
        default:
          throw new Exception("Cannot unmarshal type RenderMode");
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
        switch ((RenderMode) untypedValue)
        {
          case RenderMode.Cross:
            serializer.Serialize(writer, (object) "Cross");
            break;
          case RenderMode.Ellipse:
            serializer.Serialize(writer, (object) "Ellipse");
            break;
          case RenderMode.Rectangle:
            serializer.Serialize(writer, (object) "Rectangle");
            break;
          case RenderMode.Tile:
            serializer.Serialize(writer, (object) "Tile");
            break;
          default:
            throw new Exception("Cannot marshal type RenderMode");
        }
      }
    }
  }
}
