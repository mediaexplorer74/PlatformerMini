// Decompiled with JetBrains decompiler
// Type: MonolithEngine.SpriteGroupAnimation
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public class SpriteGroupAnimation : AbstractAnimation
  {
    public List<Texture2D> Textures;

    public SpriteGroupAnimation(
      Entity parent,
      List<Texture2D> textures,
      int framerate = 0,
      SpriteEffects spriteEffect = SpriteEffects.None)
      : base(parent, textures.Count, framerate, spriteEffect)
    {
      this.Textures = textures;
    }

    public SpriteGroupAnimation Copy()
    {
      SpriteGroupAnimation anim = new SpriteGroupAnimation(this.Parent, (List<Texture2D>) null, spriteEffect: this.SpriteEffect)
      {
        Textures = this.Textures
      };
      this.Copy((AbstractAnimation) anim);
      return anim;
    }

    public SpriteGroupAnimation CopyFlipped()
    {
      SpriteGroupAnimation spriteGroupAnimation = this.Copy();
      spriteGroupAnimation.Flip();
      return spriteGroupAnimation;
    }

    internal override Texture2D GetTexture()
    {
      this.Origin = new Vector2((float) Math.Floor((Decimal) this.Textures[this.CurrentFrame].Width / 2M), (float) Math.Floor((Decimal) this.Textures[this.CurrentFrame].Height / 2M));
      this.SourceRectangle = new Rectangle(0, 0, this.Textures[this.CurrentFrame].Width, this.Textures[this.CurrentFrame].Height);
      return this.Textures[this.CurrentFrame];
    }

    public override void Destroy() => this.Textures = (List<Texture2D>) null;
  }
}
