// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.MainMenuScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class MainMenuScene : AbstractScene
  {
    public MainMenuScene()
      : base("MainMenu")
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      //RnD
      float num1 = 0.25f;// MonolithGame.Platform.IsMobile() ? /*1f*/0.5f : 0.25f;

      Logger.Debug("Loading main menu UI elements...");
      Texture2D texture2D1 = Assets.GetTexture2D("HUDNewGameBase");
      Texture2D texture2D2 = Assets.GetTexture2D("HUDNewGameSelected");
      Vector2 position1 = new Vector2(150f, 150f);
      float num2 = num1;
      Rectangle sourceRectangle1 = new Rectangle();
      double scale1 = (double) num2;
      Color color1 = new Color();
      SelectableImage newElement1 = new SelectableImage(texture2D1, texture2D2, position1, sourceRectangle1, (float) scale1, color: color1);
      newElement1.HoverSoundEffectName = "MenuHover";
      newElement1.SelectSoundEffectName = "MenuSelect";
      newElement1.OnClick = (Action) (() => this.SceneManager.LoadScene("LevelSelect"));
      SelectableImage newElement2 = (SelectableImage) null;

      //RnD
      if (1==1)//(!MonolithGame.Platform.IsMobile())
      {
        Texture2D texture2D3 = Assets.GetTexture2D("HUDSettingsBase");
        Texture2D texture2D4 = Assets.GetTexture2D("HUDSettingsSelected");
        Vector2 position2 = new Vector2(150f, 200f);
        float num3 = num1;
        Rectangle sourceRectangle2 = new Rectangle();
        double scale2 = (double) num3;
        Color color2 = new Color();
        newElement2 = new SelectableImage(texture2D3, texture2D4, position2, sourceRectangle2,
            (float) scale2, color: color2);
        newElement2.HoverSoundEffectName = "MenuHover";
        newElement2.SelectSoundEffectName = "MenuSelect";
        newElement2.OnClick = (Action) (() => this.SceneManager.StartScene("Settings"));
      }

      Texture2D texture2D5 = Assets.GetTexture2D("HUDQuitBase");
      Texture2D texture2D6 = Assets.GetTexture2D("HUDQuitSelected");
      Vector2 position3 = new Vector2(150f, 250f);
      float num4 = num1;
      Rectangle sourceRectangle3 = new Rectangle();
      double scale3 = (double) num4;
      Color color3 = new Color();
      this.UI.AddUIElement((IUIElement) new SelectableImage(texture2D5, texture2D6, position3, sourceRectangle3, (float) scale3, color: color3)
      {
        HoverSoundEffectName = "MenuHover",
        SelectSoundEffectName = "MenuSelect",
        OnClick = Config.ExitAction
      });

            //RnD
      if (1==1)//(!MonolithGame.Platform.IsMobile())
        this.UI.AddUIElement((IUIElement) newElement2);

      this.UI.AddUIElement((IUIElement) newElement1);
    }

    public override void OnEnd()
    {
    }

    public override void OnStart() => PlatformerGame.Paused = false;

    public override void OnFinished()
    {
    }
  }
}
