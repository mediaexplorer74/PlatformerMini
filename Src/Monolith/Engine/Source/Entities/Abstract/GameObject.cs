// Decompiled with JetBrains decompiler
// Type: MonolithEngine.GameObject
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System.Collections.Generic;


namespace MonolithEngine
{
  public abstract class GameObject : IGameObject
  {
    private static int GLOBAL_ID;
    protected HashSet<string> Tags = new HashSet<string>();
    private IGameObject parent;

    private int ID { get; set; }

    public AbstractTransform Transform { get; set; }

    public virtual IGameObject Parent
    {
      get => this.parent;
      set
      {
        if (value != null)
        {
          if (this.parent != null)
            this.parent.RemoveChild((IGameObject) this);
          this.parent = value;
          value.AddChild((IGameObject) this);
        }
        else
        {
          if (this.parent != null)
          {
            this.Transform.DetachFromParent();
            this.parent.RemoveChild((IGameObject) this);
          }
          this.parent = (IGameObject) null;
        }
      }
    }

    public GameObject(IGameObject parent = null)
    {
      if (parent != null)
        this.Parent = parent;
      this.ID = GameObject.GLOBAL_ID++;
    }

    public abstract void Destroy();

    public override bool Equals(object obj)
    {
      return obj is GameObject && this.ID == ((GameObject) obj).ID;
    }

    public override int GetHashCode() => this.ID;

    public int GetID() => this.ID;

    public static int GetObjectCount() => GameObject.GLOBAL_ID;

    public ICollection<string> GetTags() => (ICollection<string>) this.Tags;

    public virtual void AddTag(string tag) => this.Tags.Add(tag);

    public bool HasTag(string tag) => this.Tags.Contains(tag);

    public bool HasAnyTag(ICollection<string> tags)
    {
      foreach (string tag in (IEnumerable<string>) tags)
      {
        if (tags.Contains(tag))
          return true;
      }
      return false;
    }

    public virtual void RemoveTag(string tag) => this.Tags.Remove(tag);

    public abstract bool IsAlive();

    public abstract void AddChild(IGameObject gameObject);

    public abstract void RemoveChild(IGameObject gameObject);
  }
}
