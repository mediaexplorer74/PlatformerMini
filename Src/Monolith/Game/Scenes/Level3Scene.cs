
// Type: ForestPlatformerExample.Level3Scene

// My Experimentations with LDtk - Level 3 added (draaaft drawings only for ui/code testings)))


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class Level3Scene : AbstractScene
  {
    private Hero hero;
    private SpriteFont font;
    private LDTKMap world;

    public Level3Scene(LDTKMap world, SpriteFont spriteFont)
      : base("Level_3", useLoadingScreen: true)
    {
      this.font = spriteFont;
      this.world = world;
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      Logger.Debug("Loading LEVEL 3: assets");
      this.LoadData();
      Logger.Debug("Loading LEVEL 3: adjusting camera");
      foreach (Camera camera in this.Cameras)
      {
        camera.Initialize();

        if (MonolithGame.Platform.IsMobile())
        {
          camera.Zoom += 0.5f;
          camera.Zoom *= 2f;
        }
        else
          camera.Zoom += 0.5f;
      }
    }

    private void LoadData()
    {
      Logger.Debug("Loading LEVEL 3: UI");

      this.UI.AddUIElement((IUIElement) new Image(Assets.GetTexture2D("HUDCointCount"), 
          new Vector2(5f, 5f), scale: 2f));

      this.UI.AddUIElement((IUIElement) new TextField(this.font, 
          (Func<string>) (() => PlatformerGame.CoinCount.ToString()), new Vector2(50f, 5f), scale: 2.5f));

      Logger.Debug("Loading LEVEL 3: creating entity parser...");

      EntityParser entityParser = new EntityParser(this.world);

      Logger.Debug("Loading LEVEL 3: loading entities...");
      entityParser.LoadEntities((AbstractScene) this, this.SceneName);
      entityParser.LoadIntGrid((AbstractScene) this);
      
      this.hero = entityParser.GetHero();

      //Mobile
      if (MonolithGame.Platform.IsMobile())
      {
        foreach (IUIElement button in new MobileButtonPanel(this.hero).GetButtons())
          this.UI.AddUIElement(button);
      }
      this.world = (LDTKMap) null;
    }

    public override void OnEnd()
    {
        AudioEngine.Pause("Level3Music");
    }

    public override void OnStart()
    {
        foreach (Camera camera in this.Cameras)
        camera.TrackTarget((Entity) this.hero, true);
        PlatformerGame.Paused = false;
        PlatformerGame.WasGameStarted = true;
        AudioEngine.Play("Level3Music");
        PlatformerGame.CurrentScene = this.SceneName;
    }

    public override void OnFinished()
        {
        //this.SceneManager.LoadScene("Level_1"); // go to level 1 ))
        this.SceneManager.LoadScene("EndScene"); // GAME OVER :)
        }
  }
}
