
// Type: MonolithEngine.AbstractTransform
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;


namespace MonolithEngine
{
  public abstract class AbstractTransform : ITransform
  {
    protected IGameObject owner;
    internal Vector2 gridCoordinates;
    internal Vector2 InCellLocation;
    internal Vector2 PositionWithoutParent;

    internal Vector2 GridCoordinates
    {
      get
      {
        return this.owner.Parent == null ? this.gridCoordinates : this.owner.Parent.Transform.GridCoordinates + this.gridCoordinates;
      }
      set => this.gridCoordinates = value;
    }

    internal float GridCoordinatesX
    {
      get
      {
        return this.owner.Parent == null ? this.gridCoordinates.X : this.owner.Parent.Transform.GridCoordinates.X + this.gridCoordinates.X;
      }
      set => this.gridCoordinates.X = value;
    }

    internal float GridCoordinatesY
    {
      get
      {
        return this.owner.Parent == null ? this.gridCoordinates.Y : this.owner.Parent.Transform.GridCoordinates.Y + this.gridCoordinates.Y;
      }
      set => this.gridCoordinates.Y = value;
    }

    public abstract Vector2 Velocity { get; set; }

    public abstract float VelocityX { get; set; }

    public abstract float VelocityY { get; set; }

    public float Rotation { get; set; }

    public Vector2 Position
    {
      get
      {
        return this.owner.Parent == null ? this.PositionWithoutParent : this.owner.Parent.Transform.Position + this.PositionWithoutParent;
      }
      set
      {
        this.PositionWithoutParent = value;
        this.Reposition(value);
      }
    }

    public float X
    {
      get
      {
        return this.owner.Parent == null ? this.PositionWithoutParent.X : this.owner.Parent.Transform.Position.X + this.PositionWithoutParent.X;
      }
      internal set
      {
        this.PositionWithoutParent.X = value;
        this.InCellLocation.X = MathUtil.CalculateInCellLocation(this.PositionWithoutParent).X;
        this.gridCoordinates.X = (float) (int) ((double) this.PositionWithoutParent.X / (double) Config.GRID);
      }
    }

    public float Y
    {
      get
      {
        return this.owner.Parent == null ? this.PositionWithoutParent.Y : this.owner.Parent.Transform.Position.Y + this.PositionWithoutParent.Y;
      }
      internal set
      {
        this.PositionWithoutParent.Y = value;
        this.InCellLocation.Y = MathUtil.CalculateInCellLocation(this.PositionWithoutParent).Y;
        this.gridCoordinates.Y = (float) (int) ((double) this.PositionWithoutParent.Y / (double) Config.GRID);
      }
    }

    public AbstractTransform(IGameObject owner, Vector2 position = default (Vector2))
    {
      this.owner = owner;
      this.Position = position;
    }

    public void OverridePositionOffset(Vector2 newPositionOffset)
    {
      this.PositionWithoutParent = newPositionOffset;
    }

    public void DetachFromParent()
    {
      this.PositionWithoutParent = this.owner.Transform.Position;
      this.Reposition(this.PositionWithoutParent);
    }

    private void Reposition(Vector2 position)
    {
      this.GridCoordinates = MathUtil.CalculateGridCoordintes(position);
      this.InCellLocation = MathUtil.CalculateInCellLocation(position);
    }
  }
}
