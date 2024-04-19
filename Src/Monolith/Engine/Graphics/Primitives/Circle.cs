
// Type: MonolithEngine.Circle
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonolithEngine
{
  public class Circle : Entity
  {
    private Vector2 center;
    private Color color;
    private float radius;
    private Vector2 offset;

    public Circle(AbstractScene scene, Entity parent, Vector2 center, int radius, Color color)
      : base(scene.LayerManager.EntityLayer, parent, center)
    {
      this.SetSprite(new MonolithTexture(AssetUtil.CreateCircle(radius, color)));
      this.color = color;
      this.center = center;
      this.radius = (float) radius;
      this.offset = new Vector2((float) radius, (float) radius) / 2f;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      this.GetComponent<Sprite>().DrawOffset -= this.offset;
      base.Draw(spriteBatch);
    }
  }
}
