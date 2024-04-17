// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Level1Scene
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
  internal class Level1Scene : AbstractScene
  {
    private Hero hero;
    private SpriteFont font;
    private LDTKMap world;

    public Level1Scene(LDTKMap world, SpriteFont spriteFont)
      : base("Level_1", useLoadingScreen: true)
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
      Logger.Debug("Loading LEVEL 1: assets");
      this.LoadData();
      Logger.Debug("Loading LEVEL 1: adjusting camera");
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
      Logger.Debug("Loading LEVEL 1: UI");
      this.UI.AddUIElement((IUIElement) new Image(Assets.GetTexture2D("HUDCointCount"), new Vector2(5f, 5f), scale: 2f));
      this.UI.AddUIElement((IUIElement) new TextField(this.font, (Func<string>) (() => PlatformerGame.CoinCount.ToString()), new Vector2(50f, 5f), scale: 2.5f));
      Logger.Debug("Loading LEVEL 1: creating entity parser...");
      EntityParser entityParser = new EntityParser(this.world);
      Logger.Debug("Loading LEVEL 1: loading entities...");
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
            AudioEngine.Pause("Level1Music");
        }

        public override void OnStart()
        {
          foreach (Camera camera in this.Cameras)
            camera.TrackTarget((Entity) this.hero, true);
          PlatformerGame.Paused = false;
          PlatformerGame.WasGameStarted = true;
          AudioEngine.Play("Level1Music");
          PlatformerGame.CurrentScene = this.SceneName;
        }

        public override void OnFinished()
        {
            this.SceneManager.LoadScene("Level_2");
        }
    }
}
