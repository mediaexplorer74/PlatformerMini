// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.AbstractDestroyable
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class AbstractDestroyable : PhysicalEntity
  {
    private readonly string DESTROY_AMINATION = "Destroy";
    protected float RotationRate;
    private bool hasDestroyAnimation;
    protected Random random;

    public AbstractDestroyable(AbstractScene scene, Vector2 position)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.random = new Random();
    }

    public virtual void Die()
    {
      if (!this.hasDestroyAnimation)
      {
        this.HorizontalFriction = 0.99f;
        this.VerticalFriction = 0.99f;
        int num = this.random.Next(0, 11);
        Vector2 vector2 = new Vector2(0.1f, -0.1f);
        this.RotationRate = 0.1f;
        if (num % 2 == 0)
        {
          vector2.X *= -1f;
          this.RotationRate *= -1f;
        }
        this.CheckGridCollisions = false;
        this.CollisionsEnabled = false;
        this.Transform.Velocity += vector2;
        Timer.TriggerAfter(3000f, new Action(((GameObject) this).Destroy));
      }
      else
      {
        if (this.GetComponent<AnimationStateMachine>() == null || !this.GetComponent<AnimationStateMachine>().HasAnimation(this.DESTROY_AMINATION + this.CurrentFaceDirection.ToString()))
          return;
        this.GetComponent<AnimationStateMachine>().PlayAnimation(this.DESTROY_AMINATION + this.CurrentFaceDirection.ToString());
      }
    }

    public void SetDestroyAnimation(AbstractAnimation destroyAnimation, Direction direction = Direction.CENTER)
    {
      this.hasDestroyAnimation = true;
      if (this.GetComponent<AnimationStateMachine>() == null)
        this.AddComponent<AnimationStateMachine>(new AnimationStateMachine());
      destroyAnimation.StartedCallback += (Action) (() => this.DisableCollisions());
      destroyAnimation.StoppedCallback += new Action(((GameObject) this).Destroy);
      destroyAnimation.Looping = false;
      this.GetComponent<AnimationStateMachine>().RegisterAnimation(this.DESTROY_AMINATION + direction.ToString(), destroyAnimation, (Func<bool>) (() => false));
    }

    private void DisableCollisions()
    {
      this.CollisionsEnabled = false;
      this.CanFireTriggers = false;
    }
  }
}
