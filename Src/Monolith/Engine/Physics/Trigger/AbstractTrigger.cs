
// Type: MonolithEngine.AbstractTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public abstract class AbstractTrigger : ITrigger, IComponent
  {
    protected Entity owner;
    private string tag = "";
    protected Vector2 PositionOffset;

    public Vector2 Position => this.owner.Transform.Position + this.PositionOffset;

    public bool UniquePerEntity { get; set; }

    public AbstractTrigger(Entity owner, Vector2 positionOffset = default (Vector2), string tag = "")
    {
      this.owner = owner;
      this.PositionOffset = positionOffset;
      this.tag = tag;
      this.UniquePerEntity = false;
    }

    public abstract bool IsInsideTrigger(IGameObject otherObject);

    public string GetTag() => this.tag;

    public void SetTag(string tag) => this.tag = tag;

    Type IComponent.GetComponentType() => typeof (ITrigger);
  }
}
