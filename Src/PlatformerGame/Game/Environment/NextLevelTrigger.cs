// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.NextLevelTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;

#nullable disable
namespace ForestPlatformerExample
{
  internal class NextLevelTrigger : Entity
  {
    public NextLevelTrigger(AbstractScene scene, Vector2 position, int width, int height)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.Visible = false;
      this.Active = true;
      this.AddTag("Environment");
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, Vector2.Zero));
      this.AddTriggeredAgainst(typeof (Hero));
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).LevelEndReached = true;
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
      {
        if ((otherEntity as Hero).ReadyForNextLevel)
          this.Scene.Finish();
        else
          (otherEntity as Hero).LevelEndReached = false;
      }
      base.OnLeaveTrigger(triggerTag, otherEntity);
    }
  }
}
