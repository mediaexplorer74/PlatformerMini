
// Type: ForestPlatformerExample.Coin
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class Coin : AbstractInteractive
  {
    public Coin(AbstractScene scene, Vector2 position, int bounceCount = 0, float friction = 0.9f)
      : base(scene, position)
    {
      this.Active = true;
      this.AddTag("COIN");
      this.DrawPriority = 2f;
      this.SetCircleCollider();
      this.HasGravity = true;
      this.HorizontalFriction = friction;
      this.VerticalFriction = friction;
      this.CollisionOffsetBottom = 0.0f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      newComponent.Offset = new Vector2(8f, 8f);
      this.AddComponent<AnimationStateMachine>(newComponent);
      SpriteSheetAnimation animation = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CoinPickup"), 30);
      animation.AnimationPauseCondition = (Func<bool>) (() => !this.IsAtRest());
      newComponent.RegisterAnimation("Idle", (AbstractAnimation) animation);
      this.SetDestroyAnimation((AbstractAnimation) new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CoinPickupEffect"), 45));
    }

    public void SetBump(Vector2 power) => this.Bump(power, true);

    public override void PostUpdate()
    {
      if (this.Destroyed)
        return;
      base.PostUpdate();
    }

    public void SetCircleCollider()
    {
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 10f, new Vector2(8f, 8f)));
    }

    public override void Update()
    {
      base.Update();
      if (this.Destroyed || (double) this.Transform.Y <= 5000.0)
        return;
      this.Destroy();
    }

    public override void Destroy() => base.Destroy();
  }
}
