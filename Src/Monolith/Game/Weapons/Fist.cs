
// Type: ForestPlatformerExample.Fist
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class Fist : PhysicalEntity
  {
    private PhysicalEntity hero;
    private List<IGameObject> collidesWith = new List<IGameObject>();

    public Fist(AbstractScene scene, Entity parent, Vector2 positionOffset)
      : base(scene.LayerManager.EntityLayer, parent, positionOffset)
    {
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 10f));
      this.CheckGridCollisions = false;
      this.HasGravity = false;
      this.hero = parent as PhysicalEntity;
      this.CurrentFaceDirection = parent.CurrentFaceDirection;
      this.AddCollisionAgainst(typeof (Carrot));
      this.AddCollisionAgainst(typeof (Trunk));
      this.AddCollisionAgainst(typeof (Rock));
      this.AddCollisionAgainst(typeof (Ghost));
      this.AddCollisionAgainst(typeof (IceCream));
      this.AddCollisionAgainst(typeof (Box));
    }

    public override void OnCollisionStart(IGameObject otherCollider)
    {
      this.collidesWith.Add(otherCollider);
    }

    public override void OnCollisionEnd(IGameObject otherCollider)
    {
      this.collidesWith.Remove(otherCollider);
    }

    public void Attack()
    {
      if (Timer.IsSet("IsAttacking"))
        return;
      AudioEngine.Play("HeroPunch");
      if (this.CurrentFaceDirection == Direction.WEST)
        this.hero.GetComponent<AnimationStateMachine>().PlayAnimation("AttackLeft");
      else if (this.CurrentFaceDirection == Direction.EAST)
        this.hero.GetComponent<AnimationStateMachine>().PlayAnimation("AttackRight");
      Timer.SetTimer("IsAttacking", 300f);
      foreach (IGameObject gameObject in this.collidesWith)
      {
        if (gameObject is IAttackable)
        {
          Direction impactDirection = (double) gameObject.Transform.X < (double) this.hero.Transform.X ? Direction.WEST : Direction.EAST;
          (gameObject as IAttackable).Hit(impactDirection);
        }
      }
    }

    public void ChangeDirection()
    {
      if (this.CurrentFaceDirection == (this.Parent as Entity).CurrentFaceDirection)
        return;
      this.CurrentFaceDirection = (this.Parent as Entity).CurrentFaceDirection;
      int num = 1;
      if (this.CurrentFaceDirection == Direction.WEST)
        num = -1;
      this.Transform.Position = new Vector2((float) (num * 20), -10f);
    }
  }
}
