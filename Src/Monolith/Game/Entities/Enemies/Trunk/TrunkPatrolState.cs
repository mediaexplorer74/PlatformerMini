// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.TrunkPatrolState
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class TrunkPatrolState : AIState<Trunk>
  {
    public TrunkPatrolState(Trunk trunk)
      : base(trunk)
    {
    }

    public override void Begin()
    {
    }

    public override void End()
    {
    }

    public override void FixedUpdate()
    {
      if (this.controlledEntity.IsAttacking)
        return;
      AIUtil.Patrol(true, (AbstractEnemy) this.controlledEntity);
    }
  }
}
