
// Type: ForestPlatformerExample.PauseMenuScene


using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class PauseMenuScene : AbstractScene
  {
    public PauseMenuScene()
      : base("PauseMenu", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      SelectableImage ContinueElement = new SelectableImage(Assets.GetTexture2D("HUDContinueBase"), 
          Assets.GetTexture2D("HUDContinueSelected"), new Vector2(150f, 150f), scale: 0.25f);
      ContinueElement.HoverSoundEffectName = "MenuHover";
      ContinueElement.SelectSoundEffectName = "MenuSelect";
      ContinueElement.OnClick = (Action) (() => this.SceneManager.StartScene(PlatformerGame.CurrentScene));

      SelectableImage SettingsElement = new SelectableImage(Assets.GetTexture2D("HUDSettingsBase"),
          Assets.GetTexture2D("HUDSettingsSelected"), new Vector2(150f, 200f), scale: 0.25f);
      SettingsElement.HoverSoundEffectName = "MenuHover";
      SettingsElement.SelectSoundEffectName = "MenuSelect";
      SettingsElement.OnClick = (Action) (() => this.SceneManager.StartScene("Settings"));

        this.UI.AddUIElement((IUIElement)ContinueElement);
            
        this.UI.AddUIElement((IUIElement)SettingsElement);

        this.UI.AddUIElement((IUIElement) new SelectableImage(Assets.GetTexture2D("HUDQuitBase"),
          Assets.GetTexture2D("HUDQuitSelected"), new Vector2(150f, 250f), scale: 0.25f)
          {
            HoverSoundEffectName = "MenuHover",
            SelectSoundEffectName = "MenuSelect",
            OnClick = Config.ExitAction
          });

    
    }

    public override void OnEnd()
    {
    }

    public override void OnStart() => PlatformerGame.Paused = true;

    public override void OnFinished()
    {
    }
  }
}
