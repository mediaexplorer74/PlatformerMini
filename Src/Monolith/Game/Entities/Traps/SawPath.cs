// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.SawPath
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;

#nullable disable
namespace ForestPlatformerExample
{
  internal class SawPath : Entity
  {
    public SawPath(AbstractScene scene, Vector2 position, int width, int height)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.Visible = false;
      this.Active = true;
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, Vector2.Zero));
      this.AddTriggeredAgainst(typeof (Saw));
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Saw)
        (otherEntity as Saw).ChangeDirection();
      base.OnLeaveTrigger(triggerTag, otherEntity);
    }
  }
}
