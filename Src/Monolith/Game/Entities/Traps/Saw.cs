// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Saw
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class Saw : PhysicalEntity
  {
    private float ROTATION_RATE = 0.2f;
    private Sprite sprite;
    private float Speed = 0.2f;

    public Saw(AbstractScene scene, Vector2 position, bool horizontalMovement = true, Vector2 pivot = default (Vector2))
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.AddTag(nameof (Saw));
      this.CanFireTriggers = true;
      this.CheckGridCollisions = false;
      this.HorizontalFriction = 0.0f;
      this.VerticalFriction = 0.0f;
      this.Pivot = pivot;
      this.sprite = new Sprite((Entity) this, Assets.GetTexture(nameof (Saw)));
      this.sprite.Origin = new Vector2((float) (this.sprite.SourceRectangle.Width / 2), (float) (this.sprite.SourceRectangle.Height / 2));
      this.AddComponent<Sprite>(this.sprite);
      this.HasGravity = false;
      if (horizontalMovement)
        this.Transform.VelocityX = this.Speed;
      else
        this.Transform.VelocityY = this.Speed;
      this.AddComponent<CircleCollisionComponent>(new CircleCollisionComponent((IColliderEntity) this, 19f, new Vector2((float) this.sprite.SourceRectangle.Width, (float) this.sprite.SourceRectangle.Height) * -this.Pivot));
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
      this.sprite.Rotation += this.ROTATION_RATE;
    }

    public void ChangeDirection() => this.Transform.Velocity *= -1f;
  }
}
