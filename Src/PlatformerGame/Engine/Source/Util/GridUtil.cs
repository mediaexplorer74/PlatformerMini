// Decompiled with JetBrains decompiler
// Type: MonolithEngine.GridUtil
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace MonolithEngine
{
  public class GridUtil
  {
    public static Vector2 GetRightGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X + 1f, entity.Transform.GridCoordinates.Y);
    }

    public static Vector2 GetLeftGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X - 1f, entity.Transform.GridCoordinates.Y);
    }

    public static Vector2 GetUpperGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X, entity.Transform.GridCoordinates.Y - 1f);
    }

    public static Vector2 GetUpperRightGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X + 1f, entity.Transform.GridCoordinates.Y - 1f);
    }

    public static Vector2 GetUpperLeftGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X - 1f, entity.Transform.GridCoordinates.Y - 1f);
    }

    public static Vector2 GetBelowGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X, entity.Transform.GridCoordinates.Y + 1f);
    }

    public static Vector2 GetRightBelowGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X + 1f, entity.Transform.GridCoordinates.Y + 1f);
    }

    public static Vector2 GetLeftBelowGrid(IGameObject entity)
    {
      return new Vector2(entity.Transform.GridCoordinates.X - 1f, entity.Transform.GridCoordinates.Y + 1f);
    }

    public static Vector2 GetGridCoord(IGameObject entity, Direction direction)
    {
      switch (direction)
      {
        case Direction.CENTER:
          return entity.Transform.GridCoordinates;
        case Direction.NORTH:
          return GridUtil.GetUpperGrid(entity);
        case Direction.SOUTH:
          return GridUtil.GetBelowGrid(entity);
        case Direction.WEST:
          return GridUtil.GetLeftGrid(entity);
        case Direction.EAST:
          return GridUtil.GetRightGrid(entity);
        case Direction.NORTHWEST:
          return GridUtil.GetUpperLeftGrid(entity);
        case Direction.NORTHEAST:
          return GridUtil.GetUpperRightGrid(entity);
        case Direction.SOUTHWEST:
          return GridUtil.GetLeftBelowGrid(entity);
        case Direction.SOUTHEAST:
          return GridUtil.GetRightBelowGrid(entity);
        default:
          throw new Exception("Unknown direction!");
      }
    }
  }
}
