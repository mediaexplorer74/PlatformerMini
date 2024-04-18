// Decompiled with JetBrains decompiler
// Type: MonolithEngine.AnimationTexture
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonolithEngine
{
  public class AnimationTexture : MonolithTexture
  {
    internal int Rows;
    internal int Columns;
    internal int Width;
    internal int Height;
    private int frameCount;

    public int FrameCount => this.frameCount;

    public AnimationTexture(
      Texture2D texture,
      int width = 0,
      int height = 0,
      int rows = 0,
      int columns = 0,
      int totalFrames = 0)
      : base(texture)
    {
      if (width == 0 && height == 0)
      {
        this.Width = this.Height = this.GetFrameSize();
      }
      else
      {
        this.Width = width;
        this.Height = height;
      }
      this.Rows = rows == 0 ? texture.Height / this.Height : rows;
      this.Columns = columns == 0 ? texture.Width / this.Width : columns;
      if (totalFrames == 0)
      {
        if (MonolithGame.IsGameStarted)
          throw new Exception("iOS compatibility bug: method with Texture2D.GetData() function called from game loop! Please move the call to the game's initailization section!");
        RenderTarget2D renderTarget = new RenderTarget2D(MonolithTexture.GraphicsDevice, texture.Width, texture.Height, false, MonolithTexture.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
        MonolithTexture.GraphicsDevice.SetRenderTarget(renderTarget);
        MonolithTexture.GraphicsDevice.Clear(Color.Transparent);
        SpriteBatch spriteBatch = new SpriteBatch(MonolithTexture.GraphicsDevice);
        spriteBatch.Begin();
        spriteBatch.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        spriteBatch.End();
        MonolithTexture.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
        Color[] colorArray = new Color[renderTarget.Width * renderTarget.Height];
        renderTarget.GetData<Color>(colorArray);
        this.frameCount = this.GetFrameCount(colorArray, this.Rows, this.Columns);
      }
      else
        this.frameCount = totalFrames;
    }

    private int GetFrameCount(Color[] allPixels, int rows, int columns)
    {
      int frameCount = 0;
      for (int index1 = 0; index1 < rows; ++index1)
      {
        for (int index2 = 0; index2 < columns; ++index2)
        {
          Color[] pixels = AssetUtil.GetPixels(allPixels, new Rectangle(index2 * this.Width, index1 * this.Height, this.Width, this.Height), this.texture.Width);
          bool flag = true;
          for (int index3 = 0; index3 < this.Width * this.Height; ++index3)
          {
            if (pixels[index3].ToVector4() != Vector4.Zero)
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return frameCount;
          ++frameCount;
        }
      }
      return frameCount;
    }

    private int GetFrameSize()
    {
      int d = Math.Max(this.texture.Width, this.texture.Height);
      int num1 = 0;
      for (int y = 1; (double) y <= Math.Log((double) d); ++y)
      {
        int num2 = (int) Math.Pow(2.0, (double) y);
        if (this.texture.Width % num2 == 0 && this.texture.Height % num2 == 0)
          num1 = num2;
      }
      return num1 != 0 ? num1 : throw new Exception("Can't determine frame size, the image dimensions are not the multiples of power of 2");
    }
  }
}
