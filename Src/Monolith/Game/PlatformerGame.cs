
// Type: ForestPlatformerExample.PlatformerGame

// PlatformerGame is simple Platformer Example (it demonstares MonolithEngine using)


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonolithEngine;
using System.IO;


namespace ForestPlatformerExample
{
  public class PlatformerGame : MonolithGame
  {
    public static int CoinCount;
    private SpriteFont font;
    private KeyboardState prevKeyboardState;
    public static bool Paused;
    public static bool WasGameStarted;
    private LDTKMap world;
    public static string CurrentScene;

    public PlatformerGame() : base()
    {
    }

    protected override void Init()
    {
      Config.FIXED_UPDATE_FPS = 30;
      Config.CAMERA_DEADZONE = 10f;
      Logger.Info("Launching game...");
      this.font = this.Content.Load<SpriteFont>("Fonts/DefaultFont");
      Logger.LogToFile = true;
      Logger.Info("Graphics adapter: " + GraphicsAdapter.DefaultAdapter.Description);
      int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
      int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

      //RnD
      if (MonolithGame.Platform.IsMobile())
      {
        VideoConfiguration.RESOLUTION_WIDTH = this.Window.ClientBounds.Width;
        VideoConfiguration.RESOLUTION_HEIGHT = this.Window.ClientBounds.Height;
      }
      else
      {
        int num = this.GDC(width, height);
        if (width / num == 16 && height / num == 9)
        {
          VideoConfiguration.RESOLUTION_WIDTH = width;
          VideoConfiguration.RESOLUTION_HEIGHT = height;
          VideoConfiguration.FULLSCREEN = true;
        }
        else
        {
          VideoConfiguration.RESOLUTION_WIDTH = 1400;//1280;
          VideoConfiguration.RESOLUTION_HEIGHT = 680;//720;
          VideoConfiguration.FULLSCREEN = false;
        }
      }
      Logger.Info("Display resolution: " + VideoConfiguration.RESOLUTION_WIDTH.ToString() + "x" + VideoConfiguration.RESOLUTION_HEIGHT.ToString());
      Logger.Info("Supported display modes: ");
      foreach (object supportedDisplayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
        Logger.Info("\t" + supportedDisplayMode.ToString());
      VideoConfiguration.FRAME_LIMIT = 0;
      VideoConfiguration.VSYNC = false;
    }

    private int GDC(int a, int b)
    {
      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }
      return a | b;
    }

