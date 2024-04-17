// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.SlideWall
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class SlideWall : Entity
  {
    public SlideWall(AbstractScene scene, Vector2 position, int width, int height)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      if (width == 0 || height == 0)
        throw new Exception("Invalid slide wall dimensions!");
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 
          (float) width, (float) height));
      this.AddTag("Environment");
      this.Active = false;
      this.Visible = false;
    }
  }
}
