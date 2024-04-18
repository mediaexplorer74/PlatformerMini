// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IGameObject
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public interface IGameObject
  {
    AbstractTransform Transform { get; set; }

    IGameObject Parent { get; }

    ICollection<string> GetTags();

    bool HasTag(string tag);

    void AddChild(IGameObject gameObject);

    void RemoveChild(IGameObject gameObject);

    void Destroy();

    bool IsAlive();
  }
}
