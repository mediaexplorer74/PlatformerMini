// Type: MonolithEngine.StaticPopup
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MonolithEngine
{
  public class StaticPopup : Entity
  {
    private SpriteFont font;
    private string text;
    private Color textColor;
    private float timeout;
    private Keys continueButton;

    public StaticPopup(AbstractScene scene, Vector2 position, float timeout = 0.0f, Keys continueButton = Keys.Space)
      : base(scene.LayerManager.UILayer, startPosition: position)
    {
      this.AddComponent<UserInputController>(new UserInputController());
      this.timeout = timeout;
      this.continueButton = continueButton;
      this.Visible = false;
    }

    public void SetSprite(Texture2D texture, float scale)
    {
      this.AddComponent<Sprite>(new Sprite((Entity) this, new MonolithTexture(texture))
      {
        Scale = scale
      });
    }

    public void SetText(SpriteFont font, string text, Color textColor = default (Color))
    {
      this.text = text;
      this.font = font;
      if (textColor == new Color())
        this.textColor = Color.White;
      else
        this.textColor = textColor;
    }

    public override void FixedUpdate() => base.FixedUpdate();

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (this.text == null)
        return;
      spriteBatch.DrawString(this.font, this.text, this.Transform.Position, this.textColor);
    }

    public void Display()
    {
      this.Active = true;
      this.Visible = true;
      if ((double) this.timeout == 0.0)
      {
        this.Scene.LayerManager.Paused = true;
        this.GetComponent<UserInputController>().RegisterKeyPressAction(this.continueButton, (Action) (() =>
        {
          this.Scene.LayerManager.Paused = false;
          this.Destroy();
        }));
      }
      else
        Timer.TriggerAfter(this.timeout, (Action) (() => this.Destroy()));
    }
  }
}
