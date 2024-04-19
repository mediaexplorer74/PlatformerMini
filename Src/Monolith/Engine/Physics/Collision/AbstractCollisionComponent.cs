
// Type: MonolithEngine.AbstractCollisionComponent
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public abstract class AbstractCollisionComponent : ICollisionComponent, IComponent
  {
    protected Vector2 PositionOffset;
    protected ColliderType type;
    protected IColliderEntity owner;

    public Vector2 Position => this.PositionOffset + this.owner.Transform.Position;

    public bool UniquePerEntity { get; set; }

    protected AbstractCollisionComponent(
      ColliderType type,
      IColliderEntity owner,
      Vector2 positionOffset = default (Vector2))
    {
      this.owner = owner;
      this.PositionOffset = positionOffset;
      this.type = type;
      this.UniquePerEntity = true;
    }

    public abstract bool CollidesWith(IColliderEntity otherCollider);

    ColliderType ICollisionComponent.GetType() => this.type;

    Type IComponent.GetComponentType() => typeof (ICollisionComponent);
  }
}
