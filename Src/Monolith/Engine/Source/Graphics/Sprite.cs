// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Sprite
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace MonolithEngine
{
  public class Sprite : IComponent, IDrawableComponent
  {
    public MonolithTexture Texture;
    public Vector2 DrawOffset;
    public Entity Owner;
    public SpriteEffects SpriteEffect;
    public float Rotation;
    public Vector2 Origin;
    private Vector2 offset = Vector2.Zero;
    public float Scale = 1f;
    public Color Color = Color.White;

    public bool UniquePerEntity { get; set; }

    public Microsoft.Xna.Framework.Rectangle SourceRectangle => this.Texture.GetSourceRectangle();

    public Sprite(
      Entity owner,
      MonolithTexture texture,
      Vector2 drawOffset = default (Vector2),
      float rotation = 0.0f,
      Vector2 origin = default (Vector2))
    {
      this.Texture = texture;
      this.UniquePerEntity = true;
      this.DrawOffset = drawOffset;
      this.Owner = owner;
      this.Rotation = rotation;
      this.Origin = origin;
      if (drawOffset == new Vector2())
        this.offset = new Vector2((float) this.Texture.GetSourceRectangle().Width * owner.Pivot.X, (float) this.Texture.GetSourceRectangle().Height * owner.Pivot.Y);
      else
        this.offset = drawOffset;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture.GetTexture2D(), this.Owner.DrawPosition - this.offset, new Microsoft.Xna.Framework.Rectangle?(this.Texture.GetSourceRectangle()), this.Color, this.Rotation, this.Origin, this.Scale, this.SpriteEffect, this.Owner.Depth);
    }

    public Type GetComponentType() => this.GetType();

    public static Texture2D Rectangle(int width, int height, Color color)
    {
      return AssetUtil.CreateRectangle(width, height, color);
    }

    public static Texture2D Square(int size, Color color)
    {
      return AssetUtil.CreateRectangle(size, size, color);
    }

    public static Texture2D Rectangle(int width, int height, Color[] color)
    {
      return AssetUtil.TextureFromColor(color, width, height);
    }

    public static Texture2D Square(int size, Color[] color)
    {
      return AssetUtil.TextureFromColor(color, size, size);
    }
  }
}
