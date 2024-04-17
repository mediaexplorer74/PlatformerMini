// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.CarrotIdleState
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;

#nullable disable
namespace ForestPlatformerExample
{
  internal class CarrotIdleState : AIState<Carrot>
  {
    public CarrotIdleState(Carrot carrot)
      : base(carrot)
    {
    }

    public override void Begin() => this.controlledEntity.Transform.Velocity = Vector2.Zero;

    public override void End()
    {
    }

    public override void FixedUpdate()
    {
    }
  }
}
