// Type: ForestPlatformerExample.AIUtil
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class AIUtil
  {
    private static bool changeDirectionAllowed;

    public static bool WillColliderOrFall(AbstractEnemy enemy)
    {
      if (enemy.CurrentFaceDirection == Direction.WEST)
      {
        StaticCollider colliderAt = enemy.Scene.GridCollisionChecker.GetColliderAt(GridUtil.GetLeftBelowGrid((IGameObject) enemy));
        return enemy.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) enemy, Direction.WEST) || colliderAt == null || !colliderAt.BlocksMovementFrom(Direction.SOUTH);
      }
      if (enemy.CurrentFaceDirection != Direction.EAST)
        throw new Exception("Wrong CurrentFaceDirection for enemy!");
      StaticCollider colliderAt1 = enemy.Scene.GridCollisionChecker.GetColliderAt(GridUtil.GetRightBelowGrid((IGameObject) enemy));
      return enemy.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) enemy, Direction.EAST) || colliderAt1 == null || !colliderAt1.BlocksMovementFrom(Direction.SOUTH);
    }

    public static void Patrol(bool checkCollisions, AbstractEnemy enemy, float waitingTime = 0.0f)
    {
      if ((double) enemy.Transform.VelocityY > 0.0)
        return;
      Direction direction = enemy.CurrentFaceDirection;
      int id;
      if (checkCollisions && AIUtil.WillColliderOrFall(enemy))
      {
        if ((double) waitingTime > 0.0 && !AIUtil.changeDirectionAllowed)
        {
          id = enemy.GetID();
          Timer.SetTimer("CARROT_WAIT" + id.ToString(), waitingTime);
          enemy.Transform.Velocity = Vector2.Zero;
          AIUtil.changeDirectionAllowed = true;
        }
        if (enemy.CurrentFaceDirection == Direction.WEST)
          direction = Direction.EAST;
        else if (enemy.CurrentFaceDirection == Direction.EAST)
          direction = Direction.WEST;
      }
      switch (direction)
      {
        case Direction.WEST:
          enemy.MoveDirection = -1;
          break;
        case Direction.EAST:
          enemy.MoveDirection = 1;
          break;
      }
      id = enemy.GetID();
      if (Timer.IsSet("CARROT_WAIT" + id.ToString()))
        return;
      enemy.CurrentFaceDirection = direction;
            //RnD
      //enemy.Transform.VelocityX += enemy.CurrentSpeed 
      //          * (float) enemy.MoveDirection * Globals.FixedUpdateMultiplier;
      AIUtil.changeDirectionAllowed = false;
    }
  }
}
