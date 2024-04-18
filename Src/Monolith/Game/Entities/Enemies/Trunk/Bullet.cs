// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Bullet
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class Bullet : PhysicalEntity
  {
    public Bullet(AbstractScene scene, Vector2 position, Vector2 speed)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.DrawPriority = 3f;
      this.AddTag("Projectile");
      Sprite newComponent = new Sprite((Entity) this, Assets.GetTexture("TrunkBullet"));
      if ((double) speed.X > 0.0)
        newComponent.SpriteEffect = SpriteEffects.FlipHorizontally;
      this.AddComponent<Sprite>(newComponent);
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 3f, new Vector2(8f, 8f)));
      this.CheckGridCollisions = false;
      this.HorizontalFriction = 0.0f;
      this.VerticalFriction = 0.0f;
      this.HasGravity = false;
      this.Transform.Velocity = speed;
      this.Transform.Position = position;
      Timer.TriggerAfter(5000f, new Action(((GameObject) this).Destroy));
    }
  }
}
