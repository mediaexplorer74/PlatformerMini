// Type: MonolithEngine.AIState`1
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

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
