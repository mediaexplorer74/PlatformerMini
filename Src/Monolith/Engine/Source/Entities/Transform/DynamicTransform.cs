// Decompiled with JetBrains decompiler
// Type: MonolithEngine.DynamicTransform
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;


namespace MonolithEngine
{
  internal class DynamicTransform : StaticTransform
  {
    private Vector2 velocity;

    public DynamicTransform(IGameObject owner, Vector2 position = default (Vector2))
      : base(owner, position)
    {
    }

    public override Vector2 Velocity
    {
      get => this.velocity;
      set => this.velocity = value;
    }

    public override float VelocityX
    {
      get => this.velocity.X;
      set => this.velocity.X = value;
    }

    public override float VelocityY
    {
      get => this.velocity.Y;
      set => this.velocity.Y = value;
    }
  }
}
