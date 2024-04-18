// Decompiled with JetBrains decompiler
// Type: MonolithEngine.BgPosConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace MonolithEngine
{
  internal class BgPosConverter : JsonConverter
  {
    public static readonly BgPosConverter Singleton = new BgPosConverter();

    public override bool CanConvert(Type t) => t == typeof (BgPos) || t == typeof (BgPos?);

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
        case "Contain":
          return (object) BgPos.Contain;
        case "Cover":
          return (object) BgPos.Cover;
        case "CoverDirty":
          return (object) BgPos.CoverDirty;
        case "Unscaled":
          return (object) BgPos.Unscaled;
        default:
          throw new Exception("Cannot unmarshal type BgPos");
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
        switch ((BgPos) untypedValue)
        {
          case BgPos.Contain:
            serializer.Serialize(writer, (object) "Contain");
            break;
          case BgPos.Cover:
            serializer.Serialize(writer, (object) "Cover");
            break;
          case BgPos.CoverDirty:
            serializer.Serialize(writer, (object) "CoverDirty");
            break;
          case BgPos.Unscaled:
            serializer.Serialize(writer, (object) "Unscaled");
            break;
          default:
            throw new Exception("Cannot marshal type BgPos");
        }
      }
    }
  }
}
