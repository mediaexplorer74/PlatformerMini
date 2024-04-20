// Type: MonolithEngine.Assets

// This class provides methods for loading and accessing various assets in the game.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace MonolithEngine
{
    // This class provides methods for loading and accessing various assets in the game.

    public class Assets
  {
    private static Dictionary<string, MonolithTexture> textures = new Dictionary<string, MonolithTexture>();
    private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

    // Method: LoadTexture
    // Loads a texture from the specified file path.
    public static void LoadTexture(
      string name,
      string path,
      bool flipVertical = false,
      bool flipHorizontal = false,
      bool autoBoundingBox = false)
    {
      if (flipVertical | flipHorizontal)
        Assets.textures.Add(name, new MonolithTexture(
            AssetUtil.FlipTexture(AssetUtil.LoadTexture(path), 
            flipVertical, flipHorizontal), autoBoundingBox));
      else
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.LoadTexture(path), autoBoundingBox));
    }

    // Method: LoadTexture
    // Loads a texture from the specified file path and creates a Texture2D object.
    public static void LoadTexture(
      string name,
      string path,
      Rectangle boundingBox,
      bool flipVertical = false,
      bool flipHorizontal = false)
    {
      if (flipVertical | flipHorizontal)
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.FlipTexture(
            AssetUtil.LoadTexture(path), flipVertical, flipHorizontal), boundingBox));
      else
        Assets.textures.Add(name, new MonolithTexture(AssetUtil.LoadTexture(path), boundingBox));
    }

    // Method: LoadAnimationTexture
    // Loads an animation texture from the specified file path and creates a Texture2D object.
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
        Assets.textures.Add(name, (MonolithTexture) new AnimationTexture(
            AssetUtil.FlipTexture(AssetUtil.LoadTexture(path), flipVertical, flipHorizontal),
            width, height, rows, columns, totalFrames));
      else
        Assets.textures.Add(name, (MonolithTexture) new AnimationTexture(
            AssetUtil.LoadTexture(path), width, height, rows, columns, totalFrames));
    }

    // Method: GetTexture2D
    // Retrieves a Texture2D object from the cache, given a texture path.
    public static Texture2D GetTexture2D(string name)
    {
        return Assets.textures[name].GetTexture2D();
    }

    // Method: GetTexture
    // Retrieves a Texture object from the cache, given a texture path.
    public static MonolithTexture GetTexture(string name)
    {
        return Assets.textures[name];
    }

    // Method: GetAnimationTexture
    // Retrieves an AnimationTexture object from the cache, given a texture path.
    public static AnimationTexture GetAnimationTexture(string name)
    {
      return (AnimationTexture) Assets.textures[name];
    }

    // Method: LoadAndGetTexture2D
    // Loads a texture from the specified file path and returns a Texture2D object.
    public static Texture2D LoadAndGetTexture2D(string name, string path)
    {
      Assets.LoadTexture(name, path);
      return Assets.textures[name].GetTexture2D();
    }

    public static Texture2D CreateRectangle(int size, Color color)
    {
      return AssetUtil.CreateRectangle(size, color);
    }

    // Method: CreateRectangle
    // Creates a rectangle shape with the specified dimensions and color.
    public static Texture2D CreateRectangle(int width, int height, Color color)
    {
      return AssetUtil.CreateRectangle(width, height, color);
    }

    // Method: CreateCircle
    // Creates a circle shape with the specified radius and color.
    public static Texture2D CreateCircle(int diameter, Color color, bool filled = false)
    {
      return AssetUtil.CreateCircle(diameter, color, filled);
    }

    // Method: AddFont
    // Add font with the specified name and sprite font.
    public static void AddFont(string name, SpriteFont spriteFont)
    {
      Assets.fonts.Add(name, spriteFont);
    }

    // Method: GetFont
    // Get sprite font from the specified name.
    public static SpriteFont GetFont(string name) => Assets.fonts[name];
  }
}
