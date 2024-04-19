
// Type: ForestPlatformerExample.RespawnPoint
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class RespawnPoint : Entity
  {
    public RespawnPoint(AbstractScene scene, int width, int height, Vector2 position)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      if (width == 0 || height == 0)
        throw new Exception("Invalid respawn point trigger size");
      this.Visible = false;
      this.Active = true;
      this.AddTag("Environment");
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, new Vector2((float) (-width / 2), (float) (-height + Config.GRID))));
      this.AddTriggeredAgainst(typeof (Hero));
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).LastSpawnPoint = this.Transform.Position;
      base.OnEnterTrigger(triggerTag, otherEntity);
    }
  }
}
