
// Type: MonolithEngine.VideoConfiguration
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll


namespace MonolithEngine
{
  public class VideoConfiguration
  {
    internal static MonolithGame GameInstance;
    public static int RESOLUTION_WIDTH = 0;
    public static int RESOLUTION_HEIGHT = 0;
    public static bool VSYNC = true;
    public static int FRAME_LIMIT = 0;
    public static bool FULLSCREEN = true;

    public static void Apply()
    {
        VideoConfiguration.GameInstance.ApplyVideoConfiguration();
    }
  }
}
