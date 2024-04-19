
// Type: MonolithEngine.StaticCollider
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class StaticCollider : GameObject
  {
    private AbstractScene scene;
    private HashSet<Direction> blockedFrom = new HashSet<Direction>();
    public bool BlocksMovement = true;

    public StaticCollider(AbstractScene scene, Vector2 gridPosition)
      : base()
    {
      StaticTransform staticTransform = new StaticTransform((IGameObject) this);
      staticTransform.GridCoordinates = gridPosition;
      this.Transform = (AbstractTransform) staticTransform;
      this.scene = scene;
      scene.GridCollisionChecker.Add(this);
    }

    public override void Destroy() => this.scene.GridCollisionChecker.Remove(this);

    public void AddBlockedDirection(Direction direction) => this.blockedFrom.Add(direction);

    public void RemoveBlockedDirection(Direction direction) => this.blockedFrom.Remove(direction);

    public bool IsBlockedFrom(Direction direction)
    {
      return this.blockedFrom.Count == 0 || this.blockedFrom.Contains(direction);
    }

    public bool BlocksMovementFrom(Direction direction)
    {
      return this.BlocksMovement && this.IsBlockedFrom(direction);
    }

    public override bool IsAlive() => throw new NotImplementedException();

    public override void AddChild(IGameObject gameObject) => throw new NotImplementedException();

    public override void RemoveChild(IGameObject gameObject) => throw new NotImplementedException();
  }
}
