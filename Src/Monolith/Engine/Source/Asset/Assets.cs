// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Assets
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class Assets
  {
    private static Dictionary<string, MonolithTexture> textures = new Dictionary<string, MonolithTexture>();
    private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

    public static void LoadTexture(
      string name,
      string path,
      bool flipVertical = false,
      bool flipHorizontal = false,
      bool autoBoundingBox = false)
    {
      if (flipVertical | flipHorizontal)
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.FlipTexture(AssetUtil.LoadTexture(path), flipVertical, flipHorizontal), autoBoundingBox));
      else
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.LoadTexture(path), autoBoundingBox));
    }

    public static void LoadTexture(
      string name,
      string path,
      Rectangle boundingBox,
      bool flipVertical = false,
      bool flipHorizontal = false)
    {
      if (flipVertical | flipHorizontal)
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.FlipTexture(AssetUtil.LoadTexture(path), flipVertical, flipHorizontal), boundingBox));
      else
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.LoadTexture(path), boundingBox));
    }

    public static void LoadAnimationTexture(
      string name,
      string path,
      int width = 0,
      int height = 0,
      int rows = 0,
      int columns = 0,
      int totalFrames = 0,
      bool flipVertical = false,
      bool flipHorizontal = false)
    {
      if (flipVertical | flipHorizontal)
        Assets.textures.Add(name, (MonolithTexture) new AnimationTexture(AssetUtil.FlipTexture(AssetUtil.LoadTexture(path), flipVertical, flipHorizontal), width, height, rows, columns, totalFrames));
      else
        Assets.textures.Add(name, (MonolithTexture) new AnimationTexture(AssetUtil.LoadTexture(path), width, height, rows, columns, totalFrames));
    }

    public static Texture2D GetTexture2D(string name) => Assets.textures[name].GetTexture2D();

    public static MonolithTexture GetTexture(string name) => Assets.textures[name];

    public static AnimationTexture GetAnimationTexture(string name)
    {
      return (AnimationTexture) Assets.textures[name];
    }

    public static Texture2D LoadAndGetTexture2D(string name, string path)
    {
      Assets.LoadTexture(name, path);
      return Assets.textures[name].GetTexture2D();
    }

    public static Texture2D CreateRectangle(int size, Color color)
    {
      return AssetUtil.CreateRectangle(size, color);
    }

    public static Texture2D CreateRectangle(int width, int height, Color color)
    {
      return AssetUtil.CreateRectangle(width, height, color);
    }

    public static Texture2D CreateCircle(int diameter, Color color, bool filled = false)
    {
      return AssetUtil.CreateCircle(diameter, color, filled);
    }

    public static void AddFont(string name, SpriteFont spriteFont)
    {
      Assets.fonts.Add(name, spriteFont);
    }

    public static SpriteFont GetFont(string name) => Assets.fonts[name];
  }
}
