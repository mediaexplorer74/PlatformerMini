
// Type: ForestPlatformerExample.GameEndScene

using Microsoft.Xna.Framework;
using MonolithEngine;
using System.Collections.Generic;


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
      Image FinishedText = new Image(Assets.GetTexture2D("FinishedText"),
          new Vector2(150f, 150f), scale: 0.25f);

      SelectableImage QuitSelect = new SelectableImage(Assets.GetTexture2D("HUDQuitBase"), 
          Assets.GetTexture2D("HUDQuitSelected"), new Vector2(150f, 250f), scale: 0.25f);
      QuitSelect.HoverSoundEffectName = "MenuHover";
      QuitSelect.SelectSoundEffectName = "MenuSelect";
      QuitSelect.OnClick = Config.ExitAction;

      this.UI.AddUIElement((IUIElement) FinishedText);

      this.UI.AddUIElement((IUIElement) QuitSelect);
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
