
// Type: MonolithEngine.PhysicsUtil
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public class PhysicsUtil
  {
    public static void ApplyRepel(
      IColliderEntity thisCollider,
      IColliderEntity otherCollider,
      float repelForceOverride = 0.0f,
      RepelMode repelMode = RepelMode.BOTH)
    {
      float num1 = (float) Math.Atan2((double) otherCollider.GetCollisionComponent().Position.Y - (double) thisCollider.GetCollisionComponent().Position.Y, (double) otherCollider.GetCollisionComponent().Position.X - (double) thisCollider.GetCollisionComponent().Position.X);
      float num2 = (double) repelForceOverride != 0.0 ? repelForceOverride : Vector2.Distance(thisCollider.GetCollisionComponent().Position, otherCollider.GetCollisionComponent().Position);
      if (repelMode == RepelMode.ONLY_THIS || repelMode == RepelMode.BOTH)
        (thisCollider as PhysicalEntity).AddForce(new Vector2((float) -Math.Cos((double) num1) * num2, (float) -Math.Sin((double) num1) * num2));
      if (repelMode != RepelMode.OTHER_COLLIDER_ONLY && repelMode != RepelMode.BOTH)
        return;
      (otherCollider as PhysicalEntity).AddForce(new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2));
    }
  }
}
