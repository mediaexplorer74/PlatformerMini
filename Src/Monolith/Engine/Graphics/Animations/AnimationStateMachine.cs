
// Type: MonolithEngine.AnimationStateMachine


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MonolithEngine
{
  public class AnimationStateMachine : IComponent, IUpdatableComponent, IDrawableComponent
  {
    private List<AnimationStateMachine.StateAnimation> animations;
    private HashSet<(string, string)> transitions = new HashSet<(string, string)>();
    private AnimationStateMachine.StateAnimation currentAnimation;
    private Vector2 offset = Vector2.Zero;
    private AnimationStateMachine.StateAnimation animationOverride;
    private int? transitionFrame;

    public Vector2 Offset
    {
      get => this.offset;
      set
      {
        this.offset = value;
        foreach (AnimationStateMachine.StateAnimation animation in this.animations)
          animation.animation.Offset = this.offset;
      }
    }

    public bool UniquePerEntity { get; set; }

    public AnimationStateMachine()
    {
      this.animations = new List<AnimationStateMachine.StateAnimation>();
      this.UniquePerEntity = true;
    }

    public void RegisterAnimation(
      string stateName,
      AbstractAnimation animation,
      Func<bool> playCondition = null,
      int priority = 0)
    {
      if (playCondition == null)
        playCondition = (Func<bool>) (() => true);
      if (animation.Offset == Vector2.Zero)
        animation.Offset = this.Offset;
      this.animations.Add(new AnimationStateMachine.StateAnimation(stateName, animation, playCondition, priority));
      this.animations.Sort((Comparison<AnimationStateMachine.StateAnimation>) ((a, b) => a.priority.CompareTo(b.priority) * -1));
    }

    public void PlayAnimation(string stateName)
    {
            try
            {
                foreach (AnimationStateMachine.StateAnimation animation in this.animations)
                {
                    if (animation.state.Equals(stateName))
                    {
                        this.animationOverride = animation;
                        this.animationOverride.animation.Init();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("[ex] AnimationStateMachine - Requested animation not found: " + ex.Message);
            }
      //throw new Exception("Requested animation not found");
    }

    public bool HasAnimation(string state)
    {
      foreach (AnimationStateMachine.StateAnimation animation in this.animations)
      {
        if (animation.state.Equals(state))
          return true;
      }
      return false;
    }

    public AbstractAnimation GetAnimation(string state)
    {
      foreach (AnimationStateMachine.StateAnimation animation in this.animations)
      {
        if (animation.state.Equals(state))
          return animation.animation;
      }
      return (AbstractAnimation) null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (this.animations.Count == 0)
        return;
      this.Play(spriteBatch);
    }

    private void Play(SpriteBatch spriteBatch)
    {
      this.transitionFrame = new int?();
      if (this.animationOverride != null && this.animationOverride.animation.Finished())
        this.animationOverride = (AnimationStateMachine.StateAnimation) null;
      AnimationStateMachine.StateAnimation stateAnimation = this.Pop();
      if (stateAnimation == null)
        return;
      if (stateAnimation != this.currentAnimation)
      {
        if (this.currentAnimation != null)
        {
          if (this.transitions.Contains((this.currentAnimation.state, stateAnimation.state)))
            this.transitionFrame = new int?(this.currentAnimation.animation.GetCurrentFrame());
          this.currentAnimation.animation.Stop();
          this.currentAnimation.animation.InvokeStoppedCallback();
          Action animationSwitchCallback = this.currentAnimation.animation.AnimationSwitchCallback;
          if (animationSwitchCallback != null)
            animationSwitchCallback();
        }
        this.currentAnimation = stateAnimation;
        this.currentAnimation.animation.Init(this.transitionFrame);
        this.currentAnimation?.animation.InvokeStartedCallback();
      }
      this.currentAnimation.animation.Play(spriteBatch);
    }

    public void AddFrameTransition(string anim1, string anim2)
    {
      this.transitions.Add((anim1, anim2));
      this.transitions.Add((anim2, anim1));
    }

    public void Update()
    {
      if (this.animations.Count == 0 || this.currentAnimation == null)
        return;
      this.currentAnimation.animation.Update();
    }

    private AnimationStateMachine.StateAnimation Pop()
    {
      if (this.animationOverride != null)
        return this.animationOverride;
      foreach (AnimationStateMachine.StateAnimation animation in this.animations)
      {
        if (animation.function())
          return animation;
      }
      return (AnimationStateMachine.StateAnimation) null;
    }

    public void Destroy()
    {
      foreach (AnimationStateMachine.StateAnimation animation in this.animations)
        animation.animation.Destroy();
    }

    public string GetCurrentAnimationState()
    {
      return this.currentAnimation == null ? "NULL" : this.currentAnimation.state;
    }

    public void PreUpdate()
    {
    }

    public void PostUpdate()
    {
    }

    public Type GetComponentType() => this.GetType();

    private class StateAnimation
    {
      public string state;
      public Func<bool> function;
      public AbstractAnimation animation;
      public int priority;
      public bool played;

      public StateAnimation(
        string state,
        AbstractAnimation animation,
        Func<bool> function = null,
        int priority = 0)
      {
        this.state = state;
        this.animation = animation;
        this.priority = priority;
        this.function = function;
        this.played = false;
      }

      public override bool Equals(object obj)
      {
        return obj is AnimationStateMachine.StateAnimation stateAnimation && this.state == stateAnimation.state
                    && EqualityComparer<Func<bool>>.Default.Equals(this.function, stateAnimation.function) 
                    && EqualityComparer<AbstractAnimation>.Default.Equals(this.animation, stateAnimation.animation)
                    && this.priority == stateAnimation.priority;
      }

      public override int GetHashCode()
      {
          return HashCode.Combine<string, Func<bool>, AbstractAnimation, int>(this.state, this.function, this.animation, this.priority);
      }
    }
  }
}
