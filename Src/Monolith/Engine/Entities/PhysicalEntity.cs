
// Type: MonolithEngine.PhysicalEntity
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonolithEngine
{
  public class PhysicalEntity : Entity
  {
    private Vector2 bump;
    private bool keepBouncing;
    public bool IsOnGround;
    public float HorizontalFriction = Config.HORIZONTAL_FRICTION;
    public float VerticalFriction = Config.VERTICAL_FRICTION;
    protected float BumpFriction = Config.BUMP_FRICTION;
    protected float MovementSpeed = Config.CHARACTER_SPEED;
    public float GravityValue = Config.GRAVITY_FORCE;
    public bool HasGravity = Config.GRAVITY_ON;
    private Texture2D colliderMarker;
    internal PhysicalEntity MountedOn;
    private PhysicalEntity leftCollider;
    private PhysicalEntity rightCollider;
    private Vector2 previousPosition = Vector2.Zero;
    private float previousRotation;
    private float stepX;
    private float stepY;
    private DynamicTransform Transform;

    protected float FallSpeed { get; set; }

    protected bool CollidesOnGrid { get; private set; }

    public PhysicalEntity(Layer layer, Entity parent = null, Vector2 startPosition = default (Vector2))
      : base(layer, parent, startPosition)
    {
      this.Transform = new DynamicTransform((IGameObject) this, startPosition);
      base.Transform = (AbstractTransform) this.Transform;
      this.previousPosition = startPosition;
      this.CheckGridCollisions = true;
      this.Active = true;
      this.ResetPosition(startPosition);
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);

    public void Bump(Vector2 direction, bool keepBouncing = false)
    {
      this.bump = direction;
      this.FallSpeed = 0.0f;
      this.stepY = 0.0f;
      this.keepBouncing = keepBouncing;
    }

    public override void Update()
    {
      if (this.previousPosition == this.Transform.Position || Config.FIXED_UPDATE_FPS == VideoConfiguration.FRAME_LIMIT || Config.FIXED_UPDATE_FPS == 0)
        this.DrawPosition = this.Transform.Position;
      else
        this.DrawPosition = Vector2.Lerp(this.previousPosition, this.Transform.Position, Globals.FixedUpdateAlpha);
      if ((double) this.previousRotation != (double) this.Transform.Rotation)
        this.DrawRotation = MathUtil.LerpRotationDegrees(this.previousRotation, this.Transform.Rotation, Globals.FixedUpdateAlpha);
      base.Update();
    }

    public override void PreFixedUpdate() => base.PreFixedUpdate();

    public override void FixedUpdate()
    {
      this.CollidesOnGrid = false;
      if (this.Transform.Velocity != Vector2.Zero)
        this.Transform.Velocity.Normalize();
      this.previousPosition = this.Parent != null ? (this.Parent as PhysicalEntity).previousPosition + this.Transform.PositionWithoutParent : this.Transform.Position;
      this.previousRotation = this.Transform.Rotation;
      if (this.leftCollider != null)
      {
        if ((double) this.leftCollider.Transform.VelocityX != 0.0)
        {
          if ((double) this.Transform.VelocityX >= 0.0)
            this.Transform.VelocityX = this.leftCollider.Transform.VelocityX * (float) (1.0 / Math.Pow((double) this.HorizontalFriction, (double) Globals.FixedUpdateMultiplier));
        }
        else if ((double) this.Transform.VelocityX < 0.0)
          this.Transform.VelocityX = 0.0f;
      }
      if (this.rightCollider != null)
      {
        if ((double) this.rightCollider.Transform.VelocityX != 0.0)
        {
          if ((double) this.Transform.VelocityX <= 0.0)
            this.Transform.VelocityX = this.rightCollider.Transform.VelocityX * (float) (1.0 / Math.Pow((double) this.HorizontalFriction, (double) Globals.FixedUpdateMultiplier));
        }
        else if ((double) this.Transform.VelocityX > 0.0)
          this.Transform.VelocityX = 0.0f;
      }
      if (this.HasGravity && !this.OnGround())
      {
        if ((double) this.FallSpeed == 0.0)
          this.FallSpeed = (float) Globals.GameTime.TotalGameTime.TotalSeconds;
        this.ApplyGravity();
      }
      if (this.OnGround())
        this.FallSpeed = 0.0f;
      float num = (float) Math.Ceiling((double) Math.Abs((this.Transform.VelocityX + this.bump.X) * Globals.FixedUpdateMultiplier) + (double) Math.Abs((this.Transform.VelocityY + this.bump.Y) * Globals.FixedUpdateMultiplier)) / Config.DYNAMIC_COLLISION_CHECK_FREQUENCY;
      this.IsCollisionCheckedInCurrentLoop = (double) num > 0.0;
      if ((double) num > 0.0)
      {
        this.stepX = (this.Transform.VelocityX + this.bump.X) * Globals.FixedUpdateMultiplier / num;
        this.stepY = (this.Transform.VelocityY + this.bump.Y) * Globals.FixedUpdateMultiplier / num;
        while ((double) num > 0.0)
        {
          this.Transform.InCellLocation.X += this.stepX;
          if (this.CheckGridCollisions && (double) this.Transform.InCellLocation.X > (double) this.CollisionOffsetLeft && this.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) this, Direction.EAST))
          {
            this.Transform.InCellLocation.X = this.CollisionOffsetLeft;
            this.CollidesOnGrid = true;
          }
          if (this.CheckGridCollisions && (double) this.Transform.InCellLocation.X < (double) this.CollisionOffsetRight && this.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) this, Direction.WEST))
          {
            this.Transform.InCellLocation.X = this.CollisionOffsetRight;
            this.CollidesOnGrid = true;
          }
          for (; (double) this.Transform.InCellLocation.X > 1.0; ++this.Transform.gridCoordinates.X)
            --this.Transform.InCellLocation.X;
          for (; (double) this.Transform.InCellLocation.X < 0.0; --this.Transform.gridCoordinates.X)
            ++this.Transform.InCellLocation.X;
          this.Transform.InCellLocation.Y += this.stepY;
          if (this.MountedOn == null && this.CheckGridCollisions && (double) this.Transform.InCellLocation.Y > (double) this.CollisionOffsetBottom && this.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) this, Direction.SOUTH))
          {
            if ((double) this.Transform.VelocityY > 0.0)
              this.OnLand(this.Transform.Velocity);
            this.Transform.VelocityY = 0.0f;
            if (!this.keepBouncing)
              this.bump.Y = 0.0f;
            this.Transform.InCellLocation.Y = this.CollisionOffsetBottom;
            this.CollidesOnGrid = true;
          }
          if (this.CheckGridCollisions && (double) this.Transform.InCellLocation.Y < (double) this.CollisionOffsetTop && this.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) this, Direction.NORTH))
          {
            this.Transform.VelocityY = 0.0f;
            this.bump.Y = 0.0f;
            this.Transform.InCellLocation.Y = this.CollisionOffsetTop;
            this.CollidesOnGrid = true;
          }
          for (; (double) this.Transform.InCellLocation.Y > 1.0; ++this.Transform.gridCoordinates.Y)
            --this.Transform.InCellLocation.Y;
          for (; (double) this.Transform.InCellLocation.Y < 0.0; --this.Transform.gridCoordinates.Y)
            ++this.Transform.InCellLocation.Y;
          this.SetPosition();
          this.Scene.CollisionEngine.CheckCollisions((IColliderEntity) this);
          --num;
          if ((double) this.stepX == 0.0 && (double) this.stepY == 0.0)
            num = 0.0f;
        }
      }
      if ((double) this.HorizontalFriction > 0.0)
        this.Transform.VelocityX *= (float) Math.Pow((double) this.HorizontalFriction, (double) Globals.FixedUpdateMultiplier);
      if ((double) this.BumpFriction > 0.0)
        this.bump.X *= (float) Math.Pow((double) this.BumpFriction, (double) Globals.FixedUpdateMultiplier * 0.1);
      if ((double) Math.Abs(this.Transform.VelocityX) <= 0.0005 * (double) Globals.FixedUpdateMultiplier)
        this.Transform.VelocityX = 0.0f;
      if ((double) Math.Abs(this.bump.X) <= 0.0005 * (double) Globals.FixedUpdateMultiplier)
        this.bump.X = 0.0f;
      if ((double) this.VerticalFriction > 0.0)
        this.Transform.VelocityY *= (float) Math.Pow((double) this.VerticalFriction, (double) Globals.FixedUpdateMultiplier);
      if ((double) this.BumpFriction > 0.0)
        this.bump.Y *= (float) Math.Pow((double) this.BumpFriction, (double) Globals.FixedUpdateMultiplier * 0.1);
      if ((double) Math.Abs(this.Transform.VelocityY) <= 0.0005 * (double) Globals.FixedUpdateMultiplier)
        this.Transform.VelocityY = 0.0f;
      if ((double) Math.Abs(this.bump.Y) <= 0.1 * (double) Globals.FixedUpdateMultiplier)
        this.bump.Y = 0.0f;
      if (this.Parent == null)
        this.SetPosition();
      base.FixedUpdate();
    }

    private void SetPosition()
    {
      this.Transform.PositionWithoutParent = (this.Transform.gridCoordinates + this.Transform.InCellLocation) * (float) Config.GRID;
    }

    private void ApplyGravity()
    {
      if (Config.INCREASING_GRAVITY)
        this.Transform.VelocityY += this.GravityValue * (((float) Globals.GameTime.TotalGameTime.TotalSeconds - this.FallSpeed) * Config.GRAVITY_T_MULTIPLIER) * Globals.FixedUpdateMultiplier;
      else
        this.Transform.VelocityY += this.GravityValue * Globals.FixedUpdateMultiplier * Config.TIME_OFFSET;
    }

    public override void PostUpdate() => base.PostUpdate();

    private bool OnGround()
    {
      bool flag = this.MountedOn != null || this.Scene.GridCollisionChecker.HasBlockingColliderAt((IGameObject) this, Direction.SOUTH) && (double) this.Transform.InCellLocation.Y == (double) this.CollisionOffsetBottom && (double) this.Transform.VelocityY >= 0.0;
      if (!flag && this.IsOnGround)
        this.OnLeaveGround();
      this.IsOnGround = flag;
      return this.IsOnGround;
    }

    protected virtual void OnLeaveGround()
    {
    }

    protected virtual void OnLand(Vector2 velocity)
    {
    }

    internal override sealed void HandleCollisionStart(IGameObject otherCollider, bool allowOverlap)
    {
      this.PositionEntity(otherCollider, allowOverlap);
      base.HandleCollisionStart(otherCollider, allowOverlap);
    }

    private void PositionEntity(IGameObject otherCollider, bool allowOverlap)
    {
      if (allowOverlap || this.Parent != null || !(otherCollider is Entity) || (otherCollider as Entity).GetCollisionComponent() == null)
        return;
      ICollisionComponent collisionComponent1 = this.GetCollisionComponent();
      ICollisionComponent collisionComponent2 = (otherCollider as Entity).GetCollisionComponent();
      switch (collisionComponent1)
      {
        case BoxCollisionComponent _ when collisionComponent2 is BoxCollisionComponent:
          BoxCollisionComponent collisionComponent3 = collisionComponent1 as BoxCollisionComponent;
          BoxCollisionComponent collisionComponent4 = collisionComponent2 as BoxCollisionComponent;
          float num1 = Math.Max(0.0f, Math.Min(collisionComponent3.Position.X + collisionComponent3.Width, collisionComponent4.Position.X + collisionComponent4.Width) - Math.Max(collisionComponent3.Position.X, collisionComponent4.Position.X));
          float num2 = Math.Max(0.0f, Math.Min(collisionComponent3.Position.Y + collisionComponent3.Height, collisionComponent4.Position.Y + collisionComponent4.Height) - Math.Max(collisionComponent3.Position.Y, collisionComponent4.Position.Y));
          if ((double) num2 != 0.0 && (double) num2 < (double) num1 && (double) collisionComponent3.Position.Y < (double) collisionComponent4.Position.Y)
          {
            if ((double) num2 <= 0.0 || this.OnGround() || (double) this.Transform.VelocityY <= 0.0)
              break;
            this.stepY = 0.0f;
            this.OnLand(this.Transform.Velocity);
            this.Transform.VelocityY = 0.0f;
            this.MountedOn = otherCollider as PhysicalEntity;
            float x = this.Transform.Position.X - this.MountedOn.Transform.Position.X;
            this.Parent = (IGameObject) this.MountedOn;
            this.Transform.Position = new Vector2(x, 0.0f);
            this.bump = Vector2.Zero;
            this.FallSpeed = 0.0f;
            break;
          }
          if ((double) num1 <= 0.0 || (double) num1 >= (double) num2)
            break;
          if ((double) this.Transform.VelocityX > 0.0)
          {
            this.stepX = 0.0f;
            this.Transform.VelocityX = 0.0f;
            this.rightCollider = otherCollider as PhysicalEntity;
            this.Transform.X -= num1;
          }
          if ((double) this.Transform.VelocityX >= 0.0)
            break;
          this.stepX = 0.0f;
          this.Transform.VelocityX = 0.0f;
          this.leftCollider = otherCollider as PhysicalEntity;
          this.Transform.X += num1;
          break;
        case CircleCollisionComponent _:
          CircleCollisionComponent collisionComponent5 = collisionComponent2 as CircleCollisionComponent;
          throw new Exception("Non-overlapping collision type is not implemented between the current colliders");
        default:
          throw new Exception("Non-overlapping collision type is not implemented between the current colliders");
      }
    }

    internal override sealed void HandleCollisionEnd(IGameObject otherCollider)
    {
      if (this.MountedOn != null && otherCollider.Equals((object) this.MountedOn))
      {
        DynamicTransform transform = this.Transform;
        transform.Velocity = transform.Velocity + this.MountedOn.Transform.Velocity;
        this.MountedOn = (PhysicalEntity) null;
        this.Parent = (IGameObject) null;
      }
      if (this.leftCollider != null && otherCollider.Equals((object) this.leftCollider))
        this.leftCollider = (PhysicalEntity) null;
      if (this.rightCollider != null && otherCollider.Equals((object) this.rightCollider))
        this.rightCollider = (PhysicalEntity) null;
      base.HandleCollisionEnd(otherCollider);
    }

    public void ResetPosition(Vector2 position)
    {
      this.Transform.InCellLocation = MathUtil.CalculateInCellLocation(position);
      this.Transform.GridCoordinates = new Vector2((float) (int) ((double) position.X / (double) Config.GRID), (float) (int) ((double) position.Y / (double) Config.GRID));
      this.Transform.Position = position;
      this.FallSpeed = 0.0f;
    }

    public override void Destroy()
    {
      this.CheckGridCollisions = false;
      this.Transform.Velocity = Vector2.Zero;
      this.bump = Vector2.Zero;
      base.Destroy();
    }

    public bool IsMovingAtLeast(float speed)
    {
      return (double) Math.Abs(this.Transform.VelocityX) >= (double) speed || (double) Math.Abs(this.Transform.VelocityY) >= (double) speed;
    }

    public Vector2 GetVelocity() => this.Transform.Velocity;

    public void AddForce(Vector2 force)
    {
      DynamicTransform transform = this.Transform;
      transform.Velocity = transform.Velocity + force;
    }

    public void SetVelocity(Vector2 velocity) => this.Transform.Velocity = velocity;

    public void AddVelocity(Vector2 velocity)
    {
      DynamicTransform transform = this.Transform;
      transform.Velocity = transform.Velocity + velocity;
    }

    public bool IsAtRest() => this.Transform.Velocity == Vector2.Zero && this.bump == Vector2.Zero;

    public void CancelVelocities()
    {
      this.Transform.Velocity = Vector2.Zero;
      this.bump = Vector2.Zero;
      this.stepX = 0.0f;
      this.stepY = 0.0f;
    }
  }
}
