// Decompiled with JetBrains decompiler
// Type: MonolithEngine.MonolithTexture
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

#nullable disable
namespace MonolithEngine
{
  public class MonolithTexture
  {
    protected Texture2D texture;
    private Rectangle sourceRectangle;
    private Rectangle boundingBox;
    internal static GraphicsDevice GraphicsDevice;

    public MonolithTexture(Texture2D texture, Rectangle boundingBox, Rectangle sourceRectangle = default (Rectangle))
    {
      this.texture = texture;
      this.sourceRectangle = sourceRectangle == new Rectangle() ? this.TextureBorders() : sourceRectangle;
      this.boundingBox = boundingBox;
    }

    public MonolithTexture(Texture2D texture, bool autoBoundingBox = false, Rectangle sourceRectangle = default (Rectangle))
    {
      this.texture = texture;
      this.sourceRectangle = sourceRectangle == new Rectangle() ? this.TextureBorders() : sourceRectangle;
      this.boundingBox = autoBoundingBox ? AssetUtil.AutoBoundingBox(texture) : new Rectangle();
    }

    public Texture2D GetTexture2D() => this.texture;

    private Rectangle TextureBorders()
    {
        Rectangle rect = new Rectangle(0, 0, 1, 1); 
        //RnD
        try
        {
          //if (texture != null)
            rect = new Rectangle(0, 0, this.texture.Width, this.texture.Height);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] TextureBorder bug: " + ex.Message);
        }

        return rect;
    }

    public Rectangle GetSourceRectangle() => this.sourceRectangle;

    public Rectangle GetBoundingBox() => this.boundingBox;

    public void SetBoundingBox(Rectangle boundingBox) => this.boundingBox = boundingBox;

    public void SetSourceRectangle(Rectangle sourceRectangle)
    {
      this.sourceRectangle = sourceRectangle;
    }
  }
}
