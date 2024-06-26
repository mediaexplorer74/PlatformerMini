﻿
// Type: ForestPlatformerExample.IceCreamPatrolState
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class IceCreamPatrolState : AIState<IceCream>
  {
    protected bool checkCollisions;

    public IceCreamPatrolState(IceCream carrot)
      : base(carrot)
    {
    }

    public override void Begin()
    {
      this.checkCollisions = true;
      this.controlledEntity.CurrentSpeed = this.controlledEntity.DefaultSpeed;
    }

    public override void End()
    {
    }

    public override void FixedUpdate()
    {
      AIUtil.Patrol(this.checkCollisions, (AbstractEnemy) this.controlledEntity, 5000f);
    }
  }
}
