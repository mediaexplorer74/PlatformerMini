// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.Level2Scene
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
  internal class Level2Scene : AbstractScene
  {
    private Hero hero;
    private SpriteFont font;
    private LDTKMap world;

    public Level2Scene(LDTKMap world, SpriteFont spriteFont)
      : base("Level_2", useLoadingScreen: true)
    {
      this.font = spriteFont;
      this.world = world;
      this.BackgroundColor = Color.LightBlue;
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      this.LoadData();
      foreach (Camera camera in this.Cameras)
      {
        camera.Initialize();

        //RnD
        if (1==0)//(MonolithGame.Platform.IsMobile())
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
      this.UI.AddUIElement((IUIElement) new Image(Assets.GetTexture2D("HUDCointCount"), 
          new Vector2(5f, 5f), scale: 2f));
      this.UI.AddUIElement((IUIElement) new TextField(this.font, 
          (Func<string>) (() => PlatformerGame.CoinCount.ToString()), new Vector2(50f, 5f), scale: 2.5f));

      EntityParser entityParser = new EntityParser(this.world);
      entityParser.LoadEntities((AbstractScene) this, this.SceneName);
      entityParser.LoadIntGrid((AbstractScene) this);
      this.hero = entityParser.GetHero();

      if (!MonolithGame.Platform.IsMobile())
        return;
      
      // Mobile
      foreach (IUIElement button in new MobileButtonPanel(this.hero).GetButtons())
        this.UI.AddUIElement(button);
    }

    public override void OnEnd() => AudioEngine.Pause("Level2Music");

    public override void OnStart()
    {
      foreach (Camera camera in this.Cameras)
        camera.TrackTarget((Entity) this.hero, true);
      PlatformerGame.Paused = false;
      PlatformerGame.WasGameStarted = true;
      AudioEngine.Play("Level2Music");
      PlatformerGame.CurrentScene = this.SceneName;
    }

    public override void OnFinished() => this.SceneManager.LoadScene("EndScene");
  }
}
