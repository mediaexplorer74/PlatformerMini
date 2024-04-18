// Decompiled with JetBrains decompiler
// Type: MonolithEngine.TextLangageModeConverter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using System;


namespace MonolithEngine
{
  internal class TextLangageModeConverter : JsonConverter
  {
    public static readonly TextLangageModeConverter Singleton = new TextLangageModeConverter();

    public override bool CanConvert(Type t)
    {
      return t == typeof (TextLangageMode) || t == typeof (TextLangageMode?);
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
        case "LangC":
          return (object) TextLangageMode.LangC;
        case "LangHaxe":
          return (object) TextLangageMode.LangHaxe;
        case "LangJS":
          return (object) TextLangageMode.LangJs;
        case "LangJson":
          return (object) TextLangageMode.LangJson;
        case "LangLua":
          return (object) TextLangageMode.LangLua;
        case "LangMarkdown":
          return (object) TextLangageMode.LangMarkdown;
        case "LangPython":
          return (object) TextLangageMode.LangPython;
        case "LangRuby":
          return (object) TextLangageMode.LangRuby;
        case "LangXml":
          return (object) TextLangageMode.LangXml;
        default:
          throw new Exception("Cannot unmarshal type TextLangageMode");
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
        switch ((TextLangageMode) untypedValue)
        {
          case TextLangageMode.LangC:
            serializer.Serialize(writer, (object) "LangC");
            break;
          case TextLangageMode.LangHaxe:
            serializer.Serialize(writer, (object) "LangHaxe");
            break;
          case TextLangageMode.LangJs:
            serializer.Serialize(writer, (object) "LangJS");
            break;
          case TextLangageMode.LangJson:
            serializer.Serialize(writer, (object) "LangJson");
            break;
          case TextLangageMode.LangLua:
            serializer.Serialize(writer, (object) "LangLua");
            break;
          case TextLangageMode.LangMarkdown:
            serializer.Serialize(writer, (object) "LangMarkdown");
            break;
          case TextLangageMode.LangPython:
            serializer.Serialize(writer, (object) "LangPython");
            break;
          case TextLangageMode.LangRuby:
            serializer.Serialize(writer, (object) "LangRuby");
            break;
          case TextLangageMode.LangXml:
            serializer.Serialize(writer, (object) "LangXml");
            break;
          default:
            throw new Exception("Cannot marshal type TextLangageMode");
        }
      }
    }
  }
}
