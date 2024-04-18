// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LimitScopeConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace MonolithEngine
{
  internal class LimitScopeConverter : JsonConverter
  {
    public static readonly LimitScopeConverter Singleton = new LimitScopeConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (LimitScope) || t == typeof (LimitScope?);
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
        case "PerLayer":
          return (object) LimitScope.PerLayer;
        case "PerLevel":
          return (object) LimitScope.PerLevel;
        case "PerWorld":
          return (object) LimitScope.PerWorld;
        default:
          throw new Exception("Cannot unmarshal type LimitScope");
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
        switch ((LimitScope) untypedValue)
        {
          case LimitScope.PerLayer:
            serializer.Serialize(writer, (object) "PerLayer");
            break;
          case LimitScope.PerLevel:
            serializer.Serialize(writer, (object) "PerLevel");
            break;
          case LimitScope.PerWorld:
            serializer.Serialize(writer, (object) "PerWorld");
            break;
          default:
            throw new Exception("Cannot marshal type LimitScope");
        }
      }
    }
  }
}
