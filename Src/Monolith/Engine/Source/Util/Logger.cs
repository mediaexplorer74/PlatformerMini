// Type: MonolithEngine.Logger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace MonolithEngine
{
  public class Logger
  {
    private static string ERROR = nameof (ERROR);
    private static string INFO = nameof (INFO);
    private static string DEBUG = nameof (DEBUG);
    private static string WARNING = nameof (WARNING);
    public static bool LogToFile = false;

    public static void Info(string message) => Logger.Log(Logger.INFO, message);

    public static void Debug(string message) => Logger.Log(Logger.DEBUG, message);

    public static void Warn(string message) => Logger.Log(Logger.WARNING, message);

    public static void Error(string message) => Logger.Log(Logger.ERROR, message);

    public static void Info(object toLog) => Logger.Log(Logger.INFO, toLog.ToString());

        public static void Debug(object toLog) =>
            //Logger.Log(Logger.DEBUG, toLog.ToString());
            System.Diagnostics.Debug.WriteLine("[debug]" + toLog.ToString());

        public static void Warn(object toLog) => Logger.Log(Logger.WARNING, toLog.ToString());

    public static void Error(object toLog) => Logger.Log(Logger.ERROR, toLog.ToString());

    private static void Log(string level, string message)
    {
      string message1 = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", (IFormatProvider) CultureInfo.InvariantCulture) + " [" + level + "]: " + message;
      if (level == Logger.DEBUG)
        return;
      System.Diagnostics.Debug.WriteLine(message1);
    }
  }
}
