// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.MovingPlatformTurner
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;

#nullable disable
namespace ForestPlatformerExample
{
  internal class MovingPlatformTurner : Entity
  {
    public Direction TurnDirection;

    public MovingPlatformTurner(AbstractScene scene, Vector2 position, Direction turnDirection)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.TurnDirection = turnDirection;
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, (float) Config.GRID, (float) Config.GRID));
      this.AddTag("PlatformTurner");
    }
  }
}