    protected override void LoadGameContent()
    {
      foreach (Camera camera in this.Cameras)
        camera.Limits = new Rectangle?(new Rectangle(0, 50, 5470, 700));

      Logger.Debug("Loading map from json...");
      using (Stream stream = TitleContainer.OpenStream(Path.Combine(this.Content.RootDirectory,
          "Map/level.json")))
        this.world = ((MapSerializer) new LDTKJsonMapParser()).Load(stream);

      Logger.Debug("Loading assets: HUD texts...");
      Assets.LoadTexture("HUDNewGameBase", "ForestAssets/UI/new_game_base");
      Assets.LoadTexture("HUDNewGameSelected", "ForestAssets/UI/new_game_selected");

      Assets.LoadTexture("HUDSettingsBase", "ForestAssets/UI/settings_base");
      Assets.LoadTexture("HUDSettingsSelected", "ForestAssets/UI/settings_selected");

      Assets.LoadTexture("HUDQuitBase", "ForestAssets/UI/quit_base");
      Assets.LoadTexture("HUDQuitSelected", "ForestAssets/UI/quit_selected");

      Assets.LoadTexture("HUDContinueBase", "ForestAssets/UI/continue_base");
      Assets.LoadTexture("HUDContinueSelected", "ForestAssets/UI/continue_selected");

      Assets.LoadTexture("HUDVideoSettingsBase", "ForestAssets/UI/video_base");
      Assets.LoadTexture("HUDVideoSettingsSelected", "ForestAssets/UI/video_selected");

      Assets.LoadTexture("HUDAudioSettingsBase", "ForestAssets/UI/audio_base");
      Assets.LoadTexture("HUDAudioSettingsSelected", "ForestAssets/UI/audio_selected");

      Assets.LoadTexture("HUDBackBase", "ForestAssets/UI/back_base");
      Assets.LoadTexture("HUDBackSelected", "ForestAssets/UI/back_selected");

      Assets.LoadTexture("HUDResolutionLabel", "ForestAssets/UI/resolution");
      Assets.LoadTexture("HUDFPSLimitLabel", "ForestAssets/UI/fps_limit");
      Assets.LoadTexture("HUDVsyncLabel", "ForestAssets/UI/vsync");
      Assets.LoadTexture("HUDWindowModeLabel", "ForestAssets/UI/window_mode");
      Assets.LoadTexture("HUD30", "ForestAssets/UI/30");
      Assets.LoadTexture("HUD60", "ForestAssets/UI/60");
      Assets.LoadTexture("HUD120", "ForestAssets/UI/120");
      Assets.LoadTexture("HUDUnlimited", "ForestAssets/UI/unlimited");
      Assets.LoadTexture("HUD720p", "ForestAssets/UI/720p");
      Assets.LoadTexture("HUD1080p", "ForestAssets/UI/1080p");
      Assets.LoadTexture("HUD1440p", "ForestAssets/UI/1440p");
      Assets.LoadTexture("HUD4K", "ForestAssets/UI/4k");
      Assets.LoadTexture("HUDOn", "ForestAssets/UI/on");
      Assets.LoadTexture("HUDOff", "ForestAssets/UI/off");
      Assets.LoadTexture("HUDApplyBase", "ForestAssets/UI/apply_base");
      Assets.LoadTexture("HUDApplySelected", "ForestAssets/UI/apply_selected");
      Assets.LoadTexture("HUDCancelBase", "ForestAssets/UI/cancel_base");
      Assets.LoadTexture("HUDCancelSelected", "ForestAssets/UI/cancel_selected");
      Assets.LoadTexture("HUDWindowed", "ForestAssets/UI/windowed");
      Assets.LoadTexture("HUDFullscreen", "ForestAssets/UI/fullscreen");
      Assets.LoadTexture("HUDArrowRightBase", "ForestAssets/UI/arrow_right_base");
      Assets.LoadTexture("HUDArrowRightSelected", "ForestAssets/UI/arrow_right_selected");
      Assets.LoadTexture("HUDArrowLeftBase", "ForestAssets/UI/arrow_right_base", flipHorizontal: true);
      Assets.LoadTexture("HUDArrowLeftSelected", "ForestAssets/UI/arrow_right_selected", flipHorizontal: true);
      Assets.LoadTexture("HUDLoading", "ForestAssets/UI/loading");

      Assets.LoadTexture("Level1Base", "ForestAssets/UI/level_1_base");
      Assets.LoadTexture("Level1Selected", "ForestAssets/UI/level_1_selected");

      Assets.LoadTexture("Level2Base", "ForestAssets/UI/level_2_base");
      Assets.LoadTexture("Level2Selected", "ForestAssets/UI/level_2_selected");

      Assets.LoadTexture("Level3Base", "ForestAssets/UI/level_3_base");
      Assets.LoadTexture("Level3Selected", "ForestAssets/UI/level_3_selected");

      Assets.LoadTexture("RestartBase", "ForestAssets/UI/restart_base");
      Assets.LoadTexture("RestartSelected", "ForestAssets/UI/restart_selected");
      Assets.LoadTexture("FinishedText", "ForestAssets/UI/finish");
      Assets.LoadTexture("HUDCointCount", "ForestAssets/UI/HUD-coin-count");
      Logger.Debug("Loading assets: entities...");
      Assets.LoadAnimationTexture("CarrotMove", "ForestAssets/Characters/Carrot/carrot@move-sheet");
      Assets.LoadAnimationTexture("CarrotHurt", "ForestAssets/Characters/Carrot/carrot@hurt-sheet");
      Assets.LoadAnimationTexture("CarrotDeath", "ForestAssets/Characters/Carrot/carrot@death-sheet");
      Assets.LoadAnimationTexture("CarrotIdle", "ForestAssets/Characters/Carrot/carrot@idle-sheet");
      Assets.LoadAnimationTexture("HeroHurt", "ForestAssets/Characters/Hero/main-character@hurt-sheet");
      Assets.LoadAnimationTexture("HeroIdle", "ForestAssets/Characters/Hero/main-character@idle-sheet");
      Assets.LoadAnimationTexture("HeroIdleWithItem", "ForestAssets/Characters/Hero/main-character@idle-with-item-sheet");
      Assets.LoadAnimationTexture("HeroRun", "ForestAssets/Characters/Hero/main-character@run-sheet");
      Assets.LoadAnimationTexture("HeroRunWithItem", "ForestAssets/Characters/Hero/main-character@run-with-item-sheet");
      Assets.LoadAnimationTexture("HeroJump", "ForestAssets/Characters/Hero/main-character@jump-sheet");
      Assets.LoadAnimationTexture("HeroJumpWithItem", "ForestAssets/Characters/Hero/main-character@jump-with-item-sheet");
      Assets.LoadAnimationTexture("HeroWallSlide", "ForestAssets/Characters/Hero/main-character@wall-slide-sheet", 64, 64);
      Assets.LoadAnimationTexture("HeroDoubleJump", "ForestAssets/Characters/Hero/main-character@double-jump-sheet");
      Assets.LoadAnimationTexture("HeroClimb", "ForestAssets/Characters/Hero/main-character@climb-sheet");
      Assets.LoadAnimationTexture("HeroAttack", "ForestAssets/Characters/Hero/main-character@attack-sheet");
      Assets.LoadAnimationTexture("HeroPickup", "ForestAssets/Characters/Hero/main-character@pick-up-sheet");
      Assets.LoadAnimationTexture("HeroSlide", "ForestAssets/Characters/Hero/main-character@slide-sheet");
      Assets.LoadAnimationTexture("TrunkAttack", "ForestAssets/Characters/Trunk/Attack (64x32)", 64, 32);
      Assets.LoadTexture("TrunkBullet", "ForestAssets/Characters/Trunk/Bullet", autoBoundingBox: true);
      Assets.GetTexture("TrunkBullet").SetSourceRectangle(Assets.GetTexture("TrunkBullet").GetBoundingBox());
      Assets.LoadAnimationTexture("TrunkHit", "ForestAssets/Characters/Trunk/Hit (64x32)", 64, 32);
      Assets.LoadAnimationTexture("TrunkIdle", "ForestAssets/Characters/Trunk/Idle (64x32)", 64, 32);
      Assets.LoadAnimationTexture("TrunkRun", "ForestAssets/Characters/Trunk/Run (64x32)", 64, 32);
      Assets.LoadAnimationTexture("TurtleSpikesIn", "ForestAssets/Characters/SpikedTurtle/Spikes in (44x26)", 44, 26);
      Assets.LoadAnimationTexture("TurtleSpikesOut", "ForestAssets/Characters/SpikedTurtle/Spikes out (44x26)", 44, 26);
      Assets.LoadAnimationTexture("TurtleHit", "ForestAssets/Characters/SpikedTurtle/Hit (44x26)", 44, 26);
      Assets.LoadAnimationTexture("TurtleIdleSpiked", "ForestAssets/Characters/SpikedTurtle/Idle 1 (44x26)", 44, 26);
      Assets.LoadAnimationTexture("TurtleIdleNormal", "ForestAssets/Characters/SpikedTurtle/Idle 2 (44x26)", 44, 26);
      Assets.LoadAnimationTexture("IceCreamIdle", "IcySkies/Characters/IceCream/ice-cream@idle");
      Assets.LoadAnimationTexture("IceCreamDeath", "IcySkies/Characters/IceCream/ice-cream@death");
      Assets.LoadAnimationTexture("IceCreamHurt", "IcySkies/Characters/IceCream/ice-cream@hurt");
      Assets.LoadAnimationTexture("IceCreamMove", "IcySkies/Characters/IceCream/ice-cream@move");
      Assets.LoadAnimationTexture("IceCreamAttack", "IcySkies/Characters/IceCream/ice-cream@attack");
      Assets.LoadAnimationTexture("IceCreamProjectileHit", "IcySkies/Characters/IceCream/ice-cream-projectile@hit", 45, 45);
      Assets.LoadAnimationTexture("IceCreamProjectileIdle", "IcySkies/Characters/IceCream/ice-cream-projectile@idle", 11, 11);
      Assets.LoadAnimationTexture("Rock1Idle", "IcySkies/Characters/Rock/Rock1_Idle (38x34)", 38, 34);
      Assets.LoadAnimationTexture("Rock1Run", "IcySkies/Characters/Rock/Rock1_Run (38x34)", 38, 34);
      Assets.LoadAnimationTexture("Rock1Hit", "IcySkies/Characters/Rock/Rock1_Hit", 38, 34);
      Assets.LoadAnimationTexture("Rock2Idle", "IcySkies/Characters/Rock/Rock2_Idle (32x28)", 32, 28);
      Assets.LoadAnimationTexture("Rock2Run", "IcySkies/Characters/Rock/Rock2_Run (32x28)", 32, 28);
      Assets.LoadAnimationTexture("Rock2Hit", "IcySkies/Characters/Rock/Rock2_Hit (32x28)", 32, 28);
      Assets.LoadAnimationTexture("Rock3Idle", "IcySkies/Characters/Rock/Rock3_Idle (22x18)", 22, 18);
      Assets.LoadAnimationTexture("Rock3Run", "IcySkies/Characters/Rock/Rock3_Run (22x18)", 22, 18);
      Assets.LoadAnimationTexture("Rock3Hit", "IcySkies/Characters/Rock/Rock3_Hit (22x18)", 22, 18);
      Assets.LoadAnimationTexture("GhostAppear", "IcySkies/Characters/Ghost/Appear (44x30)", 44, 30);
      Assets.LoadAnimationTexture("GhostDisappear", "IcySkies/Characters/Ghost/Desappear (44x30)", 44, 30);
      Assets.LoadAnimationTexture("GhostHit", "IcySkies/Characters/Ghost/Hit (44x30)", 44, 30);
      Assets.LoadAnimationTexture("GhostIdle", "IcySkies/Characters/Ghost/Idle (44x30)", 44, 30);
      Logger.Debug("Loading assets: traps and items...");
      Assets.LoadTexture("Saw", "IcySkies/Traps/Saw/saw");
      Assets.LoadAnimationTexture("CoinPickup", "ForestAssets/Items/coin-pickup");
      Assets.LoadAnimationTexture("CoinPickupEffect", "ForestAssets/Items/pickup-effect");
      Assets.LoadAnimationTexture("SpringAnim", "ForestAssets/Items/spring_spritesheet");
      Assets.LoadTexture("CoinEffect", "ForestAssets/Items/coin_effect");
      Assets.LoadTexture("ForestTileset", "ForestAssets/Tiles/forest-tileset");
      Assets.LoadAnimationTexture("BoxIdle", "ForestAssets/Items/box-idle");
      Assets.LoadAnimationTexture("BoxHit", "ForestAssets/Items/box-hit");
      Assets.LoadAnimationTexture("BoxDestroy", "ForestAssets/Items/box-destroy");
      Assets.LoadAnimationTexture("FanAnim", "IcySkies/Items/Fan/fan", 24, 8);
      Assets.LoadTexture("FinishedTrophy", "IcySkies/Items/POI/End (Idle)", autoBoundingBox: true);
      Logger.Debug("Loading assets: fonts...");
      Assets.AddFont("InGameText", this.Content.Load<SpriteFont>("Text/InGameText"));

      Logger.Debug("Loading assets: sounds...");
      AudioEngine.AddSound("Level1Music", "ForestAssets/Audio/POL-chubby-cat-long", true, AudioTag.MUSIC);
      AudioEngine.AddSound("Level2Music", "IcySkies/Audio/level_2_bg_2", true, AudioTag.MUSIC);
      AudioEngine.AddSound("Level3Music", "ForestAssets/Audio/POL-chubby-cat-long", true, AudioTag.MUSIC); //RnD

      AudioEngine.AddSound("HeroPunch", "ForestAssets/Audio/hero_punch");
      AudioEngine.AddSound("SpringBounceSound", "ForestAssets/Audio/spring");
      AudioEngine.AddSound("JumpSound", "ForestAssets/Audio/jump2", maxVolume: 0.5f);
      AudioEngine.AddSound("BoxBounceSound", "ForestAssets/Audio/box_bounce");
      AudioEngine.AddSound("CoinPickupSound", "ForestAssets/Audio/coin_pickup");
      AudioEngine.AddSound("HeroHurtSound", "ForestAssets/Audio/hero_hurt");
      AudioEngine.AddSound("CarrotJumpHurtSound", "ForestAssets/Audio/carrot_jump_hurt");
      AudioEngine.AddSound("BoxExplosionSound", "ForestAssets/Audio/box_explosion");
      AudioEngine.AddSound("FastFootstepsSound", "ForestAssets/Audio/footsteps");
      AudioEngine.AddSound("SlowFootstepsSound", "ForestAssets/Audio/footsteps_slow", true);
      AudioEngine.AddSound("CarrotExplodeSound", "ForestAssets/Audio/carrot_explode");
      AudioEngine.AddSound("MenuHover", "ForestAssets/Audio/menu_hover");
      AudioEngine.AddSound("MenuSelect", "ForestAssets/Audio/menu_select");
      AudioEngine.AddSound("TrunkHit", "ForestAssets/Audio/trunk_damage");
      AudioEngine.AddSound("TrunkShoot", "ForestAssets/Audio/trunk_shoot");
      AudioEngine.AddSound("TrunkDeath", "ForestAssets/Audio/trunk_death");
      AudioEngine.AddSound("LadderClimb", "ForestAssets/Audio/sfx_movement_ladder5loop", true);
      AudioEngine.AddSound("BoxPickup", "ForestAssets/Audio/sfx_sounds_interaction19");
      AudioEngine.AddSound("IceCreamExplode", "ForestAssets/Audio/sfx_weapon_singleshot3");
      AudioEngine.AddSound("GostDisappear", "ForestAssets/Audio/sfx_wpn_laser11");
      AudioEngine.AddSound("GostAppear", "ForestAssets/Audio/sfx_wpn_laser11");
      AudioEngine.AddSound("RockSplit", "ForestAssets/Audio/sfx_sounds_impact10");

      if (MonolithGame.Platform.IsMobile())
      {
        Assets.LoadTexture("LeftArrow", "MobileButtons/left_arrow");
        Assets.LoadTexture("RightArrow", "MobileButtons/right_arrow");
        Assets.LoadTexture("UpArrow", "MobileButtons/up_arrow");
        Assets.LoadTexture("DownArrow", "MobileButtons/down_arrow");
        Assets.LoadTexture("XButton", "MobileButtons/x_button");
        Assets.LoadTexture("SquareButton", "MobileButtons/square_button");
        Assets.LoadTexture("CircleButton", "MobileButtons/circle_button");
        Assets.LoadTexture("TriangleButton", "MobileButtons/triangle_button");
        Assets.LoadTexture("MenuButton", "MobileButtons/menu_button");
      }

      Logger.Debug("Loading scenes...");
      MainMenuScene mainMenuScene = new MainMenuScene();
      PauseMenuScene pauseMenuScene = new PauseMenuScene();

      
      VideoSettingsScene videoSettingsScene = new VideoSettingsScene();
      AudioSettingsScene audioSettingsScene = new AudioSettingsScene();

      LoadingScreenScene loadingScreenScene = new LoadingScreenScene();

      LevelSelectScreenScene levelSelectScreenScene = new LevelSelectScreenScene();
      Level1Scene level1Scene = new Level1Scene(this.world, this.font);
      Level2Scene level2Scene = new Level2Scene(this.world, this.font);
      Level3Scene level3Scene = new Level3Scene(this.world, this.font);
     

      GameEndScene gameEndScene = new GameEndScene();

      // RnD
      if (1==1)//(!MonolithGame.Platform.IsMobile())
        this.SceneManager.AddScene((AbstractScene) new SettingsScene());
      
      this.world = (LDTKMap) null;
      this.SceneManager.AddScene((AbstractScene) mainMenuScene);
      this.SceneManager.AddScene((AbstractScene) pauseMenuScene);
      
      this.SceneManager.AddScene((AbstractScene)levelSelectScreenScene);

      this.SceneManager.AddScene((AbstractScene) videoSettingsScene);
      this.SceneManager.AddScene((AbstractScene)audioSettingsScene);

      this.SceneManager.AddScene((AbstractScene) loadingScreenScene);

      this.SceneManager.AddScene((AbstractScene) level1Scene);
      this.SceneManager.AddScene((AbstractScene) level2Scene);
      this.SceneManager.AddScene((AbstractScene) level3Scene);


      this.SceneManager.AddScene((AbstractScene) gameEndScene);

      Logger.Debug("Starting main menu...");
      this.SceneManager.SetLoadingScene((AbstractScene) loadingScreenScene);
      this.SceneManager.LoadScene((AbstractScene) mainMenuScene);
    }

    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      KeyboardState state = Keyboard.GetState();
      if (this.prevKeyboardState != state && state.IsKeyDown(Keys.R))
        this.SceneManager.LoadScene(PlatformerGame.CurrentScene);
      else if (this.prevKeyboardState != state && state.IsKeyDown(Keys.Escape) 
                && PlatformerGame.WasGameStarted && !PlatformerGame.Paused)
        this.SceneManager.StartScene("PauseMenu");
      else if (this.prevKeyboardState != state && state.IsKeyDown(Keys.Escape) 
                && PlatformerGame.WasGameStarted && PlatformerGame.Paused)
        this.SceneManager.StartScene(PlatformerGame.CurrentScene);
      else if (this.prevKeyboardState != state && state.IsKeyDown(Keys.Escape)
                && !PlatformerGame.WasGameStarted)
        this.SceneManager.StartScene("MainMenu");
      this.prevKeyboardState = state;
    }
  }
}
