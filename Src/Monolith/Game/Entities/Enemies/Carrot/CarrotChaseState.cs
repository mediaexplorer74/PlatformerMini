
// Type: ForestPlatformerExample.CarrotChaseState
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll


namespace ForestPlatformerExample
{
  internal class CarrotChaseState : CarrotPatrolState
  {
    public CarrotChaseState(Carrot carrot)
      : base(carrot)
    {
    }

    public override void Begin()
    {
      this.controlledEntity.CurrentSpeed = this.controlledEntity.DefaultSpeed;
      this.checkCollisions = false;
    }
  }
}
