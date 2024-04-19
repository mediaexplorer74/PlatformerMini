
// Type: MonolithEngine.Converter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;


namespace MonolithEngine
{
  internal static class Converter
  {
    public static readonly JsonSerializerSettings Settings;

    static Converter()
    {
      JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
      serializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
      serializerSettings.DateParseHandling = DateParseHandling.None;
      serializerSettings.Converters.Add((JsonConverter) EditorDisplayModeConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) EditorDisplayPosConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) TextLangageModeConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) LimitBehaviorConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) LimitScopeConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) RenderModeConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) TileRenderModeConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) TypeEnumConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) FlagConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) BgPosConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) WorldLayoutConverter.Singleton);
      serializerSettings.Converters.Add((JsonConverter) new IsoDateTimeConverter()
      {
        DateTimeStyles = DateTimeStyles.AssumeUniversal
      });
      Converter.Settings = serializerSettings;
    }
  }
}
