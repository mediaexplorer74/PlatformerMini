// Decompiled with JetBrains decompiler
// Type: MonolithEngine.FlagConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class FlagConverter : JsonConverter
  {
    public static readonly FlagConverter Singleton = new FlagConverter();

    public override bool CanConvert(Type t) => t == typeof (Flag) || t == typeof (Flag?);

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
        case "DiscardPreCsvIntGrid":
          return (object) Flag.DiscardPreCsvIntGrid;
        case "IgnoreBackupSuggest":
          return (object) Flag.IgnoreBackupSuggest;
        default:
          throw new Exception("Cannot unmarshal type Flag");
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
        switch ((Flag) untypedValue)
        {
          case Flag.DiscardPreCsvIntGrid:
            serializer.Serialize(writer, (object) "DiscardPreCsvIntGrid");
            break;
          case Flag.IgnoreBackupSuggest:
            serializer.Serialize(writer, (object) "IgnoreBackupSuggest");
            break;
          default:
            throw new Exception("Cannot marshal type Flag");
        }
      }
    }
  }
}
