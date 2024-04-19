
// Type: MonolithEngine.ITransform
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;


namespace MonolithEngine
{
  public interface ITransform
  {
    Vector2 Position { get; set; }

    float X { get; }

    float Y { get; }

    Vector2 Velocity { get; set; }

    float Rotation { get; set; }

    void OverridePositionOffset(Vector2 newPositionOffset);
  }
}
