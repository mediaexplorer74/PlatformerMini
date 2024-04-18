// Decompiled with JetBrains decompiler
// Type: MonolithEngine.AssetUtil
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  internal class AssetUtil
  {
    public static GraphicsDeviceManager GraphicsDeviceManager;
    public static ContentManager Content;
    private static Dictionary<AssetUtil.RectangleKey, Texture2D> rectangleCache = new Dictionary<AssetUtil.RectangleKey, Texture2D>();
    private static Random random = new Random();

    public static Texture2D CreateCircle(int diameter, Color color, bool filled = false)
    {
      Texture2D circle = new Texture2D(AssetUtil.GraphicsDeviceManager.GraphicsDevice, diameter, diameter);
      Color[] data = new Color[diameter * diameter];
      float num1 = (float) diameter / 2f;
      float num2 = num1 * num1;
      for (int index1 = 0; index1 < diameter; ++index1)
      {
        for (int index2 = 0; index2 < diameter; ++index2)
        {
          int index3 = index1 * diameter + index2;
          Vector2 vector2 = new Vector2((float) index1 - num1, (float) index2 - num1);
          data[index3] = !filled ? ((double) vector2.LengthSquared() > (double) num2 || (double) vector2.LengthSquared() <= (double) num2 - 50.0 ? Color.Transparent : color) : ((double) vector2.LengthSquared() > (double) num2 ? Color.Transparent : color);
        }
      }
      circle.SetData<Color>(data);
      return circle;
    }

    public static Texture2D CreateRectangle(int size, Color color)
    {
      if (AssetUtil.rectangleCache.ContainsKey(new AssetUtil.RectangleKey(size, color)))
        return AssetUtil.rectangleCache[new AssetUtil.RectangleKey(size, color)];
      Texture2D rectangle = new Texture2D(AssetUtil.GraphicsDeviceManager.GraphicsDevice, size, size);
      Color[] data = new Color[size * size];
      for (int index = 0; index < data.Length; ++index)
        data[index] = color;
      rectangle.SetData<Color>(data);
      AssetUtil.rectangleCache.Add(new AssetUtil.RectangleKey(size, color), rectangle);
      return rectangle;
    }

    public static Texture2D CreateRectangle(int width, int height, Color color)
    {
      int size = 2 * width + height;
      if (AssetUtil.rectangleCache.ContainsKey(new AssetUtil.RectangleKey(size, color)))
        return AssetUtil.rectangleCache[new AssetUtil.RectangleKey(size, color)];
      Texture2D rectangle = new Texture2D(AssetUtil.GraphicsDeviceManager.GraphicsDevice, width, height);
      Color[] data = new Color[width * height];
      for (int index1 = 0; index1 < width; ++index1)
      {
        for (int index2 = 0; index2 < height; ++index2)
        {
          int index3 = index1 + width * index2;
          data[index3] = color;
        }
      }
      rectangle.SetData<Color>(data);
      AssetUtil.rectangleCache.Add(new AssetUtil.RectangleKey(size, color), rectangle);
      return rectangle;
    }

    public static List<Texture2D> LoadTextures(string fullPath, int frameCount)
    {
      return AssetUtil.LoadTextures(fullPath, 0, frameCount);
    }

    public static Texture2D FlipTexture(Texture2D input, bool vertical, bool horizontal)
    {
      if (MonolithGame.IsGameStarted)
        throw new Exception("iOS compatibility bug: method with Texture2D.GetData() function called from game loop! Please move the call to the game's initailization section!");
      Texture2D texture2D = new Texture2D(AssetUtil.GraphicsDeviceManager.GraphicsDevice, input.Width, input.Height);
      Color[] data1 = new Color[input.Width * input.Height];
      Color[] data2 = new Color[data1.Length];
      input.GetData<Color>(data1);
      for (int index1 = 0; index1 < input.Width; ++index1)
      {
        for (int index2 = 0; index2 < input.Height; ++index2)
        {
          int index3 = 0;
          if (horizontal & vertical)
            index3 = input.Width - 1 - index1 + (input.Height - 1 - index2) * input.Width;
          else if (horizontal && !vertical)
            index3 = input.Width - 1 - index1 + index2 * input.Width;
          else if (!horizontal & vertical)
            index3 = index1 + (input.Height - 1 - index2) * input.Width;
          else if (!horizontal && !vertical)
            index3 = index1 + index2 * input.Width;
          data2[index1 + index2 * input.Width] = data1[index3];
        }
      }
      texture2D.SetData<Color>(data2);
      return texture2D;
    }

    public static List<Texture2D> LoadTextures(string fullPath, int startFrame, int endFrame)
    {
      List<Texture2D> texture2DList = new List<Texture2D>();
      for (int index = startFrame; index <= endFrame; ++index)
        texture2DList.Add(AssetUtil.Content.Load<Texture2D>(fullPath + index.ToString()));
      return texture2DList;
    }

    public static Color[] GetPixels(Color[] allPixels, Rectangle targetArea, int textureWidth)
    {
      Color[] pixels = new Color[targetArea.Width * targetArea.Height];
      int num = 0;
      for (int y = targetArea.Y; y < targetArea.Y + targetArea.Height; ++y)
      {
        for (int x = targetArea.X; x < targetArea.X + targetArea.Width; ++x)
        {
          int index = y * textureWidth + x;
          pixels[num++] = allPixels[index];
        }
      }
      return pixels;
    }

    public static Texture2D LoadTexture(string path) => AssetUtil.Content.Load<Texture2D>(path);

    public static Color GetRandomColor()
    {
      return Color.FromNonPremultiplied(AssetUtil.random.Next(0, 256), AssetUtil.random.Next(0, 256), AssetUtil.random.Next(0, 256), 256);
    }

    public static SoundEffect LoadSoundEffect(string path)
    {
      return AssetUtil.Content.Load<SoundEffect>(path);
    }

    public static Song LoadSong(string path) => AssetUtil.Content.Load<Song>(path);

    public static Rectangle AutoBoundingBox(Color[] inputImage, int imageWidth)
    {
      int x = int.MaxValue;
      int y = int.MaxValue;
      int num1 = int.MinValue;
      int num2 = int.MinValue;
      for (int index = 0; index < inputImage.Length; ++index)
      {
        if (inputImage[index].ToVector4() != Vector4.Zero)
        {
          int num3 = index % imageWidth;
          int num4 = index / imageWidth;
          if (num3 < x)
            x = num3;
          if (num3 > num1)
            num1 = num3;
          if (num4 < y)
            y = num4;
          if (num4 > num2)
            num2 = num4;
        }
      }
      return new Rectangle(x, y, num1 - x + 1, num2 - y + 1);
    }

    public static Rectangle AutoBoundingBox(Texture2D texture)
    {
      if (MonolithGame.IsGameStarted)
        throw new Exception("iOS compatibility bug: method with Texture2D.GetData() function called from game loop! Please move the call to the game's initailization section!");
      int width = texture.Width;
      int height = texture.Height;
      Color[] colorArray = new Color[width * height];
      texture.GetData<Color>(0, new Rectangle?(new Rectangle(0, 0, width, height)), colorArray, 0, colorArray.Length);
      return AssetUtil.AutoBoundingBox(colorArray, width);
    }

    public static Rectangle AutoBoundingBox(Sprite sprite)
    {
      return AssetUtil.AutoBoundingBox(sprite.Texture.GetTexture2D());
    }

    public static float AutoCircumscribedCircle(
      AbstractAnimation anim,
      AssetUtil.BoundCalculationMode calculationMode = AssetUtil.BoundCalculationMode.MAX)
    {
      return AssetUtil.CircleFromRectangle(AssetUtil.AutoBoundingBox(anim, calculationMode), calculationMode);
    }

    public static float AutoCircumscribedCircle(
      Texture2D texture,
      AssetUtil.BoundCalculationMode calculationMode = AssetUtil.BoundCalculationMode.MAX)
    {
      return AssetUtil.CircleFromRectangle(AssetUtil.AutoBoundingBox(texture), calculationMode);
    }

    private static float CircleFromRectangle(
      Rectangle rect,
      AssetUtil.BoundCalculationMode calculationMode)
    {
      if (calculationMode == AssetUtil.BoundCalculationMode.MAX)
        return (float) Math.Max(rect.Width, rect.Height);
      return calculationMode == AssetUtil.BoundCalculationMode.MIN ? (float) Math.Min(rect.Width, rect.Height) : (float) ((rect.Width + rect.Height) / 2);
    }

    public static float AutoCircumscribedCircle(
      Sprite sprite,
      AssetUtil.BoundCalculationMode calculationMode)
    {
      return AssetUtil.AutoCircumscribedCircle(sprite.Texture.GetTexture2D(), calculationMode);
    }

    public static Rectangle AutoBoundingBox(
      AbstractAnimation anim,
      AssetUtil.BoundCalculationMode calculationMode = AssetUtil.BoundCalculationMode.MAX)
    {
      List<Rectangle> rectangleList = new List<Rectangle>();
      anim.Init();
      for (; anim.CurrentFrame < anim.EndFrame; ++anim.CurrentFrame)
        rectangleList.Add(AssetUtil.AutoBoundingBox(AssetUtil.GetTextureArea(anim.GetTexture(), anim.SourceRectangle), anim.SourceRectangle.Width));
      anim.Init();
      Rectangle rectangle1;
      switch (calculationMode)
      {
        case AssetUtil.BoundCalculationMode.MAX:
          rectangle1 = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
          using (List<Rectangle>.Enumerator enumerator = rectangleList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Rectangle current = enumerator.Current;
              if (current.X < rectangle1.X)
                rectangle1.X = current.X;
              if (current.Y < rectangle1.Y)
                rectangle1.Y = current.Y;
              if (current.Width > rectangle1.Width)
                rectangle1.Width = current.Width;
              if (current.Height > rectangle1.Height)
                rectangle1.Height = current.Height;
            }
            break;
          }
        case AssetUtil.BoundCalculationMode.MIN:
          rectangle1 = new Rectangle(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);
          using (List<Rectangle>.Enumerator enumerator = rectangleList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Rectangle current = enumerator.Current;
              if (current.X > rectangle1.X)
                rectangle1.X = current.X;
              if (current.Y > rectangle1.Y)
                rectangle1.Y = current.Y;
              if (current.Width < rectangle1.Width)
                rectangle1.Width = current.Width;
              if (current.Height < rectangle1.Height)
                rectangle1.Height = current.Height;
            }
            break;
          }
        default:
          rectangle1 = new Rectangle(0, 0, 0, 0);
          foreach (Rectangle rectangle2 in rectangleList)
          {
            rectangle1.X += rectangle2.X;
            rectangle1.Y += rectangle2.Y;
            rectangle1.Width += rectangle2.Width;
            rectangle1.Height += rectangle2.Height;
          }
          rectangle1.X = (int) Math.Round((Decimal) rectangle1.X / (Decimal) rectangleList.Count);
          rectangle1.Y = (int) Math.Round((Decimal) rectangle1.Y / (Decimal) rectangleList.Count);
          rectangle1.Width = (int) Math.Round((Decimal) rectangle1.Width / (Decimal) rectangleList.Count);
          rectangle1.Height = (int) Math.Round((Decimal) rectangle1.Height / (Decimal) rectangleList.Count);
          break;
      }
      return rectangle1;
    }

    public static Color[] GetTextureArea(Texture2D texture, Rectangle rectangle)
    {
      if (MonolithGame.IsGameStarted)
        throw new Exception("iOS compatibility bug: method with Texture2D.GetData() function called from game loop! Please move the call to the game's initailization section!");
      int width = texture.Width;
      int height = texture.Height;
      Color[] data = new Color[width * height];
      texture.GetData<Color>(0, new Rectangle?(new Rectangle(0, 0, width, height)), data, 0, data.Length);
      Color[] textureArea = new Color[rectangle.Width * rectangle.Height];
      for (int index1 = 0; index1 < rectangle.Width; ++index1)
      {
        for (int index2 = 0; index2 < rectangle.Height; ++index2)
          textureArea[index1 + index2 * rectangle.Width] = data[index1 + rectangle.X + (index2 + rectangle.Y) * width];
      }
      return textureArea;
    }

    public static Texture2D TextureFromColor(Color[] input, int width, int height)
    {
      Texture2D texture2D = new Texture2D(AssetUtil.GraphicsDeviceManager.GraphicsDevice, width, height);
      texture2D.SetData<Color>(input);
      return texture2D;
    }

    private class RectangleKey
    {
      public int size;
      public Color color;

      public RectangleKey(int size, Color color)
      {
        this.size = size;
        this.color = color;
      }

      public override bool Equals(object obj)
      {
        return obj is AssetUtil.RectangleKey rectangleKey && this.size == rectangleKey.size && this.color.Equals(rectangleKey.color);
      }

      public override int GetHashCode()
      {
        return HashCode.Combine<int, Color>(this.size, this.color);
      }
    }

    public enum BoundCalculationMode
    {
      MAX,
      MIN,
      AVERAGE,
    }
  }
}
