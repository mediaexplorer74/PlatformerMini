// Type: ForestPlatformerExample.PopupTrigger
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class PopupTrigger : Entity
  {
    private string textName;
    private StaticPopup popup;

    public PopupTrigger(
      AbstractScene scene,
      Vector2 position,
      int width,
      int height,
      string textName)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.Visible = false;
      this.Active = true;
      this.AddTag("Environment");
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, width, height, Vector2.Zero));
      this.AddTriggeredAgainst(typeof (Hero));
      switch (textName)
      {
        case "Controls":
          this.popup = new StaticPopup(this.Scene, this.Transform.Position + new Vector2(0.0f, /*-120f*/-50f), 6000f, Keys.Enter);
                    string Controlstext = " Popup testing: Hi there! Good jump...\n ohh... luck! )))";// " Controls:\n LEFT/RIGHT Arrows: Walk\n UP Arrow: Jump\n DOWN Arrow: Descend from platform\n SPACE: Punch/Throw box\n Left/Right SHIFT: Pick up/Put down box\n Left/Right CONTROL: Slide\n Mouse Wheel Up/Down: Zoom In/Out\n\n Kill enemies by jumping on their heads,\n punching them or throwing boxes at them.\n\n You won't take any damage while sliding.\n\n [PRESS ENTER TO CONTINUE]";
          this.popup.SetText(Assets.GetFont("InGameText"), Controlstext, Color.White);
          this.popup.SetSprite(new MonolithTexture(Assets.CreateRectangle(230, /*230*/80, Color.Black)));
          break;

        //RnD ************************************************************************
         case "Box":
            this.popup = new StaticPopup(this.Scene, this.Transform.Position + new Vector2(0.0f, -50f), 6000f, Keys.Down);
            string Boxtext = " Box Reminder: you can pick up boxes with SHIFT,\n then press SPACE to throw them at enemies.\n You can jump while holding a box!\n\n [PRESS DOWN TO CONTINUE]";
            this.popup.SetText(Assets.GetFont("InGameText"), Boxtext, Color.White);
            this.popup.SetSprite(new MonolithTexture(Assets.CreateRectangle(270, 80, Color.Black)));
            break;
         case "Cheat":
            this.popup = new StaticPopup(this.Scene, this.Transform.Position + new Vector2(0.0f, -50f), 6000f, Keys.Down);
            string Cheattext = " Cheat Reminder: you can fast leave this level.\n Go to left bottom corner to jump on next level!\n\n [PRESS DOWN TO CONTINUE]";
            this.popup.SetText(Assets.GetFont("InGameText"), Cheattext, Color.White);
            this.popup.SetSprite(new MonolithTexture(Assets.CreateRectangle(270, 80, Color.Black)));
            break;
                // ***************************************************************************

                case "BoxThrow":
          this.popup = new StaticPopup(this.Scene, this.Transform.Position + new Vector2(0.0f, -50f), 6000f, Keys.Enter);
          string BoxThrowtext = " Reminder: you can pick up boxes with SHIFT,\n then press SPACE to throw them at enemies.\n You can jump while holding a box!\n\n [PRESS ENTER TO CONTINUE]";
          this.popup.SetText(Assets.GetFont("InGameText"), BoxThrowtext, Color.White);
          this.popup.SetSprite(new MonolithTexture(Assets.CreateRectangle(270, 90, Color.Black)));
          break;
        case "SpikeReminder":
          this.popup = new StaticPopup(this.Scene, this.Transform.Position + new Vector2(0.0f, -10f), 6000f, Keys.Enter);
          string SpikeRemindertext = " Reminder: Press CONTROL to slide through spikes\n without taking damage.";
          this.popup.SetText(Assets.GetFont("InGameText"), SpikeRemindertext, Color.White);
          this.popup.SetSprite(new MonolithTexture(Assets.CreateRectangle(270, 30, Color.Black)));
          break;
      }
      this.textName = textName;
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
      {
        AudioEngine.StopSoundEffects();
        this.popup.Display();
        this.Destroy();
      }
      base.OnEnterTrigger(triggerTag, otherEntity);
    }
  }
}
