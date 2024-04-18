// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.GameFinishTrophy
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class GameFinishTrophy : PhysicalEntity
  {
    public GameFinishTrophy(AbstractScene scene, Vector2 position, Vector2 pivot)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.AddTag("FinishTropy");
      this.HasGravity = false;
      this.DrawPriority = 5f;
      this.Pivot = pivot;
      this.CollisionOffsetBottom = 1f;
      this.AddComponent<Sprite>(new Sprite((Entity) this, Assets.GetTexture("FinishedTrophy")));
      Rectangle boundingBox = this.GetComponent<Sprite>().Texture.GetBoundingBox();
      Vector2 positionOffset = new Vector2((float) boundingBox.Width * -this.Pivot.X, (float) boundingBox.Height * -this.Pivot.Y);
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, this.GetComponent<Sprite>().Texture.GetBoundingBox(), positionOffset));
      this.Active = true;
      this.Visible = true;
    }
  }
}
