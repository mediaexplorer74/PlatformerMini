// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Timer
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MonolithEngine
{
  public class Timer
  {
    private static Dictionary<Action, float> triggeredActions = new Dictionary<Action, float>();
    private static List<Timer.RepeatedAction> repeatedActions = new List<Timer.RepeatedAction>();
    private static Dictionary<string, float> timers = new Dictionary<string, float>();
    private static List<Timer.LerpAction> lerps = new List<Timer.LerpAction>();

    public static void TriggerAfter(float delayInMs, Action action, bool overrideExisting = true)
    {
      if (overrideExisting)
      {
        Timer.triggeredActions[action] = delayInMs;
      }
      else
      {
        if (Timer.triggeredActions.ContainsKey(action))
          return;
        Timer.triggeredActions[action] = delayInMs;
      }
    }

    public static void SetTimer(string name, float duration, bool overrideExisting = true)
    {
      if (overrideExisting)
      {
        Timer.timers[name] = duration;
      }
      else
      {
        if (Timer.timers.ContainsKey(name))
          return;
        Timer.timers[name] = duration;
      }
    }

    public static void Update(float elapsedTime)
    {
      foreach (Action key in Timer.triggeredActions.Keys.ToList<Action>())
      {
        if ((double) Timer.triggeredActions[key] - (double) elapsedTime <= 0.0)
        {
          key();
          Timer.triggeredActions.Remove(key);
        }
        else
          Timer.triggeredActions[key] -= elapsedTime;
      }
      foreach (string key in Timer.timers.Keys.ToList<string>())
      {
        if ((double) Timer.timers[key] - (double) elapsedTime <= 0.0)
          Timer.timers.Remove(key);
        else
          Timer.timers[key] -= elapsedTime;
      }
      foreach (Timer.LerpAction lerpAction in Timer.lerps.ToList<Timer.LerpAction>())
      {
        if ((double) lerpAction.progress <= (double) lerpAction.duration)
          lerpAction.callback(MathHelper.Lerp(lerpAction.from, lerpAction.to, lerpAction.progress / lerpAction.duration));
        else
          Timer.lerps.Remove(lerpAction);
        lerpAction.progress += elapsedTime;
      }
      foreach (Timer.RepeatedAction repeatedAction in Timer.repeatedActions.ToList<Timer.RepeatedAction>())
      {
        if ((double) repeatedAction.progress <= (double) repeatedAction.duration)
        {
          repeatedAction.action(elapsedTime);
          repeatedAction.progress += elapsedTime;
        }
        else
          Timer.repeatedActions.Remove(repeatedAction);
      }
    }

    public static void Clear()
    {
      Timer.triggeredActions.Clear();
      Timer.repeatedActions.Clear();
      Timer.timers.Clear();
      Timer.lerps.Clear();
    }

    public static bool IsSet(string timer) => Timer.timers.ContainsKey(timer);

    public static void CancelAction(Action action)
    {
      Timer.triggeredActions.RemoveIfExists<Action, float>(action);
    }

    public static void Repeat(float duration, Action<float> action)
    {
      Timer.repeatedActions.Add(new Timer.RepeatedAction(0.0f, duration, action));
    }

    public static void Lerp(float duration, float from, float to, Action<float> callback)
    {
      Timer.lerps.Add(new Timer.LerpAction(0.0f, duration, from, to, callback));
    }

    private class RepeatedAction
    {
      public float progress;
      public float duration;
      public Action<float> action;

      public RepeatedAction(float progress, float duration, Action<float> action)
      {
        this.progress = progress;
        this.action = action;
        this.duration = duration;
      }
    }

    private class LerpAction
    {
      public Action<float> callback;
      public float progress;
      public float duration;
      public float from;
      public float to;

      public LerpAction(
        float progress,
        float endTime,
        float from,
        float to,
        Action<float> callback)
      {
        this.callback = callback;
        this.progress = progress;
        this.duration = endTime;
        this.from = from;
        this.to = to;
      }
    }
  }
}
