// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.AbstractEnemy
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal abstract class AbstractEnemy : AbstractDestroyable, IAttackable
  {
    public int MoveDirection = 1;
    public float DefaultSpeed = 0.05f;
    public float CurrentSpeed = 0.05f;

    public AbstractEnemy(AbstractScene scene, Vector2 position)
      : base(scene, position)
    {
      this.AddTag("Enemy");
      this.CurrentFaceDirection = Direction.WEST;
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
      if ((double) this.Transform.Y > 2000.0 && (double) this.RotationRate == 0.0)
        this.Destroy();
      if ((double) this.RotationRate == 0.0)
        return;
      this.Transform.Rotation += this.RotationRate;
    }

    public virtual void Hit(Direction impactDirection)
    {
      this.Transform.Velocity = Vector2.Zero;
      Vector2 vector2 = new Vector2(1f, -1f);
      switch (impactDirection)
      {
        case Direction.WEST:
          vector2.X *= -1f;
          this.Transform.Velocity += vector2;
          break;
        case Direction.EAST:
          this.Transform.Velocity += vector2;
          break;
      }
      this.FallSpeed = 0.0f;
    }
  }
}
