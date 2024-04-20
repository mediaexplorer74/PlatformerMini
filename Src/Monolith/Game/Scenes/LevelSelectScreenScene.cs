
// Type: ForestPlatformerExample.LevelSelectScreen

// Level Select Screen. Add your designed (new) levels to Level select menu :)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class LevelSelectScreenScene : AbstractScene
  {
    public LevelSelectScreenScene()
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
      float item_scale = 0.25f; //MonolithGame.Platform.IsMobile() ? 0.5f : 0.25f;

      Logger.Debug("Loading level select scene UI...");

      // Level 1 select/element
      Texture2D Level1Basetexture = Assets.GetTexture2D("Level1Base");
      Texture2D Level1Selectedtexture = Assets.GetTexture2D("Level1Selected");
      Vector2 position1 = new Vector2(150f, 150f);
      float item_scale1 = item_scale;
      Rectangle sourceRectangle1 = new Rectangle();
      double scale1 = (double) item_scale1;
      Color color1 = new Color();

      SelectableImage Level1Select = new SelectableImage(Level1Basetexture, Level1Selectedtexture, 
          position1, sourceRectangle1, (float) scale1, color: color1);
      Level1Select.HoverSoundEffectName = "MenuHover";
      Level1Select.SelectSoundEffectName = "MenuSelect";
      Level1Select.OnClick = (Action) (() => this.SceneManager.LoadScene("Level_1"));

      // Level 2 select/element
      Texture2D Level2Basetexture = Assets.GetTexture2D("Level2Base");
      Texture2D Level2Selectedtexture = Assets.GetTexture2D("Level2Selected");
      Vector2 position2 = new Vector2(150f, 250f);
      float item_scale2 = item_scale;
      Rectangle sourceRectangle2 = new Rectangle();
      double scale2 = (double) item_scale2;
      Color color2 = new Color();

      SelectableImage Level2Select = new SelectableImage(Level2Basetexture, Level2Selectedtexture, 
          position2, sourceRectangle2, (float) scale2, color: color2);
      Level2Select.HoverSoundEffectName = "MenuHover";
      Level2Select.SelectSoundEffectName = "MenuSelect";
      Level2Select.OnClick = (Action) (() => this.SceneManager.LoadScene("Level_2"));

      // Experimental +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      // Level 2 select/element
      Texture2D Level3Basetexture = Assets.GetTexture2D("Level3Base");
      Texture2D Level3Selectedtexture = Assets.GetTexture2D("Level3Selected");
      Vector2 position3 = new Vector2(150f, 300f);
      float item_scale3 = item_scale;
      Rectangle sourceRectangle3 = new Rectangle();
      double scale3 = (double)item_scale2;
      Color color3 = new Color();

      SelectableImage Level3Select = new SelectableImage(Level3Basetexture, Level3Selectedtexture,
        position3, sourceRectangle3, (float)scale3, color: color3);
      Level3Select.HoverSoundEffectName = "MenuHover";
      Level3Select.SelectSoundEffectName = "MenuSelect";
      Level3Select.OnClick = (Action)(() => this.SceneManager.LoadScene("Level_3"));
      // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

      // Back select/element
      Texture2D HUDBackBasetexture = Assets.GetTexture2D("HUDBackBase");
      Texture2D HUDBackSelectedtexture = Assets.GetTexture2D("HUDBackSelected");
      Vector2 position_100 = new Vector2(150f, 350f);
      float num_100 = item_scale;
      Rectangle sourceRectangle_100 = new Rectangle();
      double scale_100 = (double) num_100;
      Color color_100 = new Color();

      SelectableImage BackSelect = 
            new SelectableImage(HUDBackBasetexture, HUDBackSelectedtexture, 
            position_100, sourceRectangle_100,
            (float) scale_100, color: color_100);

      BackSelect.HoverSoundEffectName = "MenuHover";
      BackSelect.SelectSoundEffectName = "MenuSelect";
      BackSelect.OnClick = (Action) (() => this.SceneManager.LoadScene("MainMenu"));

      this.UI.AddUIElement((IUIElement) Level1Select);
      this.UI.AddUIElement((IUIElement) Level2Select);
      this.UI.AddUIElement((IUIElement) Level3Select);
      this.UI.AddUIElement((IUIElement) BackSelect);
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        PlatformerGame.Paused = true;
    }

    public override void OnFinished()
    {
    }
  }
}
