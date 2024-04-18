// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Trunk
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class Trunk : AbstractEnemy
  {
    public bool IsAttacking;
    private bool canAttack = true;
    private TrunkAIStateMachine AI;
    public Hero Target;
    public bool turnedLeft = true;
    private int health = 2;

    public Trunk(AbstractScene scene, Vector2 position, Direction currentFaceDirection)
      : base(scene, position)
    {
      this.CurrentFaceDirection = currentFaceDirection;
      this.DrawPriority = 3f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      newComponent.Offset = new Vector2(0.0f, -16f);
      this.CollisionOffsetBottom = 1f;
      this.AddComponent<AnimationStateMachine>(newComponent);
      this.AI = new TrunkAIStateMachine((AIState<Trunk>) new TrunkPatrolState(this));
      this.AI.AddState((AIState<Trunk>) new TrunkAttackState(this));
      this.AddComponent<TrunkAIStateMachine>(this.AI);
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 20f, 20f, new Vector2(-10f, -24f)));
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, 512, 256, new Vector2(-256f, -144f)));
      this.AddTriggeredAgainst(typeof (Hero));
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("TrunkIdle"), 24);
      animation1.Looping = true;
      newComponent.RegisterAnimation("IdleLeft", (AbstractAnimation) animation1, (Func<bool>) (() => (double) this.Transform.VelocityX == 0.0 && this.CurrentFaceDirection == Direction.WEST));
      SpriteSheetAnimation animation2 = animation1.CopyFlipped();
      newComponent.RegisterAnimation("IdleRight", (AbstractAnimation) animation2, (Func<bool>) (() => (double) this.Transform.VelocityX == 0.0 && this.CurrentFaceDirection == Direction.EAST));
      SpriteSheetAnimation animation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("TrunkRun"), 24);
      newComponent.RegisterAnimation("RunLeft", (AbstractAnimation) animation3, (Func<bool>) (() => (double) this.Transform.VelocityX < 0.0));
      SpriteSheetAnimation animation4 = animation3.CopyFlipped();
      newComponent.RegisterAnimation("RunRight", (AbstractAnimation) animation4, (Func<bool>) (() => (double) this.Transform.VelocityX > 0.0));
      SpriteSheetAnimation animation5 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("TrunkAttack"), 24);
      animation5.AddFrameAction(8, (Action<int>) (frame => this.SpawnBullet()));
      animation5.Looping = false;
      animation5.StartedCallback = (Action) (() => this.turnedLeft = true);
      animation5.StoppedCallback = (Action) (() => this.turnedLeft = false);
      newComponent.RegisterAnimation("AttackLeft", (AbstractAnimation) animation5, (Func<bool>) (() => false));
      SpriteSheetAnimation animation6 = animation5.CopyFlipped();
      animation6.StartedCallback = (Action) (() => this.turnedLeft = false);
      animation6.StoppedCallback = (Action) (() => this.turnedLeft = true);
      newComponent.RegisterAnimation("AttackRight", (AbstractAnimation) animation6, (Func<bool>) (() => false));
      SpriteSheetAnimation animation7 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("TrunkHit"), 24);
      animation7.Looping = false;
      animation7.StartedCallback = (Action) (() => this.canAttack = false);
      animation7.StoppedCallback = (Action) (() => this.canAttack = true);
      newComponent.RegisterAnimation("HitLeft", (AbstractAnimation) animation7, (Func<bool>) (() => false));
      SpriteSheetAnimation animation8 = animation7.CopyFlipped();
      newComponent.RegisterAnimation("HitRight", (AbstractAnimation) animation8, (Func<bool>) (() => false));
      this.Active = true;
      this.Visible = true;
    }

    public override void Hit(Direction impactDirection)
    {
      if (this.health == 0)
      {
        this.Die();
      }
      else
      {
        AudioEngine.Play("TrunkHit");
        --this.health;
        this.PlayHurtAnimation();
        base.Hit(impactDirection);
      }
    }

    public override void Die()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("IdleRight");
      AudioEngine.Play("TrunkDeath");
      base.Die();
    }

    private void PlayHurtAnimation()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HitLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("HitRight");
    }

    public void Shoot()
    {
      if (Timer.IsSet("TrunkShooting" + this.GetID().ToString()) || !this.canAttack)
        return;
      this.IsAttacking = true;
      Timer.SetTimer("TrunkShooting" + this.GetID().ToString(), 1500f);
      Timer.TriggerAfter(1500f, (Action) (() => this.IsAttacking = false));
      this.PlayAttackAnimation();
    }

    private void SpawnBullet()
    {
      AudioEngine.Play("TrunkShoot");
      if (this.turnedLeft)
      {
        Bullet bullet1 = new Bullet(this.Scene, this.Transform.Position - new Vector2(29f, 15f), new Vector2(-0.3f, 0.0f));
      }
      else
      {
        Bullet bullet2 = new Bullet(this.Scene, this.Transform.Position + new Vector2(14f, -15f), new Vector2(0.3f, 0.0f));
      }
    }

    private void PlayAttackAnimation()
    {
      if (this.CurrentFaceDirection == Direction.WEST)
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AttackLeft");
      else
        this.GetComponent<AnimationStateMachine>().PlayAnimation("AttackRight");
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (!(otherEntity is Hero))
        return;
      this.Target = otherEntity as Hero;
      this.GetComponent<TrunkAIStateMachine>().ChangeState<TrunkAttackState>();
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (!(otherEntity is Hero))
        return;
      this.Target = (Hero) null;
      this.GetComponent<TrunkAIStateMachine>().ChangeState<TrunkPatrolState>();
    }

    public override void Update() => base.Update();
  }
}
