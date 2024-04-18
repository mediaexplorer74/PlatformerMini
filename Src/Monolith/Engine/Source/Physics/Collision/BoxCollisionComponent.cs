// Decompiled with JetBrains decompiler
// Type: MonolithEngine.BoxCollisionComponent
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public class BoxCollisionComponent : AbstractCollisionComponent
  {
    public float Width;
    public float Height;

    public BoxCollisionComponent(
      IColliderEntity owner,
      float width,
      float height,
      Vector2 positionOffset = default (Vector2))
      : base(ColliderType.BOX, owner, positionOffset)
    {
      this.Width = width;
      this.Height = height;
    }

    public BoxCollisionComponent(
      IColliderEntity owner,
      Rectangle boundingBox,
      Vector2 positionOffset = default (Vector2))
      : base(ColliderType.BOX, owner, positionOffset)
    {
      this.Width = (float) boundingBox.Width;
      this.Height = (float) boundingBox.Height;
    }

    public override bool CollidesWith(IColliderEntity otherCollider)
    {
      if (otherCollider.GetCollisionComponent().GetType() == ColliderType.BOX)
      {
        BoxCollisionComponent collisionComponent = otherCollider.GetCollisionComponent() as BoxCollisionComponent;
        return (double) this.Position.X <= (double) collisionComponent.Position.X + (double) collisionComponent.Width && (double) this.Position.X + (double) this.Width >= (double) collisionComponent.Position.X && (double) this.Position.Y <= (double) collisionComponent.Position.Y + (double) collisionComponent.Height && (double) this.Position.Y + (double) this.Height >= (double) collisionComponent.Position.Y;
      }
      return otherCollider.GetCollisionComponent().GetType() == ColliderType.CIRCLE ? otherCollider.GetCollisionComponent().CollidesWith(this.owner) : throw new Exception("Unknown collider type");
    }
  }
}
