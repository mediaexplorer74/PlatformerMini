// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IUIElement
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

#nullable disable
namespace MonolithEngine
{
  public interface IUIElement
  {
    void Draw(SpriteBatch spriteBatch);

    void Update(Point mousePosition = default (Point));

    void Update(TouchCollection touchLocations);

    IUIElement GetParent();

    Vector2 GetPosition();
  }
}
