// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Ghost
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class Ghost : AbstractEnemy
  {
    private readonly int APPEAR_DISAPPEAR_TIMEOUT = 2000;
    private bool beingHit;
    private int health = 2;

    public Ghost(AbstractScene scene, Vector2 position)
      : base(scene, position)
    {
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("GhostAppear"), 24);
      animation1.Looping = false;
      animation1.StoppedCallback = (Action) (() => this.CollisionsEnabled = true);
      newComponent.RegisterAnimation("AppearLeft", (AbstractAnimation) animation1, (Func<bool>) (() => false));
      SpriteSheetAnimation animation2 = animation1.CopyFlipped();
      newComponent.RegisterAnimation("AppearRight", (AbstractAnimation) animation2, (Func<bool>) (() => false));
      SpriteSheetAnimation animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("GhostDisappear"), 24);
      animation3.Looping = false;
      animation3.StoppedCallback = (Action) (() =>
      {
        this.CollisionsEnabled = false;
        this.Visible = false;
      });
      newComponent.RegisterAnimation("DisappearLeft", (AbstractAnimation) animation3, (Func<bool>) (() => false));
      SpriteSheetAnimation animation4 = animation3.CopyFlipped();
      newComponent.RegisterAnimation("DisappearRight", (AbstractAnimation) animation4, (Func<bool>) (() => false));
      SpriteSheetAnimation animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("GhostHit"), 24);
      animation5.Looping = false;
      animation5.StartedCallback = (Action) (() => this.beingHit = true);
      animation5.StoppedCallback = (Action) (() =>
      {
        this.beingHit = false;
        Timer.TriggerAfter((float) this.APPEAR_DISAPPEAR_TIMEOUT, new Action(this.Disappear));
      });
      newComponent.RegisterAnimation("HitLeft", (AbstractAnimation) animation5, (Func<bool>) (() => false));
      SpriteSheetAnimation animation6 = animation5.CopyFlipped();
      newComponent.RegisterAnimation("HitRight", (AbstractAnimation) animation6, (Func<bool>) (() => false));
      SpriteSheetAnimation animation7 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("GhostIdle"), 24);
      newComponent.RegisterAnimation("IdleLeft", (AbstractAnimation) animation7, (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation8 = animation7.CopyFlipped();
      newComponent.RegisterAnimation("IdleRight", (AbstractAnimation) animation8, (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST));
      newComponent.AddFrameTransition("IdleLeft", "IdleRight");
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 25f, 25f, new Vector2(-12f, -12f)));
      Timer.TriggerAfter((float) this.random.Next(0, this.APPEAR_DISAPPEAR_TIMEOUT), new Action(this.Disappear));
    }

    private void Appear()
    {
      if ((double) this.RotationRate != 0.0 || this.Destroyed || this.BeingDestroyed)
        return;
      this.Visible = true;
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AppearLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AppearRight");
      Timer.TriggerAfter((float) this.APPEAR_DISAPPEAR_TIMEOUT, new Action(this.Disappear));
    }

    private void Disappear()
    {
      if ((double) this.RotationRate != 0.0 || this.beingHit || this.Destroyed || this.BeingDestroyed)
        return;
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("DisappearLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("DisappearRight");
      Timer.TriggerAfter((float) this.APPEAR_DISAPPEAR_TIMEOUT, new Action(this.Appear));
    }

    public override void Hit(Direction impactDirection)
    {
      AudioEngine.Play("TrunkHit");
      if (this.health == 0)
      {
        this.Die();
      }
      else
      {
        if (this.CurrentFaceDirection == Direction.WEST)
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HitLeft");
        else
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HitRight");
        --this.health;
        base.Hit(impactDirection);
      }
    }

    public override void FixedUpdate()
    {
      if (this.IsOnGround && !this.beingHit)
        AIUtil.Patrol(true, (AbstractEnemy) this);
      base.FixedUpdate();
    }
  }
}
