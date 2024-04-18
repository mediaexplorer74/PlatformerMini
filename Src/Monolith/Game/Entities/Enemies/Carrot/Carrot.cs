// Type: ForestPlatformerExample.Carrot
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using ForestPlatformerExampl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  internal class Carrot : AbstractEnemy
  {
    private int health = 2;
    private Hero hero;
    private List<Vector2> line = new List<Vector2>();
    private bool seesHero;
    public bool OverlapsWithHero;

    public Carrot(AbstractScene scene, Vector2 position, Direction currentFaceDirection)
      : base(scene, position)
    {
      this.DrawPriority = 1f;
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 12f, new Vector2(3f, -15f)));
      this.AddComponent<CarrotAIStateMachine>(new CarrotAIStateMachine((AIState<Carrot>) new CarrotPatrolState(this)));
      this.GetComponent<CarrotAIStateMachine>().AddState((AIState<Carrot>) new CarrotChaseState(this));
      this.GetComponent<CarrotAIStateMachine>().AddState((AIState<Carrot>) new CarrotIdleState(this));
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, 300, 300, new Vector2(-150f, -150f), "vision"));
      this.AddTriggeredAgainst(typeof (Hero));
      this.CurrentFaceDirection = currentFaceDirection;
      this.CollisionOffsetBottom = 1f;
      this.CollisionOffsetLeft = 0.7f;
      this.CollisionOffsetRight = 0.7f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      newComponent.Offset = new Vector2(3f, -33f);

      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, 
          Assets.GetAnimationTexture("CarrotMove"), 12);

      newComponent.RegisterAnimation("MoveLeft", (AbstractAnimation) animation1,
          (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST
          && (double) this.Transform.VelocityX != 0.0));

           
            void setSpeed(int frame)
            {
                if (frame > 3 && frame < 8)
                {
                    CurrentSpeed = DefaultSpeed;
                }
                else
                {
                    CurrentSpeed = 0.001f;
                }
            }
            //moveLeft.EveryFrameAction = setSpeed;
            animation1.EveryFrameAction = setSpeed;


            SpriteSheetAnimation animation2 = animation1.CopyFlipped();

      newComponent.RegisterAnimation("MoveRight", (AbstractAnimation) animation2, (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST && (double) this.Transform.VelocityX != 0.0));
      newComponent.AddFrameTransition("MoveLeft", "MoveRight");
      SpriteSheetAnimation spriteSheetAnimation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CarrotHurt"), 24);
      spriteSheetAnimation1.Looping = false;
      SpriteSheetAnimation animation3 = spriteSheetAnimation1;
      animation3.StoppedCallback = (Action) (() => this.CurrentSpeed = this.DefaultSpeed);
      newComponent.RegisterAnimation("HurtLeft", (AbstractAnimation) animation3, (Func<bool>) (() => false));
      SpriteSheetAnimation animation4 = animation3.CopyFlipped();
      newComponent.RegisterAnimation("HurtRight", (AbstractAnimation) animation4, (Func<bool>) (() => false));
      SpriteSheetAnimation spriteSheetAnimation2 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CarrotDeath"), 24);
      spriteSheetAnimation2.Looping = false;
      SpriteSheetAnimation spriteSheetAnimation3 = spriteSheetAnimation2;
      newComponent.RegisterAnimation("DeathLeft", (AbstractAnimation) spriteSheetAnimation3, (Func<bool>) (() => false));
      SpriteSheetAnimation spriteSheetAnimation4 = spriteSheetAnimation3.CopyFlipped();
      newComponent.RegisterAnimation("DeathRight", (AbstractAnimation) spriteSheetAnimation4, (Func<bool>) (() => false));
      SpriteSheetAnimation animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CarrotIdle"), 24);
      newComponent.RegisterAnimation("IdleLeft", (AbstractAnimation) animation5, (Func<bool>) (() => (double) this.Transform.VelocityX == 0.0 && this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation6 = animation5.CopyFlipped();
      newComponent.RegisterAnimation("IdleRight", (AbstractAnimation) animation6, (Func<bool>) (() => (double) this.Transform.VelocityX == 0.0 && this.CurrentFaceDirection == Direction.EAST));
      SpriteSheetAnimation animation7 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("CarrotMove"), 2);
      animation7.Looping = true;
      animation7.StartFrame = 5;
      animation7.EndFrame = 6;
      newComponent.RegisterAnimation("FallLeft", (AbstractAnimation) animation7, (Func<bool>) (() => (double) this.Transform.VelocityY > 0.0 && !this.IsOnGround && this.CurrentFaceDirection == Direction.WEST), 2);
      SpriteSheetAnimation animation8 = animation7.CopyFlipped();
      newComponent.RegisterAnimation("FallRight", (AbstractAnimation) animation8, (Func<bool>) (() => (double) this.Transform.VelocityY > 0.0 && !this.IsOnGround && this.CurrentFaceDirection == Direction.EAST), 2);
      this.SetDestroyAnimation((AbstractAnimation) spriteSheetAnimation4, Direction.EAST);
      this.SetDestroyAnimation((AbstractAnimation) spriteSheetAnimation3, Direction.WEST);
      this.Active = true;
      this.Visible = true;
      this.BlocksRay = true;
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
      if (this.BeingDestroyed || this.Destroyed)
        return;
      if (this.hero != null)
      {
        this.line.Clear();
        Bresenham.GetLine(this.Transform.Position + new Vector2(0.0f, -15f), this.hero.Transform.Position + new Vector2(0.0f, -10f), this.line);
        this.seesHero = Bresenham.CanLinePass(this.Transform.Position + new Vector2(0.0f, -15f), this.hero.Transform.Position + new Vector2(0.0f, -10f), (Func<int, int, bool>) ((x, y) => this.Scene.GridCollisionChecker.HasBlockingColliderAt(new Vector2((float) (x / Config.GRID), (float) (y / Config.GRID)))));
        if (this.seesHero)
        {
          if ((double) this.hero.Transform.X < (double) this.Transform.X)
            this.CurrentFaceDirection = Direction.WEST;
          else
            this.CurrentFaceDirection = Direction.EAST;
        }
      }
      else
        this.seesHero = false;
      if (this.seesHero)
      {
        if (!this.OverlapsWithHero && (double) Math.Abs(this.hero.Transform.X - this.Transform.X) < 10.0)
          this.GetComponent<CarrotAIStateMachine>().ChangeState<CarrotIdleState>();
        else
          this.GetComponent<CarrotAIStateMachine>().ChangeState<CarrotChaseState>();
      }
      else
        this.GetComponent<CarrotAIStateMachine>().ChangeState<CarrotPatrolState>();
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);

    public override void Hit(Direction impactDirection)
    {
      if (this.health == 0)
      {
        this.CurrentSpeed = 0.0f;
        this.RemoveCollisions();
        this.Die();
      }
      else
      {
        --this.health;
        this.CurrentSpeed = 0.0f;
        this.PlayHurtAnimation();
        AudioEngine.Play("CarrotJumpHurtSound");
        if (impactDirection == Direction.NORTH)
        {
          this.CurrentSpeed = 0.0f;
          Timer.TriggerAfter(300f, (Action) (() => this.CurrentSpeed = this.DefaultSpeed));
        }
        else
          base.Hit(impactDirection);
      }
    }

    public override void Die()
    {
      AudioEngine.Play("CarrotExplodeSound");
      base.Die();
    }

    private void PlayHurtAnimation()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HurtLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HurtRight");
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (!(otherEntity is Hero))
        return;
      this.hero = otherEntity as Hero;
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      this.line.Clear();
      if (!(otherEntity is Hero))
        return;
      this.hero = (Hero) null;
    }
  }
}
