// Decompiled with JetBrains decompiler
// Type: MonolithEngine.AIState`1
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

#nullable disable
namespace MonolithEngine
{
  public abstract class AIState<T> where T : IGameObject
  {
    protected T controlledEntity;

    public AIState(T controlledEntity) => this.controlledEntity = controlledEntity;

    public abstract void Begin();

    public abstract void FixedUpdate();

    public abstract void End();
  }
}
