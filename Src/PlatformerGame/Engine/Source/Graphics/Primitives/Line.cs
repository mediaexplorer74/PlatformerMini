// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Line
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace MonolithEngine
{
  public class Line : Entity
  {
    private Vector2 Origin;
    private Vector2 Scale;
    public Vector2 From;
    public Vector2 To;
    private Vector2 fromSaved;
    private Vector2 toSaved;
    private Color color;
    private float thickness;
    private float angleRad;
    private float length;

    public Line(
      AbstractScene scene,
      Entity parent,
      Vector2 from,
      Vector2 to,
      Color color,
      float thickness = 1f)
      : base(scene.LayerManager.EntityLayer, parent, from)
    {
      this.From = this.fromSaved = from;
      this.To = this.toSaved = to;
      this.thickness = thickness;
      this.color = color;
      this.SetSprite(new MonolithTexture(AssetUtil.CreateRectangle(1, Color.White)));
      this.length = Vector2.Distance(from, to);
      this.angleRad = MathUtil.RadFromVectors(from, to);
      this.Origin = new Vector2(0.0f, 0.0f);
      this.Scale = new Vector2(this.length, thickness);
    }

    public Line(
      AbstractScene scene,
      Entity parent,
      Vector2 from,
      float angleRad,
      float length,
      Color color,
      float thickness = 1f)
      : base(scene.LayerManager.EntityLayer, parent, from)
    {
      this.From = this.fromSaved = from;
      this.thickness = thickness;
      this.color = color;
      this.SetSprite(new MonolithTexture(AssetUtil.CreateRectangle(1, Color.White)));
      this.length = length;
      this.angleRad = angleRad;
      this.To = this.toSaved = MathUtil.EndPointOfLine(from, length, this.angleRad);
      this.Origin = new Vector2(0.0f, 0.0f);
      this.Scale = new Vector2(length, thickness);
    }

    public void SetEnd(Vector2 end)
    {
      this.To = end;
      this.length = Vector2.Distance(this.From, this.To);
      this.angleRad = MathUtil.RadFromVectors(this.From, this.To);
      this.Scale = new Vector2(this.length, this.thickness);
    }

    public void Reset()
    {
      this.From = this.fromSaved;
      this.To = this.toSaved;
      this.length = Vector2.Distance(this.From, this.To);
      this.angleRad = MathUtil.RadFromVectors(this.From, this.To);
      this.Scale = new Vector2(this.length, this.thickness);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.GetComponent<Sprite>().Texture.GetTexture2D(), this.Transform.Position, new Rectangle?(), this.color, this.angleRad, this.Origin, this.Scale, SpriteEffects.None, 0.0f);
    }

    protected override void SetRayBlockers() => this.RayBlockerLines.Add((this.From, this.To));
  }
}
