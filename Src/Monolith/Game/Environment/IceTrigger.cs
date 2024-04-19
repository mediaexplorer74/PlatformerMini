
// Type: ForestPlatformerExample.IceTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class IceTrigger : Entity
  {
    public IceTrigger(AbstractScene scene, int width, int height, Vector2 position)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      if (width == 0 || height == 0)
        throw new Exception("Invalid ice trigger size");
      this.Visible = false;
      this.Active = true;
      this.AddTag("Environment");
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, Vector2.Zero));
      this.AddTriggeredAgainst(typeof (Hero));
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).OnIce = true;
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).OnIce = false;
      base.OnLeaveTrigger(triggerTag, otherEntity);
    }
  }
}
