
// Type: ForestPlatformerExample.IceCreamProjectile
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class IceCreamProjectile : AbstractDestroyable
  {
    private bool destroyStarted;

    public IceCreamProjectile(AbstractScene scene, Vector2 position)
      : base(scene, position)
    {
      this.AddTag(nameof (IceCreamProjectile));
      this.CheckGridCollisions = true;
      this.GravityValue /= 3f;
      this.DrawPriority = 0.0f;
      this.HorizontalFriction = 0.99f;
      this.VerticalFriction = 0.99f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamProjectileIdle"), 24);
      newComponent.RegisterAnimation("Idle", (AbstractAnimation) animation1);
      SpriteSheetAnimation animation2 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamProjectileHit"), 24);
      animation2.Looping = false;
      animation2.StartedCallback = (Action) (() =>
      {
        this.CancelVelocities();
        this.HasGravity = false;
        this.RemoveCollisions();
      });
      animation2.StoppedCallback = (Action) (() => this.Destroy());
      newComponent.RegisterAnimation("Hit", (AbstractAnimation) animation2, (Func<bool>) (() => false));
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 5f, Vector2.Zero));
      Timer.TriggerAfter(5000f, new Action(((GameObject) this).Destroy));
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
      if (!this.CollidesOnGrid)
        return;
      this.DestroyBullet();
    }

    public void DestroyBullet()
    {
      if (this.destroyStarted)
        return;
      this.destroyStarted = true;
      this.GetComponent<AnimationStateMachine>().PlayAnimation("Hit");
    }
  }
}
