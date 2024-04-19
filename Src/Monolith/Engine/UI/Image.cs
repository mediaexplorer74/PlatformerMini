
// Type: MonolithEngine.Image
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonolithEngine
{
  public class Image : AbstractUIElement
  {
    public Rectangle SourceRectangle;
    public float Scale = 1f;
    public float Rotation;
    public Texture2D ImageTexture;
    public SpriteEffects SpriteEffect;
    public int Depth = 1;
    public Color Color = Color.White;

    public Image(
      Texture2D texture,
      Vector2 position = default (Vector2),
      Rectangle sourceRectangle = default (Rectangle),
      float scale = 1f,
      float rotation = 0.0f,
      int depth = 1,
      Color color = default (Color))
      : base(position)
    {
      this.ImageTexture = texture;
      if (sourceRectangle == new Rectangle() && this.ImageTexture != null)
        this.SourceRectangle = new Rectangle(0, 0, this.ImageTexture.Width, this.ImageTexture.Height);
      this.Scale = scale;
      this.Rotation = rotation;
      this.Depth = depth;
      if (!(color != new Color()))
        return;
      this.Color = color;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.ImageTexture, this.GetPosition(), new Rectangle?(this.SourceRectangle), this.Color, this.Rotation, Vector2.Zero, this.Scale, this.SpriteEffect, (float) this.Depth);
    }
  }
}
