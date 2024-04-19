
// Type: MonolithEngine.GridCollisionChecker
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class GridCollisionChecker
  {
    private Dictionary<Vector2, StaticCollider> objects = new Dictionary<Vector2, StaticCollider>();
    private Dictionary<StaticCollider, Vector2> objectPositions = new Dictionary<StaticCollider, Vector2>();
    private ICollection<Direction> whereToCheck;
    private List<(StaticCollider, Direction)> allCollisionsResult = new List<(StaticCollider, Direction)>();
    private List<Direction> tagCollisionResult = new List<Direction>();
    private static readonly List<Direction> basicDirections = new List<Direction>()
    {
      Direction.CENTER,
      Direction.WEST,
      Direction.EAST,
      Direction.NORTH,
      Direction.SOUTH
    };

    public void Add(StaticCollider gameObject)
    {
      this.objectPositions[gameObject] = gameObject.Transform.GridCoordinates;
      this.objects.Add(gameObject.Transform.GridCoordinates, gameObject);
    }

    public StaticCollider GetColliderAt(Vector2 position)
    {
      return !this.objects.ContainsKey(position) ? (StaticCollider) null : this.objects[position];
    }

    public List<Direction> CollidesWithTag(
      IGameObject entity,
      string tag,
      ICollection<Direction> directionsToCheck = null)
    {
      this.tagCollisionResult.Clear();
      this.whereToCheck = directionsToCheck ?? (ICollection<Direction>) GridCollisionChecker.basicDirections;
      foreach (Direction direction in (IEnumerable<Direction>) this.whereToCheck)
      {
        if (this.objects.ContainsKey(GridUtil.GetGridCoord(entity, direction)) && this.objects[GridUtil.GetGridCoord(entity, direction)].HasTag(tag) && this.IsExactCollision(entity, direction))
          this.tagCollisionResult.Add(direction);
      }
      return this.tagCollisionResult;
    }

    public List<(StaticCollider, Direction)> HasGridCollisionAt(
      IGameObject entity,
      ICollection<Direction> directionsToCheck = null)
    {
      this.allCollisionsResult.Clear();
      this.whereToCheck = directionsToCheck ?? (ICollection<Direction>) GridCollisionChecker.basicDirections;
      foreach (Direction direction in (IEnumerable<Direction>) this.whereToCheck)
      {
        if (this.objects.ContainsKey(GridUtil.GetGridCoord(entity, direction)) && this.IsExactCollision(entity, direction))
          this.allCollisionsResult.Add((this.objects[GridUtil.GetGridCoord(entity, direction)], direction));
      }
      return this.allCollisionsResult;
    }

    private bool IsExactCollision(IGameObject entity, Direction direction)
    {
      switch (direction)
      {
        case Direction.CENTER:
          return true;
        case Direction.NORTH:
          return (double) entity.Transform.InCellLocation.Y <= (double) (entity as Entity).GetCollisionOffset(direction);
        case Direction.SOUTH:
          return (double) entity.Transform.InCellLocation.Y >= (double) (entity as Entity).GetCollisionOffset(direction);
        case Direction.WEST:
          return (double) entity.Transform.InCellLocation.X <= (double) (entity as Entity).GetCollisionOffset(direction);
        case Direction.EAST:
          return (double) entity.Transform.InCellLocation.X >= (double) (entity as Entity).GetCollisionOffset(direction);
        case Direction.NORTHWEST:
          return this.IsExactCollision(entity, Direction.NORTH) && this.IsExactCollision(entity, Direction.WEST);
        case Direction.NORTHEAST:
          return this.IsExactCollision(entity, Direction.NORTH) && this.IsExactCollision(entity, Direction.EAST);
        case Direction.SOUTHWEST:
          return this.IsExactCollision(entity, Direction.SOUTH) && this.IsExactCollision(entity, Direction.WEST);
        case Direction.SOUTHEAST:
          return this.IsExactCollision(entity, Direction.SOUTH) && this.IsExactCollision(entity, Direction.EAST);
        default:
          throw new Exception("Uknown direction");
      }
    }

    public bool HasBlockingColliderAt(IGameObject entity, Direction direction)
    {
      Vector2 gridCoord = GridUtil.GetGridCoord(entity, direction);
      return this.objects.ContainsKey(gridCoord) && this.objects[gridCoord].BlocksMovementFrom(direction);
    }

    public bool HasBlockingColliderAt(Vector2 gridCoord)
    {
      return this.objects.ContainsKey(gridCoord) && this.objects[gridCoord].BlocksMovementFrom(Direction.CENTER);
    }

    public void Remove(StaticCollider gameObject)
    {
      this.objects.Remove(this.objectPositions[gameObject]);
    }

    public void Destroy()
    {
      this.objects.Clear();
      this.objectPositions.Clear();
      this.allCollisionsResult.Clear();
      this.tagCollisionResult.Clear();
    }
  }
}
