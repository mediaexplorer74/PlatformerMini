
// Type: MonolithEngine.AbstractAnimation
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public abstract class AbstractAnimation : IAnimation
  {
    internal int CurrentFrame;
    internal int TotalFrames;
    internal int Framerate;
    private double delay;
    private double currentDelay;
    public Color Color = Color.White;
    public float Scale = 1f;
    public Vector2 Offset = Vector2.Zero;
    public bool Looping = true;
    public bool Running;
    public Action StoppedCallback;
    public Action StartedCallback;
    public Action AnimationSwitchCallback;
    private bool stopActionCalled;
    private bool startActionCalled;
    public Func<bool> AnimationPauseCondition;
    public Action<int> EveryFrameAction;
    private Dictionary<int, Action<int>> frameActions = new Dictionary<int, Action<int>>();
    public Vector2 Origin;
    public int StartFrame;
    internal Rectangle SourceRectangle;

    protected Entity Parent { get; set; }

    protected SpriteEffects SpriteEffect { get; set; }

    public int EndFrame
    {
      get => this.TotalFrames;
      set => this.TotalFrames = value;
    }

    public float DrawPriority { get; set; }

    public AbstractAnimation(
      Entity parent,
      int totalFrames,
      int framerate,
      SpriteEffects spriteEffect = SpriteEffects.None,
      Action startCallback = null,
      Action stopCallback = null)
    {
      this.Framerate = framerate >= 1 ? framerate : throw new Exception("Invalid framerate!");
      this.Parent = parent;
      this.CurrentFrame = this.StartFrame;
      this.TotalFrames = totalFrames;
      this.SpriteEffect = spriteEffect;
      this.StartedCallback = startCallback;
      this.StoppedCallback = stopCallback;
      this.delay = (double) Config.FIXED_UPDATE_FPS / (double) framerate;
    }

    public bool Finished() => !this.Running;

    protected void Copy(AbstractAnimation anim)
    {
      anim.Looping = this.Looping;
      anim.Scale = this.Scale;
      anim.Offset = this.Offset;
      anim.delay = this.delay;
      anim.SpriteEffect = this.SpriteEffect;
      anim.TotalFrames = this.TotalFrames;
      anim.StartFrame = this.StartFrame;
      anim.EndFrame = this.EndFrame;
      anim.EveryFrameAction = this.EveryFrameAction;
      anim.frameActions = this.frameActions;
      anim.StoppedCallback = this.StoppedCallback;
      anim.StartedCallback = this.StartedCallback;
      anim.AnimationSwitchCallback = this.AnimationSwitchCallback;
    }

    public virtual void Play(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.GetTexture(), this.Parent.DrawPosition + this.Offset, new Rectangle?(this.SourceRectangle), this.Color, this.Parent.DrawRotation, this.Origin, this.Scale, this.SpriteEffect, 0.0f);
    }

    internal abstract Texture2D GetTexture();

    public void Update()
    {
      if (!this.Running || this.AnimationPauseCondition != null && this.AnimationPauseCondition())
        return;
      if (this.CurrentFrame == this.StartFrame && !this.Looping)
        this.InvokeStartedCallback();
      if (this.currentDelay >= this.delay)
      {
        ++this.CurrentFrame;
        Action<int> everyFrameAction = this.EveryFrameAction;
        if (everyFrameAction != null)
          everyFrameAction(this.CurrentFrame);
        if (this.frameActions.ContainsKey(this.CurrentFrame))
          this.frameActions[this.CurrentFrame](this.CurrentFrame);
        this.currentDelay -= this.delay;
      }
      else
        this.currentDelay += (double) Globals.FixedUpdateMultiplier;
      if (this.CurrentFrame != this.TotalFrames)
        return;
      if (!this.Looping)
      {
        this.Stop();
        this.InvokeStoppedCallback();
      }
      else
        this.Init();
    }

    public int GetCurrentFrame() => this.CurrentFrame;

    public void InvokeStoppedCallback()
    {
      if (this.stopActionCalled)
        return;
      Action stoppedCallback = this.StoppedCallback;
      if (stoppedCallback != null)
        stoppedCallback();
      this.stopActionCalled = true;
    }

    public void InvokeStartedCallback()
    {
      if (this.startActionCalled)
        return;
      Action startedCallback = this.StartedCallback;
      if (startedCallback != null)
        startedCallback();
      this.startActionCalled = true;
    }

    public void Init(int? startFrame = null)
    {
      this.CurrentFrame = startFrame.HasValue ? startFrame.Value : this.StartFrame;
      this.Running = true;
      this.startActionCalled = false;
      this.stopActionCalled = false;
    }

    public void Stop()
    {
      this.CurrentFrame = this.TotalFrames - 1;
      this.Running = false;
    }

    public void Flip()
    {
      if (this.SpriteEffect == SpriteEffects.None)
        this.SpriteEffect = SpriteEffects.FlipHorizontally;
      else
        this.SpriteEffect = SpriteEffects.None;
    }

    public void AddFrameAction(int frame, Action<int> action)
    {
      if (this.frameActions.ContainsKey(frame))
        this.frameActions[frame] += action;
      else
        this.frameActions.Add(frame, action);
    }

    public abstract void Destroy();
  }
}
