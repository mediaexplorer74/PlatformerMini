// Decompiled with JetBrains decompiler
// Type: MonolithEngine.BoxTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace MonolithEngine
{
  public class BoxTrigger : AbstractTrigger
  {
    private int x1;
    private int y1;
    private int x2;
    private int y2;

    public BoxTrigger(Entity owner, int width, int height, Vector2 positionOffset = default (Vector2), string tag = "")
      : base(owner, positionOffset, tag)
    {
      this.x1 = 0;
      this.y1 = 0;
      this.x2 = width;
      this.y2 = height;
    }

    public override bool IsInsideTrigger(IGameObject otherObject)
    {
      return (double) otherObject.Transform.X >= (double) this.Position.X + (double) this.x1 && (double) otherObject.Transform.X <= (double) this.Position.X + (double) this.x2 && (double) otherObject.Transform.Y >= (double) this.Position.Y + (double) this.y1 && (double) otherObject.Transform.Y <= (double) this.Position.Y + (double) this.y2;
    }
  }
}
