// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.SettingsScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;

#nullable disable
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
      SelectableImage newElement1 = new SelectableImage(Assets.GetTexture2D("HUDVideoSettingsBase"), Assets.GetTexture2D("HUDVideoSettingsSelected"), new Vector2(150f, 150f), scale: 0.25f);
      newElement1.HoverSoundEffectName = "MenuHover";
      newElement1.SelectSoundEffectName = "MenuSelect";
      newElement1.OnClick = (Action) (() => this.SceneManager.StartScene("VideoSettings"));
      SelectableImage selectableImage = new SelectableImage(Assets.GetTexture2D("HUDAudioSettingsBase"), Assets.GetTexture2D("HUDAudioSettingsSelected"), new Vector2(150f, 200f), scale: 0.25f)
      {
        HoverSoundEffectName = "MenuHover",
        SelectSoundEffectName = "MenuSelect",
        OnClick = (Action) (() => this.SceneManager.StartScene("AudioSettings"))
      };
      SelectableImage newElement2 = new SelectableImage(Assets.GetTexture2D("HUDBackBase"), Assets.GetTexture2D("HUDBackSelected"), new Vector2(150f, 250f), scale: 0.25f);
      newElement2.HoverSoundEffectName = "MenuHover";
      newElement2.SelectSoundEffectName = "MenuSelect";
      newElement2.OnClick = (Action) (() =>
      {
        if (PlatformerGame.WasGameStarted)
          this.SceneManager.StartScene("PauseMenu");
        else
          this.SceneManager.StartScene("MainMenu");
      });
      this.UI.AddUIElement((IUIElement) newElement1);
      this.UI.AddUIElement((IUIElement) newElement2);
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
