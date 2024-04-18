// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IColliderEntity
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public interface IColliderEntity : IHasTrigger, IGameObject
  {
    bool CollisionsEnabled { get; set; }

    ICollisionComponent GetCollisionComponent();

    internal void CollisionStarted(IGameObject otherCollider, bool allowOverlap);

    internal void CollisionEnded(IGameObject otherCollider);

    internal Dictionary<Type, bool> GetCollidesAgainst();

    internal HashSet<Type> GetTriggeredAgainst();

    bool CheckGridCollisions { get; set; }
  }
}
