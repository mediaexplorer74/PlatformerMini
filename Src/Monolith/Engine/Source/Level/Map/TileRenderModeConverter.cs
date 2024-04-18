// Decompiled with JetBrains decompiler
// Type: MonolithEngine.TileRenderModeConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace MonolithEngine
{
  internal class TileRenderModeConverter : JsonConverter
  {
    public static readonly TileRenderModeConverter Singleton = new TileRenderModeConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (TileRenderMode) || t == typeof (TileRenderMode?);
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
        case "Cover":
          return (object) TileRenderMode.Cover;
        case "FitInside":
          return (object) TileRenderMode.FitInside;
        case "Repeat":
          return (object) TileRenderMode.Repeat;
        case "Stretch":
          return (object) TileRenderMode.Stretch;
        default:
          throw new Exception("Cannot unmarshal type TileRenderMode");
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
        switch ((TileRenderMode) untypedValue)
        {
          case TileRenderMode.Cover:
            serializer.Serialize(writer, (object) "Cover");
            break;
          case TileRenderMode.FitInside:
            serializer.Serialize(writer, (object) "FitInside");
            break;
          case TileRenderMode.Repeat:
            serializer.Serialize(writer, (object) "Repeat");
            break;
          case TileRenderMode.Stretch:
            serializer.Serialize(writer, (object) "Stretch");
            break;
          default:
            throw new Exception("Cannot marshal type TileRenderMode");
        }
      }
    }
  }
}
