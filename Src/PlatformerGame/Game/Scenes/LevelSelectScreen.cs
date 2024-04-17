// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.LevelSelectScreen
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  internal class LevelSelectScreen : AbstractScene
  {
    public LevelSelectScreen()
      : base("LevelSelect", true)
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
      float num1 = 0.25f; //MonolithGame.Platform.IsMobile() ? 0.5f : 0.25f;

      Logger.Debug("Loading level select scene UI...");
      Texture2D texture2D1 = Assets.GetTexture2D("Level1Base");
      Texture2D texture2D2 = Assets.GetTexture2D("Level1Selected");
      Vector2 position1 = new Vector2(150f, 150f);
      float num2 = num1;
      Rectangle sourceRectangle1 = new Rectangle();
      double scale1 = (double) num2;
      Color color1 = new Color();

      SelectableImage newElement1 = new SelectableImage(texture2D1, texture2D2, 
          position1, sourceRectangle1, (float) scale1, color: color1);

      newElement1.HoverSoundEffectName = "MenuHover";
      newElement1.SelectSoundEffectName = "MenuSelect";
      newElement1.OnClick = (Action) (() => this.SceneManager.LoadScene("Level_1"));
      Texture2D texture2D3 = Assets.GetTexture2D("Level2Base");
      Texture2D texture2D4 = Assets.GetTexture2D("Level2Selected");
      Vector2 position2 = new Vector2(150f, 250f);
      float num3 = num1;
      Rectangle sourceRectangle2 = new Rectangle();
      double scale2 = (double) num3;
      Color color2 = new Color();

      SelectableImage newElement2 = new SelectableImage(texture2D3, texture2D4, 
          position2, sourceRectangle2, (float) scale2, color: color2);

      newElement2.HoverSoundEffectName = "MenuHover";
      newElement2.SelectSoundEffectName = "MenuSelect";
      newElement2.OnClick = (Action) (() => this.SceneManager.LoadScene("Level_2"));
      Texture2D texture2D5 = Assets.GetTexture2D("HUDBackBase");
      Texture2D texture2D6 = Assets.GetTexture2D("HUDBackSelected");
      Vector2 position3 = new Vector2(150f, 350f);
      float num4 = num1;
      Rectangle sourceRectangle3 = new Rectangle();
      double scale3 = (double) num4;
      Color color3 = new Color();

      SelectableImage newElement3 = 
                new SelectableImage(texture2D5, texture2D6, position3, sourceRectangle3,
                (float) scale3, color: color3);

      newElement3.HoverSoundEffectName = "MenuHover";
      newElement3.SelectSoundEffectName = "MenuSelect";
      newElement3.OnClick = (Action) (() => this.SceneManager.LoadScene("MainMenu"));
      this.UI.AddUIElement((IUIElement) newElement1);
      this.UI.AddUIElement((IUIElement) newElement2);
      this.UI.AddUIElement((IUIElement) newElement3);
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
