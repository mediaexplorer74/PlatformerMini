
// Type: MonolithEngine.SpriteSheetAnimation
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class SpriteSheetAnimation : AbstractAnimation
  {
    private AnimationTexture texture;
    private int currentRow;
    private int currentColumn;
    private Dictionary<int, Rectangle> sourceRectangles = new Dictionary<int, Rectangle>();

    public SpriteSheetAnimation(
      Entity parent,
      AnimationTexture texture,
      int framerate = 0,
      SpriteEffects spriteEffect = SpriteEffects.None)
      : base(parent, 0, framerate, spriteEffect)
    {
      this.texture = texture;
      this.TotalFrames = texture.FrameCount;
      this.SetupSourceRectangles();
    }

    private SpriteSheetAnimation Copy()
    {
      SpriteSheetAnimation anim = new SpriteSheetAnimation(this.Parent, this.texture, this.Framerate, this.SpriteEffect)
      {
        texture = this.texture
      };
      this.Copy((AbstractAnimation) anim);
      return anim;
    }

    private void SetupSourceRectangles()
    {
      for (int startFrame = this.StartFrame; startFrame <= this.EndFrame; ++startFrame)
      {
        this.currentRow = (int) ((double) startFrame / (double) this.texture.Columns);
        this.currentColumn = startFrame % this.texture.Columns;
        this.sourceRectangles.Add(startFrame, new Rectangle(this.texture.Width * this.currentColumn, this.texture.Height * this.currentRow, this.texture.Width, this.texture.Height));
      }
    }

    public SpriteSheetAnimation CopyFlipped()
    {
      SpriteSheetAnimation spriteSheetAnimation = this.Copy();
      spriteSheetAnimation.Flip();
      return spriteSheetAnimation;
    }

    internal override Texture2D GetTexture()
    {
      this.SourceRectangle = this.sourceRectangles[this.CurrentFrame];
      this.Origin = new Vector2((float) (this.texture.Width / 2), (float) (this.texture.Height / 2));
      return this.texture.GetTexture2D();
    }

    public override void Destroy() => this.texture = (AnimationTexture) null;
  }
}
