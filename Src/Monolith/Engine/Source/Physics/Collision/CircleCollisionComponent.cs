// Decompiled with JetBrains decompiler
// Type: MonolithEngine.CircleCollisionComponent
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public class CircleCollisionComponent : AbstractCollisionComponent
  {
    public float Radius;
    public bool IsCircleCollider = true;
    private float maxDistance;
    private float distance;

    public CircleCollisionComponent(IColliderEntity owner, float radius, Vector2 positionOffset = default (Vector2))
      : base(ColliderType.CIRCLE, owner, positionOffset)
    {
      this.Radius = radius;
    }

    public override bool CollidesWith(IColliderEntity otherCollider)
    {
      if (otherCollider.GetCollisionComponent().GetType() == ColliderType.CIRCLE)
      {
        CircleCollisionComponent collisionComponent = otherCollider.GetCollisionComponent() as CircleCollisionComponent;
        if ((double) Math.Abs(this.Position.X - otherCollider.GetCollisionComponent().Position.X) > (double) (Config.GRID * 2) && (double) Math.Abs(this.Position.Y - collisionComponent.Position.Y) > (double) (Config.GRID * 2))
          return false;
        this.maxDistance = this.Radius + collisionComponent.Radius;
        this.distance = Vector2.Distance(this.Position, collisionComponent.Position);
        return (double) this.distance <= (double) this.maxDistance;
      }
      BoxCollisionComponent collisionComponent1 = otherCollider.GetCollisionComponent().GetType() == ColliderType.BOX ? otherCollider.GetCollisionComponent() as BoxCollisionComponent : throw new Exception("Unknown collider type");
      return (double) Vector2.DistanceSquared(
          this.Position, 
          new Vector2(MathClamp(this.Position.X, collisionComponent1.Position.X, collisionComponent1.Position.X + collisionComponent1.Width), 
          MathClamp(this.Position.Y, collisionComponent1.Position.Y, collisionComponent1.Position.Y + collisionComponent1.Height))) < (double) this.Radius * (double) this.Radius;
    }

        private float MathClamp(float v, float min, float max)
        {
            if (v <= min) return min;
            if (v >= max) return max;
            return v;
        }
    }
}
