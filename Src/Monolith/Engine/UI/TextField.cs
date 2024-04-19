
// Type: MonolithEngine.TextField
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonolithEngine
{
  public class TextField : AbstractUIElement
  {
    public SpriteFont Font;
    public Func<string> DataSource;
    public Color Color = Color.Black;
    public float Scale;
    public float Rotation;
    public float Depth;

    public TextField(
      SpriteFont font,
      Func<string> dataSource,
      Vector2 position,
      Color color = default (Color),
      float scale = 1f,
      float rotation = 0.0f,
      float depth = 1f)
      : base(position)
    {
      this.Font = font;
      this.DataSource = dataSource;
      if (color != new Color())
        this.Color = color;
      this.Rotation = rotation;
      this.Scale = scale;
      this.Depth = depth;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.Parent == null)
        spriteBatch.DrawString(this.Font, this.DataSource(), this.GetPosition(), this.Color, this.Rotation, Vector2.Zero, this.Scale, SpriteEffects.None, this.Depth);
      else
        spriteBatch.DrawString(this.Font, this.DataSource(), this.GetPosition(), this.Color, this.Rotation, Vector2.Zero, this.Scale, SpriteEffects.None, this.Depth);
    }
  }
}
