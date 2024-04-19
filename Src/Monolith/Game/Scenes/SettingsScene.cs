
// Type: ForestPlatformerExample.SettingsScene


using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class SettingsScene : AbstractScene
  {
    public SettingsScene()
      : base("Settings", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      // Video Settings select / element
      SelectableImage VideoSettingsSelect = new SelectableImage(Assets.GetTexture2D("HUDVideoSettingsBase"), 
          Assets.GetTexture2D("HUDVideoSettingsSelected"), new Vector2(150f, 150f), scale: 0.25f);
      VideoSettingsSelect.HoverSoundEffectName = "MenuHover";
      VideoSettingsSelect.SelectSoundEffectName = "MenuSelect";
      VideoSettingsSelect.OnClick = (Action) (() => this.SceneManager.StartScene("VideoSettings"));

      // Audio Settings select / element
      SelectableImage AudioSettingsSelect = new SelectableImage(Assets.GetTexture2D("HUDAudioSettingsBase"), 
          Assets.GetTexture2D("HUDAudioSettingsSelected"), new Vector2(150f, 250f), scale: 0.25f)
      {
        HoverSoundEffectName = "MenuHover",
        SelectSoundEffectName = "MenuSelect",
        OnClick = (Action) (() => this.SceneManager.StartScene("AudioSettings"))
      };

      // Back select / element
      SelectableImage BackSelect = new SelectableImage(Assets.GetTexture2D("HUDBackBase"), 
          Assets.GetTexture2D("HUDBackSelected"), new Vector2(150f, 350f), scale: 0.25f);
      BackSelect.HoverSoundEffectName = "MenuHover";
      BackSelect.SelectSoundEffectName = "MenuSelect";
      BackSelect.OnClick = (Action) (() =>
      {
        if (PlatformerGame.WasGameStarted)
          this.SceneManager.StartScene("PauseMenu");
        else
          this.SceneManager.StartScene("MainMenu");
      });
      this.UI.AddUIElement((IUIElement) VideoSettingsSelect);
      this.UI.AddUIElement((IUIElement)AudioSettingsSelect);
      this.UI.AddUIElement((IUIElement) BackSelect);
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
