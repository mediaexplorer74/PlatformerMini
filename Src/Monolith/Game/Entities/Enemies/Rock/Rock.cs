// Type: ForestPlatformerExample.Rock
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class Rock : AbstractEnemy
  {
    private int health;
    private RockSize size;

    public Rock(
      AbstractScene scene,
      Vector2 position,
      RockSize size = RockSize.BIG,
      Direction startingFaceDirection = Direction.WEST)
      : base(scene, position)
    {
      this.size = size;
      this.CurrentFaceDirection = startingFaceDirection;
      this.CollisionOffsetBottom = 1f;
      Vector2 vector2_1 = new Vector2(0.0f, 2f);
      SpriteSheetAnimation animation1;
      SpriteSheetAnimation animation2;
      SpriteSheetAnimation animation3;
      SpriteSheetAnimation animation4;
      SpriteSheetAnimation animation5;
      SpriteSheetAnimation animation6;
      Vector2 vector2_2;
      CircleCollisionComponent newComponent1;
      switch (size)
      {
        case RockSize.BIG:
          this.health = 2;
          animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock1Run"), 24);
          animation2 = animation1.CopyFlipped();
          animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock1Idle"), 24);
          animation4 = animation3.CopyFlipped();
          animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock1Hit"), 24);
          animation6 = animation5.CopyFlipped();
          vector2_2 = new Vector2(8f, -18f);
          newComponent1 = new CircleCollisionComponent((IColliderEntity) this, 15f, vector2_2 + vector2_1);
          break;
        case RockSize.MEDIUM:
          this.health = 1;
          this.CurrentSpeed = (float) this.random.Next(8, 11) / 100f;
          animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock2Run"), 24);
          animation2 = animation1.CopyFlipped();
          animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock2Idle"), 24);
          animation4 = animation3.CopyFlipped();
          animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock2Hit"), 24);
          animation6 = animation5.CopyFlipped();
          vector2_2 = new Vector2(8f, -14f);
          newComponent1 = new CircleCollisionComponent((IColliderEntity) this, 12f, vector2_2 + vector2_1);
          break;
        default:
          this.health = 0;
          this.CurrentSpeed = (float) this.random.Next(12, 16) / 100f;
          animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock3Run"), 24);
          animation2 = animation1.CopyFlipped();
          animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock3Idle"), 24);
          animation4 = animation3.CopyFlipped();
          animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("Rock3Hit"), 24);
          animation6 = animation5.CopyFlipped();
          vector2_2 = new Vector2(3f, -10f);
          newComponent1 = new CircleCollisionComponent((IColliderEntity) this, 8f, vector2_2 + vector2_1);
          break;
      }

      bool isRunningLeft() => Transform.VelocityX != 0 && CurrentFaceDirection == Direction.WEST;
      bool isRunningRight() => Transform.VelocityX != 0 && CurrentFaceDirection == Direction.EAST;
      bool isIdleLeft() => Transform.VelocityX == 0 && CurrentFaceDirection == Direction.WEST;
      bool isIdleRight() => Transform.VelocityX == 0 && CurrentFaceDirection == Direction.EAST;

      animation5.Looping = false;
      animation6.Looping = false;

      AnimationStateMachine newComponent2 = new AnimationStateMachine();
      newComponent2.Offset = vector2_2;
      // ISSUE: method pointer
      newComponent2.RegisterAnimation("RunningLeft", (AbstractAnimation) animation1, isRunningLeft);
      // ISSUE: method pointer
      newComponent2.RegisterAnimation("RunningRight", (AbstractAnimation) animation2, isRunningRight);
      // ISSUE: method pointer
      newComponent2.RegisterAnimation("IdleLeft", (AbstractAnimation) animation3, isIdleLeft);
      // ISSUE: method pointer
      newComponent2.RegisterAnimation("IdleRight", (AbstractAnimation) animation4, isIdleRight);
      newComponent2.RegisterAnimation("HitLeft", (AbstractAnimation) animation5, (Func<bool>) (() => false));
      newComponent2.RegisterAnimation("HitRight", (AbstractAnimation) animation6, (Func<bool>) (() => false));
      newComponent2.AddFrameTransition("RunningLeft", "RunningRight");
      this.AddComponent<AnimationStateMachine>(newComponent2);
      this.AddComponent<CircleCollisionComponent>(newComponent1);
    }

    public override void FixedUpdate()
    {
      if (this.IsOnGround)
        AIUtil.Patrol(true, (AbstractEnemy) this);
      base.FixedUpdate();
    }

    public override void Hit(Direction impactDirection)
    {
      if (this.health == 0)
      {
        if (this.size == RockSize.BIG)
        {
          new Rock(this.Scene,
              this.Transform.Position, RockSize.MEDIUM).Transform.Velocity += new Vector2(-1f, -0.5f);
          new Rock(this.Scene, 
              this.Transform.Position, RockSize.MEDIUM, Direction.EAST).Transform.Velocity += new Vector2(1f, -0.5f);
          AudioEngine.Play("RockSplit");
          this.Destroy();
        }
        else if (this.size == RockSize.MEDIUM)
        {
          new Rock(this.Scene, this.Transform.Position, RockSize.SMALL).Transform.Velocity += new Vector2(-1f, -0.5f);
          new Rock(this.Scene, this.Transform.Position, RockSize.SMALL, Direction.EAST).Transform.Velocity += new Vector2(1f, -0.5f);
          AudioEngine.Play("RockSplit");
          this.Destroy();
        }
        else
        {
          AudioEngine.Play("TrunkHit");
          this.Transform.Velocity /= 3f;
          this.CurrentSpeed = this.DefaultSpeed;
          this.Die();
        }
      }
      else
      {
        --this.health;
        if (this.CurrentFaceDirection == Direction.WEST)
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HitLeft");
        else
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HitRight");
        AudioEngine.Play("TrunkHit");
        base.Hit(impactDirection);
      }
    }
  }
}
