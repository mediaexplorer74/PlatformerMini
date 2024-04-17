// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.IceCream
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class IceCream : AbstractEnemy
  {
    public Hero Target;
    private bool isAttacking;
    private bool canAttack = true;
    private IceCreamAIStateMachine AI;
    private int health = 2;

    public IceCream(AbstractScene scene, Vector2 position)
      : base(scene, position)
    {
      this.DrawPriority = 1f;
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 12f, new Vector2(3f, -20f)));
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, 300, 300, new Vector2(-150f, -150f), "vision"));
      this.AddTriggeredAgainst(typeof (Hero));
      this.CurrentFaceDirection = Direction.WEST;
      this.CollisionOffsetBottom = 1f;
      this.CollisionOffsetLeft = 0.7f;
      this.CollisionOffsetRight = 0.7f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      newComponent.Offset = new Vector2(3f, -33f);
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamIdle"), 23);
      newComponent.RegisterAnimation("IdleLeft", (AbstractAnimation) animation1, (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST && (double) this.Transform.VelocityX == 0.0));
      SpriteSheetAnimation animation2 = animation1.CopyFlipped();
      newComponent.RegisterAnimation("IdleRight", (AbstractAnimation) animation2, (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST && (double) this.Transform.VelocityX == 0.0));
      SpriteSheetAnimation animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamHurt"), 24);
      animation3.Looping = false;
      animation3.StartedCallback = (Action) (() =>
      {
        this.CurrentSpeed = 0.0f;
        this.canAttack = false;
      });
      animation3.StoppedCallback = (Action) (() =>
      {
        this.CurrentSpeed = this.DefaultSpeed;
        this.canAttack = true;
      });
      newComponent.RegisterAnimation("HurtLeft", (AbstractAnimation) animation3, (Func<bool>) (() => false));
      SpriteSheetAnimation animation4 = animation3.CopyFlipped();
      newComponent.RegisterAnimation("HurtRight", (AbstractAnimation) animation4, (Func<bool>) (() => false));
      SpriteSheetAnimation animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamMove"), 17);
      animation5.EveryFrameAction = (Action<int>) (frame =>
      {
        if (frame >= 7 && frame <= 10)
          this.CurrentSpeed = this.DefaultSpeed;
        else
          this.CurrentSpeed = 1f / 1000f;
      });
      newComponent.RegisterAnimation("MoveLeft", (AbstractAnimation) animation5, (Func<bool>) (() => this.CurrentFaceDirection == Direction.WEST && (double) this.Transform.VelocityX != 0.0));
      SpriteSheetAnimation animation6 = animation5.CopyFlipped();
      newComponent.RegisterAnimation("MoveRight", (AbstractAnimation) animation6, (Func<bool>) (() => this.CurrentFaceDirection == Direction.EAST && (double) this.Transform.VelocityX != 0.0));
      newComponent.AddFrameTransition("MoveLeft", "MoveRight");
      SpriteSheetAnimation animation7 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamAttack"), 36);
      animation7.Looping = false;
      animation7.StartedCallback = (Action) (() => this.isAttacking = true);
      animation7.StoppedCallback = (Action) (() => this.isAttacking = false);
      animation7.AddFrameAction(21, (Action<int>) (frame => this.SpawnProjectiles()));
      newComponent.RegisterAnimation("AttackLeft", (AbstractAnimation) animation7, (Func<bool>) (() => false));
      SpriteSheetAnimation animation8 = animation7.CopyFlipped();
      newComponent.RegisterAnimation("AttackRight", (AbstractAnimation) animation8, (Func<bool>) (() => false));
      newComponent.AddFrameTransition("AttackLeft", "AttackRight");
      this.AI = new IceCreamAIStateMachine((AIState<IceCream>) new IceCreamPatrolState(this));
      this.AI.AddState((AIState<IceCream>) new IceCreamAttackState(this));
      this.AddComponent<IceCreamAIStateMachine>(this.AI);
      SpriteSheetAnimation spriteSheetAnimation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("IceCreamDeath"), 24);
      spriteSheetAnimation1.Looping = false;
      newComponent.RegisterAnimation("DeathLeft", (AbstractAnimation) spriteSheetAnimation1, (Func<bool>) (() => false));
      SpriteSheetAnimation spriteSheetAnimation2 = spriteSheetAnimation1.CopyFlipped();
      newComponent.RegisterAnimation("DeathRight", (AbstractAnimation) spriteSheetAnimation2, (Func<bool>) (() => false));
      this.SetDestroyAnimation((AbstractAnimation) spriteSheetAnimation1, Direction.WEST);
      this.SetDestroyAnimation((AbstractAnimation) spriteSheetAnimation2, Direction.EAST);
    }

    public override void FixedUpdate()
    {
      if (this.Target != null)
        this.AI.ChangeState<IceCreamAttackState>();
      else if (!this.isAttacking)
        this.AI.ChangeState<IceCreamPatrolState>();
      base.FixedUpdate();
    }

    public void Attack()
    {
      if (this.isAttacking || !this.canAttack)
        return;
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AttackLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AttackRight");
    }

    private void SpawnProjectiles()
    {
      AudioEngine.Play("IceCreamExplode");
      new IceCreamProjectile(this.Scene, this.Transform.Position + new Vector2(0.0f, -40f)).AddForce(new Vector2(-0.2f, -0.3f));
      new IceCreamProjectile(this.Scene, this.Transform.Position + new Vector2(0.0f, -40f)).AddForce(new Vector2(0.2f, -0.3f));
    }

    public override void Hit(Direction impactDirection)
    {
      if (this.health == 0)
      {
        AudioEngine.Play("CarrotExplodeSound");
        this.CurrentSpeed = 0.0f;
        this.AI.Enabled = false;
        this.Die();
      }
      else
      {
        if (this.CurrentFaceDirection == Direction.WEST)
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HurtLeft");
        else
          this.GetComponent<AnimationStateMachine>().PlayAnimation("HurtRight");
        --this.health;
        AudioEngine.Play("TrunkHit");
        base.Hit(impactDirection);
      }
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        this.Target = otherEntity as Hero;
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        this.Target = (Hero) null;
      base.OnLeaveTrigger(triggerTag, otherEntity);
    }
  }
}
