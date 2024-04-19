
// Type: MonolithEngine.LimitBehaviorConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class LimitBehaviorConverter : JsonConverter
  {
    public static readonly LimitBehaviorConverter Singleton = new LimitBehaviorConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (LimitBehavior) || t == typeof (LimitBehavior?);
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
        case "DiscardOldOnes":
          return (object) LimitBehavior.DiscardOldOnes;
        case "MoveLastOne":
          return (object) LimitBehavior.MoveLastOne;
        case "PreventAdding":
          return (object) LimitBehavior.PreventAdding;
        default:
          throw new Exception("Cannot unmarshal type LimitBehavior");
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
        switch ((LimitBehavior) untypedValue)
        {
          case LimitBehavior.DiscardOldOnes:
            serializer.Serialize(writer, (object) "DiscardOldOnes");
            break;
          case LimitBehavior.MoveLastOne:
            serializer.Serialize(writer, (object) "MoveLastOne");
            break;
          case LimitBehavior.PreventAdding:
            serializer.Serialize(writer, (object) "PreventAdding");
            break;
          default:
            throw new Exception("Cannot marshal type LimitBehavior");
        }
      }
    }
  }
}
