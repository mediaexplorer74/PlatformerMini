// Type: MonolithEngine.AnimatedImage
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;


namespace MonolithEngine
{
  public class AnimatedImage : SpriteSheetAnimation, IUIElement
  {
    private IUIElement parent;
    private Vector2 position;

    public AnimatedImage(
      AnimationTexture texture,
      Vector2 position,
      int framerate,
      SpriteEffects spriteEffect = SpriteEffects.None,
      IUIElement parent = null)
      : base((Entity) null, texture, framerate, spriteEffect)
    {
      this.parent = parent;
      this.position = position;
    }

    void IUIElement.Draw(SpriteBatch spriteBatch) => this.Play(spriteBatch);

    IUIElement IUIElement.GetParent() => this.parent;

    Vector2 IUIElement.GetPosition() => this.position;

    void IUIElement.Update(Point mousePosition)
    {
    }

    void IUIElement.Update(TouchCollection touchLocations)
    {
    }
  }
}
