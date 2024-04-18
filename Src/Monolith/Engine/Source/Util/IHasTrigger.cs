// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IHasTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System.Collections.Generic;


namespace MonolithEngine
{
  public interface IHasTrigger : IGameObject
  {
    bool IsDestroyed { get; }

    ICollection<ITrigger> GetTriggers();

    bool CanFireTriggers { get; set; }

    void OnEnterTrigger(string triggerTag, IGameObject otherEntity);

    void OnLeaveTrigger(string triggerTag, IGameObject otherEntity);
  }
}
