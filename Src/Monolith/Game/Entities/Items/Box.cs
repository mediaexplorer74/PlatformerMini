// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Box
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class Box : AbstractInteractive, IAttackable, IMovableItem
  {
    private int life = 2;
    private int bumps;
    private int currentBump = 1;
    private List<Coin> coins = new List<Coin>();

    public Box(AbstractScene scene, Vector2 position, int bumps = 1)
      : base(scene, position)
    {
      this.BumpFriction = 0.2f;
      this.DrawPriority = 2f;
      this.AddCollisionAgainst(typeof (Carrot));
      this.AddCollisionAgainst(typeof (Trunk));
      this.AddCollisionAgainst(typeof (Rock));
      this.AddCollisionAgainst(typeof (Ghost));
      this.AddCollisionAgainst(typeof (IceCream));
      this.AddTag(nameof (Box));
      this.bumps = this.currentBump = bumps;
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 20f, 15f, new Vector2(-10f, -15f)));
      this.CollisionOffsetBottom = 1f;
      this.CollisionOffsetRight = 0.5f;
      this.GravityValue /= 2f;
      this.HorizontalFriction = 0.8f;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      newComponent.Offset = new Vector2(0.0f, -16f);
      SpriteSheetAnimation animation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("BoxIdle"), 24);
      newComponent.RegisterAnimation("BoxIdle", (AbstractAnimation) animation1);
      SpriteSheetAnimation spriteSheetAnimation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("BoxHit"), 24);
      spriteSheetAnimation1.Looping = false;
      SpriteSheetAnimation animation2 = spriteSheetAnimation1;
      newComponent.RegisterAnimation("BoxHit", (AbstractAnimation) animation2);
      SpriteSheetAnimation destroyAnimation = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("BoxDestroy"), 24);
      SpriteSheetAnimation spriteSheetAnimation2 = destroyAnimation;
      spriteSheetAnimation2.StartedCallback = spriteSheetAnimation2.StartedCallback + (Action) (() => this.Pop());
      destroyAnimation.Looping = false;
      this.SetDestroyAnimation((AbstractAnimation) destroyAnimation);
      int num = this.random.Next(3, 7);
      for (int index = 0; index < num; ++index)
      {
        Coin coin = new Coin(this.Scene, new Vector2(-8f, -20f), 3, (float) this.random.Next(95, 99) / 100f);
        coin.Parent = (IGameObject) this;
        coin.Visible = false;
        coin.CollisionsEnabled = false;
        coin.Active = false;
        this.coins.Add(coin);
      }
    }

    public void Hit(Direction impactDirection)
    {
      if (this.life == 0)
      {
        this.Die();
      }
      else
      {
        --this.life;
        this.GetComponent<AnimationStateMachine>().PlayAnimation("BoxHit");
      }
    }

    public void Lift(Entity entity, Vector2 newPosition)
    {
      this.currentBump = this.bumps;
      this.DisablePysics();
      this.Parent = (IGameObject) entity;
      this.Transform.Position = newPosition;
    }

    public void PutDown(Entity entity, Vector2 newPosition) => throw new NotImplementedException();

    public void Throw(Entity entity, Vector2 force)
    {
      this.Transform.Velocity = Vector2.Zero;
      this.Transform.Velocity += force;
      this.Parent = (IGameObject) null;
      this.EnablePhysics();
      this.FallSpeed = 0.0f;
    }

    private void EnablePhysics()
    {
      this.HasGravity = true;
      this.Active = true;
    }

    private void DisablePysics() => this.HasGravity = false;

    private void Pop()
    {
      AudioEngine.Play("BoxExplosionSound");
      foreach (Coin coin in this.coins)
      {
        Coin c = coin;
        c.Parent = (IGameObject) null;
        c.Active = true;
        c.Visible = true;
        c.Transform.Velocity += new Vector2((float) this.random.Next(-2, 4) * 0.1f, (float) this.random.Next(-5, 0) * 0.01f);
        c.SetBump(new Vector2(0.0f, -0.5f));
        Timer.TriggerAfter(500f, (Action) (() => c.CollisionsEnabled = true));
      }
      foreach (Camera camera in this.Scene.Cameras)
        camera.Shake();
      this.Die();
    }

    public override void OnCollisionStart(IGameObject otherCollider)
    {
      if (otherCollider.HasTag("Enemy") && this.IsMovingAtLeast(0.5f))
      {
        (otherCollider as AbstractEnemy).Die();
        this.Explode();
      }
      base.OnCollisionStart(otherCollider);
    }

    protected override void OnLand(Vector2 velocity)
    {
      if (this.currentBump < 1)
        return;
      this.Bump(new Vector2(0.0f, (float) (-this.currentBump * 2)));
      --this.currentBump;
    }

    private void Explode()
    {
      this.Transform.Velocity = Vector2.Zero;
      this.GravityValue = 0.0f;
      this.Die();
    }
  }
}
