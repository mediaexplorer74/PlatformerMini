// Decompiled with JetBrains decompiler
// Type: MonolithEngine.EditorDisplayPosConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace MonolithEngine
{
  internal class EditorDisplayPosConverter : JsonConverter
  {
    public static readonly EditorDisplayPosConverter Singleton = new EditorDisplayPosConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (EditorDisplayPos) || t == typeof (EditorDisplayPos?);
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
        case "Above":
          return (object) EditorDisplayPos.Above;
        case "Beneath":
          return (object) EditorDisplayPos.Beneath;
        case "Center":
          return (object) EditorDisplayPos.Center;
        default:
          throw new Exception("Cannot unmarshal type EditorDisplayPos");
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
        switch ((EditorDisplayPos) untypedValue)
        {
          case EditorDisplayPos.Above:
            serializer.Serialize(writer, (object) "Above");
            break;
          case EditorDisplayPos.Beneath:
            serializer.Serialize(writer, (object) "Beneath");
            break;
          case EditorDisplayPos.Center:
            serializer.Serialize(writer, (object) "Center");
            break;
          default:
            throw new Exception("Cannot marshal type EditorDisplayPos");
        }
      }
    }
  }
}
