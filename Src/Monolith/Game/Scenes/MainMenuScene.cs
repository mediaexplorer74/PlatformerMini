
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
      float scale = 0.25f;// MonolithGame.Platform.IsMobile() ? /*1f*/0.5f : 0.25f;

      Logger.Debug("Loading main menu UI elements...");

       // New Game item
      Texture2D NewGameBasetexture = Assets.GetTexture2D("HUDNewGameBase");
      Texture2D NewGameSelectedtexture = Assets.GetTexture2D("HUDNewGameSelected");
      Vector2 NewGameposition = new Vector2(150f, 150f);
      float newgame_scale = scale;
      Rectangle NewGameRectangle = new Rectangle();
      Color new_game_color = new Color();

      SelectableImage NewGameSelect = new SelectableImage(
          NewGameBasetexture, NewGameSelectedtexture, 
          NewGameposition, NewGameRectangle, 
          newgame_scale, color: new_game_color);

      NewGameSelect.HoverSoundEffectName = "MenuHover";
      NewGameSelect.SelectSoundEffectName = "MenuSelect";
      NewGameSelect.OnClick = (Action) (() => this.SceneManager.LoadScene("LevelSelect"));


      // Settings select / element
      SelectableImage SettingsSelect = (SelectableImage) null;

      //RnD
      if (1==1)//(!MonolithGame.Platform.IsMobile())
      {
        Texture2D SettingsBasetexture = Assets.GetTexture2D("HUDSettingsBase");
        Texture2D SettingsSelectedtexture = Assets.GetTexture2D("HUDSettingsSelected");
        Vector2 Settingsposition = new Vector2(150f, 200f);
        float settings_scale = scale;
        Rectangle sourceRectangle2 = new Rectangle();
        Color settings_color = new Color();

        SettingsSelect = new SelectableImage
        (
            SettingsBasetexture, SettingsSelectedtexture, 
            Settingsposition, sourceRectangle2,
           settings_scale, color: settings_color
        );

        SettingsSelect.HoverSoundEffectName = "MenuHover";
        SettingsSelect.SelectSoundEffectName = "MenuSelect";
        SettingsSelect.OnClick = (Action) (() => this.SceneManager.StartScene("Settings"));
      }

      // Quit item
      Texture2D QuitBasetexture = Assets.GetTexture2D("HUDQuitBase");
      Texture2D QuitSelectedtexture = Assets.GetTexture2D("HUDQuitSelected");
      Vector2 quit_position = new Vector2(150f, 250f);
      float quit_scale = scale;
      Rectangle sourceRectangle3 = new Rectangle();
      Color quit_color = new Color();

      this.UI.AddUIElement((IUIElement) new SelectableImage
      (
          QuitBasetexture, QuitSelectedtexture, 
          quit_position, sourceRectangle3, 
          quit_scale, color: quit_color
      )
      {
        HoverSoundEffectName = "MenuHover",
        SelectSoundEffectName = "MenuSelect",
        OnClick = Config.ExitAction
      });

            //RnD
      if (1==1)//(!MonolithGame.Platform.IsMobile())
        this.UI.AddUIElement((IUIElement) SettingsSelect);

      this.UI.AddUIElement((IUIElement) NewGameSelect);
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        PlatformerGame.Paused = false;
    }

    public override void OnFinished()
    {
    }
  }
}
