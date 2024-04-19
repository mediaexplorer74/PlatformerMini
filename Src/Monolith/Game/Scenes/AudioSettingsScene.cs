
// Type: ForestPlatformerExample.AudioSettingsScene

// Draft - not ready yet :)

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class AudioSettingsScene : AbstractScene
  {
        /*
        // Multi-Selects (multi-items)
    private MultiSelectionImage resolutionSelect = new MultiSelectionImage(new Vector2(300f, 100f), 
        scale: 0.25f);
    private MultiSelectionImage frameLimitSelect = new MultiSelectionImage(new Vector2(300f, 200f), 
        scale: 0.25f);
    private MultiSelectionImage vsyncSelect = new MultiSelectionImage(new Vector2(300f, 300f), 
        scale: 0.25f);
    private MultiSelectionImage windowModeSelect = new MultiSelectionImage(new Vector2(300f, 400f), 
        scale: 0.25f);
      */

    public AudioSettingsScene()
      : base("AudioSettings", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      /*
      Image ResolutionLabel = new Image(Assets.GetTexture2D("HUDResolutionLabel"), 
          new Vector2(150f, 50f), scale: 0.25f);

      this.resolutionSelect.AddOption("720p", Assets.GetTexture2D("HUD720p"));
      this.resolutionSelect.AddOption("1080p", Assets.GetTexture2D("HUD1080p"));
      this.resolutionSelect.AddOption("1440p", Assets.GetTexture2D("HUD1440p"));
      this.resolutionSelect.AddOption("4K", Assets.GetTexture2D("HUD4K"));

      SelectableImage ArrowRightBase = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), 
          Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 100f), scale: 0.02f);
      ArrowRightBase.OnClick = (Action) (() => this.resolutionSelect.Next());
      SelectableImage ArrowLeftBase = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"),
          Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 100f), scale: 0.02f);
      ArrowLeftBase.OnClick = (Action) (() => this.resolutionSelect.Previous());
      ArrowRightBase.HoverSoundEffectName = "MenuHover";
      ArrowRightBase.SelectSoundEffectName = "MenuSelect";
      ArrowLeftBase.HoverSoundEffectName = "MenuHover";
      ArrowLeftBase.SelectSoundEffectName = "MenuSelect";

      Image FPSLimitLabel = new Image(Assets.GetTexture2D("HUDFPSLimitLabel"), 
          new Vector2(150f, 150f), scale: 0.25f);

      this.frameLimitSelect.AddOption("30", Assets.GetTexture2D("HUD30"));
      this.frameLimitSelect.AddOption("60", Assets.GetTexture2D("HUD60"));
      this.frameLimitSelect.AddOption("120", Assets.GetTexture2D("HUD120"));
      this.frameLimitSelect.AddOption("Unlimited", Assets.GetTexture2D("HUDUnlimited"));

      SelectableImage ArrowRightBaseSelect = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), 
          Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 200f), scale: 0.02f);
      ArrowRightBaseSelect.OnClick = (Action) (() => this.frameLimitSelect.Next());

      SelectableImage ArrowLeftSelected = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"),
          Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 200f), scale: 0.02f);

        ArrowLeftSelected.OnClick = (Action) (() => this.frameLimitSelect.Previous());
        ArrowLeftSelected.HoverSoundEffectName = "MenuHover";
        ArrowLeftSelected.SelectSoundEffectName = "MenuSelect";
        ArrowLeftSelected.HoverSoundEffectName = "MenuHover";
        ArrowLeftSelected.SelectSoundEffectName = "MenuSelect";

      Image VsyncLabel = new Image(Assets.GetTexture2D("HUDVsyncLabel"), new Vector2(150f, 250f), 
          scale: 0.25f);
      this.vsyncSelect.AddOption("On", Assets.GetTexture2D("HUDOn"));
      this.vsyncSelect.AddOption("Off", Assets.GetTexture2D("HUDOff"));

      SelectableImage ArrowRightSelected = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), 
          Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 300f), scale: 0.02f);
      ArrowRightSelected.OnClick = (Action) (() => this.vsyncSelect.Next());

      SelectableImage ArrowLeftSelectedElement = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), 
          Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 300f), scale: 0.02f);
      ArrowLeftSelectedElement.OnClick = (Action) (() => this.vsyncSelect.Previous());
      ArrowRightSelected.HoverSoundEffectName = "MenuHover";
      ArrowRightSelected.SelectSoundEffectName = "MenuSelect";

      ArrowLeftSelectedElement.HoverSoundEffectName = "MenuHover";
      ArrowLeftSelectedElement.SelectSoundEffectName = "MenuSelect";

      Image WindowModeLabel = new Image(Assets.GetTexture2D("HUDWindowModeLabel"), new Vector2(150f, 350f), 
          scale: 0.25f);

      this.windowModeSelect.AddOption("Fullscreen", Assets.GetTexture2D("HUDFullscreen"));
      this.windowModeSelect.AddOption("Windowed", Assets.GetTexture2D("HUDWindowed"));

      SelectableImage ArrowRightSelectedElement = new SelectableImage(
          Assets.GetTexture2D("HUDArrowRightBase"), 
          Assets.GetTexture2D("HUDArrowRightSelected"), 
          new Vector2(485f, 400f), scale: 0.02f);

      ArrowRightSelectedElement.OnClick = (Action) (() => this.windowModeSelect.Next());

      SelectableImage ArrowLSelected = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), 
          Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 400f), scale: 0.02f);

      ArrowLSelected.OnClick = (Action) (() => this.windowModeSelect.Previous());
      ArrowRightSelectedElement.HoverSoundEffectName = "MenuHover";
      ArrowRightSelectedElement.SelectSoundEffectName = "MenuSelect";
      ArrowLSelected.HoverSoundEffectName = "MenuHover";
      ArrowLSelected.SelectSoundEffectName = "MenuSelect";
      */

      SelectableImage CancelSelected = new SelectableImage(Assets.GetTexture2D("HUDCancelBase"), 
          Assets.GetTexture2D("HUDCancelSelected"), new Vector2(150f, 500f), scale: 0.25f);
      CancelSelected.HoverSoundEffectName = "MenuHover";
      CancelSelected.SelectSoundEffectName = "MenuSelect";
      CancelSelected.OnClick = (Action) (() => this.SceneManager.StartScene("Settings"));
       
      /*
      SelectableImage ApplySelected = new SelectableImage(Assets.GetTexture2D("HUDApplyBase"), 
          Assets.GetTexture2D("HUDApplySelected"), new Vector2(450f, 500f), scale: 0.25f);
      ApplySelected.HoverSoundEffectName = "MenuHover";
      ApplySelected.SelectSoundEffectName = "MenuSelect";
      ApplySelected.OnClick = new Action(this.ApplyConfiguration);

      this.UI.AddUIElement((IUIElement) ResolutionLabel);
      this.UI.AddUIElement((IUIElement) FPSLimitLabel);
      this.UI.AddUIElement((IUIElement) VsyncLabel);
      this.UI.AddUIElement((IUIElement) WindowModeLabel);

      this.UI.AddUIElement((IUIElement) this.resolutionSelect);
      this.UI.AddUIElement((IUIElement) this.frameLimitSelect);
      this.UI.AddUIElement((IUIElement) this.vsyncSelect);
      this.UI.AddUIElement((IUIElement) this.windowModeSelect);

      this.UI.AddUIElement((IUIElement) ArrowRightBase);
      this.UI.AddUIElement((IUIElement) ArrowLeftBase);
      this.UI.AddUIElement((IUIElement) ArrowLeftSelected);
      this.UI.AddUIElement((IUIElement) ArrowRightBaseSelect);
      this.UI.AddUIElement((IUIElement) ArrowLeftSelectedElement);
      this.UI.AddUIElement((IUIElement) ArrowRightSelected);
      this.UI.AddUIElement((IUIElement) ArrowRightSelectedElement);
      this.UI.AddUIElement((IUIElement) ArrowLSelected);
      this.UI.AddUIElement((IUIElement) ApplySelected);
      */
      this.UI.AddUIElement((IUIElement) CancelSelected);
      /*
      this.SetCurrentVideoSettings();
      */
    }

    private void SetCurrentVideoSettings()
    {
            /*
      switch (VideoConfiguration.RESOLUTION_HEIGHT)
      {
                
        case 720:
          this.resolutionSelect.SetSelected("720p");
          break;
        case 1080:
          this.resolutionSelect.SetSelected("1080p");
          break;
        case 1440:
          this.resolutionSelect.SetSelected("1440p");
          break;
        case 2160:
          this.resolutionSelect.SetSelected("4K");
          break;
          
      }

      switch (VideoConfiguration.FRAME_LIMIT)
      {
        
        case 0:
          this.frameLimitSelect.SetSelected("Unlimited");
          break;
        case 30:
          this.frameLimitSelect.SetSelected("30");
          break;
        case 60:
          this.frameLimitSelect.SetSelected("60");
          break;
        case 120:
          this.frameLimitSelect.SetSelected("120");
          break;
          
      }

      if (VideoConfiguration.VSYNC)
        this.vsyncSelect.SetSelected("On");
      else
        this.vsyncSelect.SetSelected("Off");

      if (VideoConfiguration.FULLSCREEN)
        this.windowModeSelect.SetSelected("Fullscreen");
      else
        this.windowModeSelect.SetSelected("Windowed");
      /*
    }

    public void ApplyConfiguration()
    {
            /*
      if (this.resolutionSelect.GetSelection().Equals("720p"))
      {
        VideoConfiguration.RESOLUTION_WIDTH = 1280;
        VideoConfiguration.RESOLUTION_HEIGHT = 720;
      }
      else if (this.resolutionSelect.GetSelection().Equals("1080p"))
      {
        VideoConfiguration.RESOLUTION_WIDTH = 1920;
        VideoConfiguration.RESOLUTION_HEIGHT = 1080;
      }
      else if (this.resolutionSelect.GetSelection().Equals("1440p"))
      {
        VideoConfiguration.RESOLUTION_WIDTH = 2560;
        VideoConfiguration.RESOLUTION_HEIGHT = 1440;
      }
      else if (this.resolutionSelect.GetSelection().Equals("4K"))
      {
        VideoConfiguration.RESOLUTION_WIDTH = 3840;
        VideoConfiguration.RESOLUTION_HEIGHT = 2160;
      }
      VideoConfiguration.VSYNC = this.vsyncSelect.GetSelection().Equals("On");
      VideoConfiguration.FULLSCREEN = this.windowModeSelect.GetSelection().Equals("Fullscreen");
      VideoConfiguration.FRAME_LIMIT = !this.frameLimitSelect.GetSelection().Equals("Unlimited") 
                ? int.Parse(this.frameLimitSelect.GetSelection()) : 0;
      VideoConfiguration.Apply();

      foreach (Camera camera in this.Cameras)
        camera.Initialize();
      this.SceneManager.OnResolutionChanged();
      */
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
      PlatformerGame.Paused = true;
      //this.SetCurrentVideoSettings();
    }

    public override void OnFinished()
    {
    }
  }
}
