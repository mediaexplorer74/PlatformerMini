
// Type: MonolithEngine.DynamicPopup
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonolithEngine
{
  internal class DynamicPopup : Entity
  {
    public DynamicPopup(
      AbstractScene scene,
      Texture2D texture,
      Vector2 position,
      Entity follow,
      float scale = 1f,
      float timeout = 0.0f)
      : base(scene.LayerManager.EntityLayer, follow, position)
    {
      this.AddComponent<UserInputController>(new UserInputController());
      Timer.TriggerAfter(timeout, (Action) (() => this.Destroy()));
      this.AddComponent<Sprite>(new Sprite((Entity) this, new MonolithTexture(texture))
      {
        Scale = scale
      });
      this.Active = true;
      this.Visible = true;
    }
  }
}
