// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.MovingPlatform
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;

#nullable disable
namespace ForestPlatformerExample
{
  internal class MovingPlatform : PhysicalEntity
  {
    private static readonly float SPEED = 0.2f;
    private float speedX = MovingPlatform.SPEED;
    private float speedY;
    private int directionX = -1;
    private int directionY = -1;

    public MovingPlatform(AbstractScene scene, Vector2 startPosition, int width, int height)
      : base(scene.LayerManager.EntityLayer, startPosition: startPosition)
    {
      this.CheckGridCollisions = false;
      this.HasGravity = false;
      this.Active = true;
      this.HorizontalFriction = 0.0f;
      this.VerticalFriction = 0.0f;
      this.BumpFriction = 0.0f;
      
      //RnD
      //this.Transform.VelocityX = this.speedX * (float) this.directionX;
      //this.Transform.VelocityY = this.speedY * (float) this.directionY;
      
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, (float) width, (float) height));
      TileGroup tileGroup = new TileGroup(width, height);
      Texture2D texture2D = Assets.GetTexture2D("ForestTileset");
      for (int x = 0; x < width; x += Config.GRID)
      {
        for (int y = 0; y < height; y += Config.GRID)
          tileGroup.AddTile(texture2D, new Vector2((float) x, (float) y), new Rectangle(304, 288, Config.GRID, Config.GRID));
      }
      this.AddComponent<Sprite>(new Sprite((Entity) this, new MonolithTexture(tileGroup.GetTexture())));
      this.AddCollisionAgainst(typeof (MovingPlatformTurner));
      this.AddTag("Mountable");
      this.AddTag(nameof (MovingPlatform));
    }

    public override void OnCollisionStart(IGameObject otherCollider)
    {
      if (otherCollider is MovingPlatformTurner)
      {
        MovingPlatformTurner movingPlatformTurner = otherCollider as MovingPlatformTurner;
        if (movingPlatformTurner.TurnDirection == Direction.WEST)
        {
          this.directionX = -1;
          this.speedY = 0.0f;
          this.speedX = MovingPlatform.SPEED;
        }
        else if (movingPlatformTurner.TurnDirection == Direction.EAST)
        {
          this.directionX = 1;
          this.speedY = 0.0f;
          this.speedX = MovingPlatform.SPEED;
        }
        else if (movingPlatformTurner.TurnDirection == Direction.NORTH)
        {
          this.directionY = -1;
          this.speedX = 0.0f;
          this.speedY = MovingPlatform.SPEED;
        }
        else if (movingPlatformTurner.TurnDirection == Direction.SOUTH)
        {
          this.directionY = 1;
          this.speedX = 0.0f;
          this.speedY = MovingPlatform.SPEED;
        }
        this.Transform.VelocityX = this.speedX * (float) this.directionX;
        this.Transform.VelocityY = this.speedY * (float) this.directionY;
      }
      base.OnCollisionStart(otherCollider);
    }
  }
}
