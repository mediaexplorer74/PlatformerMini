
// Type: ForestPlatformerExample.TrunkAttackState
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class TrunkAttackState : AIState<Trunk>
  {
    public TrunkAttackState(Trunk trunk)
      : base(trunk)
    {
    }

    public override void Begin() => this.controlledEntity.Transform.Velocity = Vector2.Zero;

    public override void End()
    {
    }

    public override void FixedUpdate()
    {
      if (this.controlledEntity.Target == null)
        return;
      if ((double) this.controlledEntity.Target.Transform.X < (double) this.controlledEntity.Transform.X)
        this.controlledEntity.CurrentFaceDirection = Direction.WEST;
      else
        this.controlledEntity.CurrentFaceDirection = Direction.EAST;
      this.controlledEntity.Shoot();
    }
  }
}
