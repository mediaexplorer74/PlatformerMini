// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Ladder
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class Ladder : Entity
  {
    public Ladder(AbstractScene scene, Vector2 position, int width, int height)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      if (width == 0 || height == 0)
        throw new Exception("Invalid ladder dimensions!");
      this.AddTag("Environment");
      this.Active = true;
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, Vector2.Zero, nameof (Ladder)));
      this.AddTriggeredAgainst(typeof (Hero));
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).EnterLadder(this);
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).LeaveLadder();
      base.OnLeaveTrigger(triggerTag, otherEntity);
    }
  }
}
