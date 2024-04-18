// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.VideoSettingsScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  internal class VideoSettingsScene : AbstractScene
  {
    private MultiSelectionImage resolutionSelect = new MultiSelectionImage(new Vector2(300f, 100f), scale: 0.25f);
    private MultiSelectionImage frameLimitSelect = new MultiSelectionImage(new Vector2(300f, 200f), scale: 0.25f);
    private MultiSelectionImage vsyncSelect = new MultiSelectionImage(new Vector2(300f, 300f), scale: 0.25f);
    private MultiSelectionImage windowModeSelect = new MultiSelectionImage(new Vector2(300f, 400f), scale: 0.25f);

    public VideoSettingsScene()
      : base("VideoSettings", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      Image newElement1 = new Image(Assets.GetTexture2D("HUDResolutionLabel"), new Vector2(150f, 50f), scale: 0.25f);
      this.resolutionSelect.AddOption("720p", Assets.GetTexture2D("HUD720p"));
      this.resolutionSelect.AddOption("1080p", Assets.GetTexture2D("HUD1080p"));
      this.resolutionSelect.AddOption("1440p", Assets.GetTexture2D("HUD1440p"));
      this.resolutionSelect.AddOption("4K", Assets.GetTexture2D("HUD4K"));
      SelectableImage newElement2 = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 100f), scale: 0.02f);
      newElement2.OnClick = (Action) (() => this.resolutionSelect.Next());
      SelectableImage newElement3 = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 100f), scale: 0.02f);
      newElement3.OnClick = (Action) (() => this.resolutionSelect.Previous());
      newElement2.HoverSoundEffectName = "MenuHover";
      newElement2.SelectSoundEffectName = "MenuSelect";
      newElement3.HoverSoundEffectName = "MenuHover";
      newElement3.SelectSoundEffectName = "MenuSelect";
      Image newElement4 = new Image(Assets.GetTexture2D("HUDFPSLimitLabel"), new Vector2(150f, 150f), scale: 0.25f);
      this.frameLimitSelect.AddOption("30", Assets.GetTexture2D("HUD30"));
      this.frameLimitSelect.AddOption("60", Assets.GetTexture2D("HUD60"));
      this.frameLimitSelect.AddOption("120", Assets.GetTexture2D("HUD120"));
      this.frameLimitSelect.AddOption("Unlimited", Assets.GetTexture2D("HUDUnlimited"));
      SelectableImage newElement5 = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 200f), scale: 0.02f);
      newElement5.OnClick = (Action) (() => this.frameLimitSelect.Next());
      SelectableImage newElement6 = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 200f), scale: 0.02f);
      newElement6.OnClick = (Action) (() => this.frameLimitSelect.Previous());
      newElement5.HoverSoundEffectName = "MenuHover";
      newElement5.SelectSoundEffectName = "MenuSelect";
      newElement6.HoverSoundEffectName = "MenuHover";
      newElement6.SelectSoundEffectName = "MenuSelect";
      Image newElement7 = new Image(Assets.GetTexture2D("HUDVsyncLabel"), new Vector2(150f, 250f), scale: 0.25f);
      this.vsyncSelect.AddOption("On", Assets.GetTexture2D("HUDOn"));
      this.vsyncSelect.AddOption("Off", Assets.GetTexture2D("HUDOff"));
      SelectableImage newElement8 = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 300f), scale: 0.02f);
      newElement8.OnClick = (Action) (() => this.vsyncSelect.Next());
      SelectableImage newElement9 = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 300f), scale: 0.02f);
      newElement9.OnClick = (Action) (() => this.vsyncSelect.Previous());
      newElement8.HoverSoundEffectName = "MenuHover";
      newElement8.SelectSoundEffectName = "MenuSelect";
      newElement9.HoverSoundEffectName = "MenuHover";
      newElement9.SelectSoundEffectName = "MenuSelect";
      Image newElement10 = new Image(Assets.GetTexture2D("HUDWindowModeLabel"), new Vector2(150f, 350f), scale: 0.25f);
      this.windowModeSelect.AddOption("Fullscreen", Assets.GetTexture2D("HUDFullscreen"));
      this.windowModeSelect.AddOption("Windowed", Assets.GetTexture2D("HUDWindowed"));
      SelectableImage newElement11 = new SelectableImage(Assets.GetTexture2D("HUDArrowRightBase"), Assets.GetTexture2D("HUDArrowRightSelected"), new Vector2(485f, 400f), scale: 0.02f);
      newElement11.OnClick = (Action) (() => this.windowModeSelect.Next());
      SelectableImage newElement12 = new SelectableImage(Assets.GetTexture2D("HUDArrowLeftBase"), Assets.GetTexture2D("HUDArrowLeftSelected"), new Vector2(265f, 400f), scale: 0.02f);
      newElement12.OnClick = (Action) (() => this.windowModeSelect.Previous());
      newElement11.HoverSoundEffectName = "MenuHover";
      newElement11.SelectSoundEffectName = "MenuSelect";
      newElement12.HoverSoundEffectName = "MenuHover";
      newElement12.SelectSoundEffectName = "MenuSelect";
      SelectableImage newElement13 = new SelectableImage(Assets.GetTexture2D("HUDCancelBase"), Assets.GetTexture2D("HUDCancelSelected"), new Vector2(150f, 500f), scale: 0.25f);
      newElement13.HoverSoundEffectName = "MenuHover";
      newElement13.SelectSoundEffectName = "MenuSelect";
      newElement13.OnClick = (Action) (() => this.SceneManager.StartScene("Settings"));
      SelectableImage newElement14 = new SelectableImage(Assets.GetTexture2D("HUDApplyBase"), Assets.GetTexture2D("HUDApplySelected"), new Vector2(450f, 500f), scale: 0.25f);
      newElement14.HoverSoundEffectName = "MenuHover";
      newElement14.SelectSoundEffectName = "MenuSelect";
      newElement14.OnClick = new Action(this.ApplyConfiguration);
      this.UI.AddUIElement((IUIElement) newElement1);
      this.UI.AddUIElement((IUIElement) newElement4);
      this.UI.AddUIElement((IUIElement) newElement7);
      this.UI.AddUIElement((IUIElement) newElement10);
      this.UI.AddUIElement((IUIElement) this.resolutionSelect);
      this.UI.AddUIElement((IUIElement) this.frameLimitSelect);
      this.UI.AddUIElement((IUIElement) this.vsyncSelect);
      this.UI.AddUIElement((IUIElement) this.windowModeSelect);
      this.UI.AddUIElement((IUIElement) newElement2);
      this.UI.AddUIElement((IUIElement) newElement3);
      this.UI.AddUIElement((IUIElement) newElement6);
      this.UI.AddUIElement((IUIElement) newElement5);
      this.UI.AddUIElement((IUIElement) newElement9);
      this.UI.AddUIElement((IUIElement) newElement8);
      this.UI.AddUIElement((IUIElement) newElement11);
      this.UI.AddUIElement((IUIElement) newElement12);
      this.UI.AddUIElement((IUIElement) newElement14);
      this.UI.AddUIElement((IUIElement) newElement13);
      this.SetCurrentVideoSettings();
    }

    private void SetCurrentVideoSettings()
    {
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
    }

    public void ApplyConfiguration()
    {
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
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
      PlatformerGame.Paused = true;
      this.SetCurrentVideoSettings();
    }

    public override void OnFinished()
    {
    }
  }
}
