// Type: ForestPlatformerExample.Spikes
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class Spikes : Entity
  {
    public Direction Direction;

    public Spikes(AbstractScene scene, Vector2 position, int length, Direction direction)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.Direction = direction;
      this.AddTag(nameof (Spikes));
      TileGroup tileGroup = new TileGroup(length, Config.GRID);
      Texture2D texture2D = Assets.GetTexture2D("ForestTileset");
      for (int x = 0; x < length; x += Config.GRID)
      {
        Rectangle sourceRectangle = x != 0 ? (x != length - Config.GRID ? new Rectangle(256, 368, Config.GRID, Config.GRID) : new Rectangle(272, 368, Config.GRID, Config.GRID)) : new Rectangle(240, 368, Config.GRID, Config.GRID);
        for (int y = 0; y < Config.GRID; y += Config.GRID)
          tileGroup.AddTile(texture2D, new Vector2((float) x, (float) y), sourceRectangle);
      }
      Rectangle rectangle = new Rectangle(0, 0, length, Config.GRID);
      Sprite newComponent;
      if (this.Direction == Direction.SOUTH || this.Direction == Direction.NORTH)
      {
        SpriteEffects spriteEffects = direction == Direction.SOUTH ? SpriteEffects.FlipVertically : SpriteEffects.None;
        newComponent = new Sprite((Entity) this, new MonolithTexture(tileGroup.GetTexture(), rectangle, rectangle));
        newComponent.SpriteEffect = spriteEffects;
        this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, (float) length, (float) Config.GRID));
      }
      else
      {
        if (this.Direction != Direction.WEST && this.Direction != Direction.EAST)
          throw new Exception("Wrong spikes orientation");
        Vector2 vector2 = Vector2.Zero;
        float rad;
        if (this.Direction == Direction.EAST)
        {
          rad = MathUtil.DegreesToRad(90f);
          vector2 = new Vector2((float) -Config.GRID, 0.0f);
        }
        else
        {
          rad = MathUtil.DegreesToRad(-90f);
          vector2 = new Vector2(0.0f, (float) -length);
        }
        MonolithTexture texture = new MonolithTexture(tileGroup.GetTexture(), rectangle, rectangle);
        float num = rad;
        Vector2 drawOffset = vector2;
        double rotation = (double) num;
        Vector2 origin = new Vector2();
        newComponent = new Sprite((Entity) this, texture, drawOffset, (float) rotation, origin);
        this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, (float) Config.GRID, (float) length));
      }
      this.AddComponent<Sprite>(newComponent);
      this.DrawPriority = 1f;
    }
  }
}
