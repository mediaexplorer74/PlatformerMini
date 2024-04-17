// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.GameEndScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  internal class GameEndScene : AbstractScene
  {
    public GameEndScene()
      : base("EndScene", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      Image newElement1 = new Image(Assets.GetTexture2D("FinishedText"), new Vector2(150f, 150f), scale: 0.25f);
      SelectableImage newElement2 = new SelectableImage(Assets.GetTexture2D("HUDQuitBase"), Assets.GetTexture2D("HUDQuitSelected"), new Vector2(150f, 250f), scale: 0.25f);
      newElement2.HoverSoundEffectName = "MenuHover";
      newElement2.SelectSoundEffectName = "MenuSelect";
      newElement2.OnClick = Config.ExitAction;
      this.UI.AddUIElement((IUIElement) newElement1);
      this.UI.AddUIElement((IUIElement) newElement2);
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
      PlatformerGame.Paused = true;
      PlatformerGame.WasGameStarted = false;
    }

    public override void OnFinished()
    {
    }
  }
}
