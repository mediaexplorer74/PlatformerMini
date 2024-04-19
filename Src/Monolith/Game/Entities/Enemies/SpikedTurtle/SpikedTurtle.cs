
// Type: ForestPlatformerExample.SpikedTurtle
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null


using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Diagnostics;

namespace ForestPlatformerExample
{
  internal class SpikedTurtle : AbstractEnemy
  {
    public bool SpikesOut;
    private float health = 2f;
    private bool beingHit;

    public SpikedTurtle(AbstractScene scene, Vector2 position, Direction currentFaceDirection)
      : base(scene, position)
    {
      this.CurrentFaceDirection = currentFaceDirection;
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 
          17f, new Vector2(8f, -5f)));
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      newComponent.Offset = new Vector2(8f, -13f);
      this.CollisionOffsetBottom = 1f;
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, 
          Assets.GetAnimationTexture("TurtleIdleNormal"), 24);
      animation1.Looping = true;
      animation1.StartedCallback = (Action) (() => Timer.TriggerAfter(
          3000f, (Action) (() => this.SpikesOutState())));
      newComponent.RegisterAnimation("IdleNormalLeft", (AbstractAnimation) animation1,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation2 = animation1.CopyFlipped();
      newComponent.RegisterAnimation("IdleNormalRight", (AbstractAnimation) animation2,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST));
      SpriteSheetAnimation animation3 = new SpriteSheetAnimation((Entity) this,
          Assets.GetAnimationTexture("TurtleIdleSpiked"), 24);
      animation3.Looping = true;
      newComponent.RegisterAnimation("IdleSpikedLeft", (AbstractAnimation) animation3,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation4 = animation3.CopyFlipped();
      newComponent.RegisterAnimation("IdleSpikedRight", (AbstractAnimation) animation4,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST));
      SpriteSheetAnimation animation5 = new SpriteSheetAnimation((Entity) this,
          Assets.GetAnimationTexture("TurtleHit"), 24);
      animation5.Looping = false;
      animation5.StartedCallback = (Action) (() => this.beingHit = true);
      animation5.StoppedCallback = (Action) (() => this.beingHit = false);
      newComponent.RegisterAnimation("HitLeft", (AbstractAnimation) animation5,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation6 = animation5.CopyFlipped();
      newComponent.RegisterAnimation("HitRight", (AbstractAnimation) animation6,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST));
      SpriteSheetAnimation animation7 = new SpriteSheetAnimation((Entity) this, 
          Assets.GetAnimationTexture("TurtleSpikesOut"), 240);
      animation7.Looping = false;
      animation7.AddFrameAction(5, (Action<int>) (frame => this.SpikesOut = true));
      animation7.StoppedCallback = (Action) (() => this.SpikesOutWait());
      newComponent.RegisterAnimation("SpikesOutLeft", (AbstractAnimation) animation7, 
          (Func<bool>) (() => false));
      SpriteSheetAnimation animation8 = animation7.CopyFlipped();
      newComponent.RegisterAnimation("SpikesOutRight", (AbstractAnimation) animation8,
          (Func<bool>) (() => false));
      SpriteSheetAnimation animation9 = new SpriteSheetAnimation((Entity) this, 
          Assets.GetAnimationTexture("TurtleSpikesIn"), 240);
      animation9.Looping = false;
      animation9.AddFrameAction(6, (Action<int>) (frame => this.SpikesOut = false));
      newComponent.RegisterAnimation("SpikesInLeft", (AbstractAnimation) animation9, 
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation10 = animation9.CopyFlipped();
      newComponent.RegisterAnimation("SpikesInRight", (AbstractAnimation) animation10,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST));
    }

    private void SpikesOutState()
    {
      if (this.beingHit || (double) this.RotationRate != 0.0)
        return;
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("SpikesOutLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("SpikesOutRight");
    }

    private void SpikesOutWait()
    {
      string nextAnim = "SpikesInLeft";
      if (this.CurrentFaceDirection == Direction.WEST)
      {
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleSpikedLeft");
      }
      else
      {
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleSpikedRight");
        nextAnim = "SpikesInRight";
      }
      Timer.TriggerAfter(3000f, (Action) (() =>
      {
        if ((double) this.RotationRate != 0.0)
          return;
        this.PlayNext(nextAnim);
      }));
    }

    private void PlayNext(string nextAnim)
    {
            try
            {
                if (this.GetComponent<AnimationStateMachine>() != null)
                  this.GetComponent<AnimationStateMachine>().PlayAnimation(nextAnim);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] SpikedTurtle error: " + ex.Message);
            }
      this.SpikesOut = true;
    }

    public override void Hit(Direction impactDirection)
    {
      if ((double) this.health == 0.0)
      {
        this.Die();
      }
      else
      {
        --this.health;
        this.PlayHurtAnimation();
        base.Hit(impactDirection);
      }
    }

    public override void Die()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleNormalLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleNormalRight");
      base.Die();
    }

    private void PlayHurtAnimation()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HitLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HitRight");
    }
  }
}
