// Decompiled with JetBrains decompiler
// Type: MonolithEngine.AIStateMachine`1
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class AIStateMachine<T> : IComponent, IUpdatableComponent where T : IGameObject
  {
    public Dictionary<Type, AIState<T>> states = new Dictionary<Type, AIState<T>>();
    private AIState<T> currentState;
    public float TimeSpentInCurrentState;
    public bool Enabled = true;

    public bool UniquePerEntity { get; set; }

    public AIStateMachine(AIState<T> initialState = null)
    {
      this.UniquePerEntity = true;
      if (initialState == null)
        return;
      this.states.Add(initialState.GetType(), initialState);
      this.currentState = initialState;
      this.currentState.Begin();
    }

    public void AddState(AIState<T> state) => this.states.Add(state.GetType(), state);

    public void RemoveState<R>() where R : AIState<T> => this.states.Remove(typeof (R));

    public R GetState<R>() where R : AIState<T> => (R) this.states[typeof (R)];

    public R ChangeState<R>() where R : AIState<T>
    {
      Type type = typeof (R);
      if (this.currentState.GetType().Equals(type))
        return (R) this.currentState;
      if (this.currentState != null)
        this.currentState.End();
      this.TimeSpentInCurrentState = 0.0f;
      this.currentState = this.states[type];
      this.currentState.Begin();
      return (R) this.currentState;
    }

    public void Update()
    {
      if (!this.Enabled || this.currentState == null)
        return;
      this.TimeSpentInCurrentState += Globals.FixedUpdateMultiplier;
      this.currentState.FixedUpdate();
    }

    public AIState<T> GetCurrentState() => this.currentState;

    public void PreUpdate()
    {
    }

    public void PostUpdate()
    {
    }

    public Type GetComponentType() => this.GetType();
  }
}
