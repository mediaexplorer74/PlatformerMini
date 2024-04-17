// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Entity
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MonolithEngine
{
  public class Entity : GameObject, IColliderEntity, IHasTrigger, IGameObject, IRayBlocker
  {
    protected List<IGameObject> Children = new List<IGameObject>();
    private List<IGameObject> childrenToAdd = new List<IGameObject>();
    private List<IGameObject> childrenToRemove = new List<IGameObject>();
    public AbstractScene Scene;
    internal bool IsCollisionCheckedInCurrentLoop;
    public Vector2 DrawPosition;
    protected float CollisionOffsetLeft;
    protected float CollisionOffsetRight;
    protected float CollisionOffsetBottom;
    protected float CollisionOffsetTop;
    private ComponentList componentList;
    private Dictionary<Type, bool> collidesAgainst = new Dictionary<Type, bool>();
    private HashSet<Type> triggeredAgainst = new HashSet<Type>();
    private bool checkGridCollisions;
    private bool canFireTriggers;
    private bool visible = true;
    private bool active = true;
    private float drawPriority;
    public Vector2 Pivot = Vector2.Zero;
    protected List<(Vector2 start, Vector2 end)> RayBlockerLines;
    private Texture2D pivotMarker;
    public float Depth;
    protected bool Destroyed;
    protected bool BeingDestroyed;
    public float DrawRotation;
    private UserInputController UserInput;

    public bool IsDestroyed => this.Destroyed || this.BeingDestroyed;

    public bool CheckGridCollisions
    {
      get => this.checkGridCollisions;
      set
      {
        if (value == this.checkGridCollisions)
          return;
        this.checkGridCollisions = value;
        this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
      }
    }

    public bool CanFireTriggers
    {
      get => this.canFireTriggers;
      set
      {
        this.canFireTriggers = value;
        this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
      }
    }

    public Direction CurrentFaceDirection { get; set; }

    public bool Visible
    {
      get => this.visible;
      set
      {
        if (value != this.visible)
          this.Layer.OnObjectChanged(this);
        this.visible = value;
      }
    }

    public bool Active
    {
      get => this.active;
      set
      {
        if (value != this.active)
          this.Layer.OnObjectChanged(this);
        this.active = value;
      }
    }

    public float DrawPriority
    {
      get => this.drawPriority;
      set
      {
        if ((double) value == (double) this.drawPriority)
          return;
        this.drawPriority = value;
        this.Layer.SortByPriority();
      }
    }

    protected Layer Layer { get; set; }

    public bool BlocksRay { get; set; }

    public bool CollisionsEnabled { get; set; } = true;

    public override sealed IGameObject Parent
    {
      get => base.Parent;
      set
      {
        base.Parent = value;
        this.Layer.OnObjectChanged(this);
      }
    }

    public Entity(Layer layer, Entity parent = null, Vector2 startPosition = default (Vector2))
      : base()
    {
      this.componentList = new ComponentList((IGameObject) this);
      this.DrawPosition = startPosition;
      this.Transform = (AbstractTransform) new StaticTransform((IGameObject) this, startPosition);
      this.Layer = layer;
      this.Scene = layer.Scene;
      layer.OnObjectChanged(this);
      this.Parent = (IGameObject) parent;
    }

    protected virtual void SetRayBlockers()
    {
      this.RayBlockerLines.Clear();
      this.RayBlockerLines.Add((this.Transform.Position, new Vector2(this.Transform.X, this.Transform.Y + (float) Config.GRID)));
      this.RayBlockerLines.Add((this.Transform.Position, new Vector2(this.Transform.X + (float) Config.GRID, this.Transform.Y)));
      this.RayBlockerLines.Add((new Vector2(this.Transform.X + (float) Config.GRID, this.Transform.Y), new Vector2(this.Transform.X + (float) Config.GRID, this.Transform.Y + (float) Config.GRID)));
      this.RayBlockerLines.Add((new Vector2(this.Transform.X, this.Transform.Y + (float) Config.GRID), new Vector2(this.Transform.X + (float) Config.GRID, this.Transform.Y + (float) Config.GRID)));
    }

    public T GetComponent<T>() where T : IComponent => this.componentList.GetComponent<T>();

    public List<T> GetComponents<T>() where T : IComponent => this.componentList.GetComponents<T>();

    public void AddComponent<T>(T newComponent) where T : IComponent
    {
      this.componentList.AddComponent<T>(newComponent);
      if ((object) newComponent is ICollisionComponent || (object) newComponent is ITrigger)
        this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
      if (!((object) newComponent is UserInputController))
        return;
      this.UserInput = (object) newComponent as UserInputController;
    }

    public void RemoveComponent<T>(T component) where T : IComponent
    {
      if (!((object) component is ICollisionComponent) && !((object) component is ITrigger))
        return;
      this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
    }

    public void RemoveComponent<T>() where T : IComponent
    {
      this.componentList.RemoveComponent<T>();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      this.componentList.DrawAll(spriteBatch);
      if (this.Children.Count > 0)
      {
        foreach (Entity child in this.Children)
        {
          if (child.Visible)
            child.Draw(spriteBatch);
        }
      }
      this.HandleChangedChildrenChildren();
    }

    public virtual void PreUpdate()
    {
      this.componentList.PreUpdateAll();
      foreach (Entity child in this.Children)
      {
        if (child.Active)
          child.PreUpdate();
      }
    }

    public virtual void PreFixedUpdate()
    {
      foreach (Entity child in this.Children)
      {
        if (child.Active)
          child.PreFixedUpdate();
      }
      this.HandleChangedChildrenChildren();
    }

    public virtual void FixedUpdate()
    {
      if (!this.IsCollisionCheckedInCurrentLoop)
        this.Scene.CollisionEngine.CheckCollisions((IColliderEntity) this);
      this.componentList.UpdateAll();
      foreach (Entity child in this.Children)
      {
        if (child.Active)
          child.FixedUpdate();
      }
      this.HandleChangedChildrenChildren();
    }

    public virtual void Update()
    {
      foreach (Entity child in this.Children)
      {
        if (child.Active)
          child.Update();
      }
      this.HandleChangedChildrenChildren();
    }

    public virtual void PostUpdate()
    {
      this.componentList.PostUpdateAll();
      foreach (Entity child in this.Children)
      {
        if (child.Active)
          child.PostUpdate();
      }
      this.HandleChangedChildrenChildren();
    }

    public override void Destroy()
    {
      if (this.BeingDestroyed || this.Destroyed)
        return;
      this.BeingDestroyed = true;
      this.RemoveCollisions();
      this.ClearAllComponents();
      this.Cleanup();
      this.Destroyed = true;
      this.BeingDestroyed = false;
    }

    protected void Cleanup()
    {
      this.componentList.ClearAll();
      if (this.Parent != null)
        this.Parent.RemoveChild((IGameObject) this);
      if (this.Children.Any<IGameObject>())
      {
        foreach (Entity entity in this.Children.ToList<IGameObject>())
          entity?.Destroy();
      }
      this.Active = false;
      this.Visible = false;
    }

    internal void ClearAllComponents() => this.componentList.ClearAll();

    protected virtual void RemoveCollisions()
    {
      this.componentList.Clear<ICollisionComponent>();
      this.componentList.Clear<ITrigger>();
      this.collidesAgainst.Clear();
      this.CanFireTriggers = false;
      this.CollisionsEnabled = false;
      this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
      this.BlocksRay = false;
    }

    public void SetSprite(MonolithTexture texture)
    {
      if (texture == null)
        return;
      this.AddComponent<Sprite>(new Sprite(this, texture));
    }

    public virtual List<(Vector2 start, Vector2 end)> GetRayBlockerLines()
    {
      if (this.RayBlockerLines == null)
        this.RayBlockerLines = new List<(Vector2, Vector2)>();
      this.SetRayBlockers();
      return this.RayBlockerLines;
    }

    public Vector2 GetGridCoord() => this.Transform.GridCoordinates;

    public float GetCollisionOffset(Direction direction)
    {
      switch (direction)
      {
        case Direction.NORTH:
          return this.CollisionOffsetTop;
        case Direction.SOUTH:
          return this.CollisionOffsetBottom;
        case Direction.WEST:
          return this.CollisionOffsetRight;
        case Direction.EAST:
          return this.CollisionOffsetLeft;
        default:
          throw new Exception("Unsupported direction");
      }
    }

    public ICollisionComponent GetCollisionComponent()
    {
      return this.componentList.GetComponent<ICollisionComponent>();
    }

    public virtual void OnCollisionStart(IGameObject otherCollider)
    {
    }

    public virtual void OnCollisionEnd(IGameObject otherCollider)
    {
    }

    internal virtual void HandleCollisionStart(IGameObject otherCollider, bool allowOverlap)
    {
      this.OnCollisionStart(otherCollider);
    }

    internal virtual void HandleCollisionEnd(IGameObject otherCollider)
    {
      this.OnCollisionEnd(otherCollider);
    }

    void IColliderEntity.CollisionStarted(IGameObject otherCollider, bool allowOverlap)
    {
      this.HandleCollisionStart(otherCollider, allowOverlap);
    }

    void IColliderEntity.CollisionEnded(IGameObject otherCollider)
    {
      this.HandleCollisionEnd(otherCollider);
    }

    Dictionary<Type, bool> IColliderEntity.GetCollidesAgainst() => this.collidesAgainst;

    HashSet<Type> IColliderEntity.GetTriggeredAgainst() => this.triggeredAgainst;

    public void AddCollisionAgainst(Type type, bool allowOverlap = true)
    {
      this.collidesAgainst[type] = allowOverlap;
      this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
    }

    public void AddTriggeredAgainst(Type type, bool allowOverlap = true)
    {
      this.triggeredAgainst.Add(type);
      this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
    }

    public ICollection<ITrigger> GetTriggers()
    {
      return (ICollection<ITrigger>) this.componentList.GetComponents<ITrigger>();
    }

    public void RemoveTrigger(AbstractTrigger trigger)
    {
      foreach (ITrigger component in this.componentList.GetComponents<ITrigger>())
      {
        if (component.Equals((object) trigger))
        {
          this.RemoveTrigger(component);
          break;
        }
      }
    }

    public void RemoveTrigger(ITrigger trigger)
    {
      this.componentList.RemoveComponent<ITrigger>(trigger);
      this.Scene.CollisionEngine.ObjectChanged((IGameObject) this);
    }

    public virtual void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
    }

    public virtual void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
    }

    public override void AddTag(string tag) => base.AddTag(tag);

    public override void RemoveTag(string tag) => base.RemoveTag(tag);

    public override bool IsAlive() => !this.IsDestroyed && !this.BeingDestroyed;

    public override void AddChild(IGameObject gameObject) => this.childrenToAdd.Add(gameObject);

    public override void RemoveChild(IGameObject gameObject)
    {
      this.childrenToRemove.Add(gameObject);
    }

    private void HandleChangedChildrenChildren()
    {
      if (this.childrenToAdd.Count > 0)
      {
        foreach (IGameObject gameObject in this.childrenToAdd)
          this.Children.Add(gameObject);
        this.childrenToAdd.Clear();
      }
      if (this.childrenToRemove.Count <= 0)
        return;
      foreach (IGameObject gameObject in this.childrenToRemove)
        this.Children.Remove(gameObject);
      this.childrenToRemove.Clear();
    }
  }
}
