// Type: MonolithEngine.AbstractUIElement
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;


namespace MonolithEngine
{
  public abstract class AbstractUIElement : IUIElement
  {
    public IUIElement Parent;
    public Vector2 Position;

    public AbstractUIElement(Vector2 position) => this.Position = position;

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }

    public IUIElement GetParent() => this.Parent;

    public Vector2 GetPosition()
    {
      return this.Parent != null ? this.Parent.GetPosition() + this.Position : this.Position;
    }

    public virtual void Update(Point mousePosition = default (Point))
    {
    }

    public virtual void Update(TouchCollection touchLocations)
    {
    }
  }
}
