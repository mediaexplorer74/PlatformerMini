// Decompiled with JetBrains decompiler
// Type: MonolithEngine.MonolithGame
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MonolithEngine
{
  public abstract class MonolithGame : Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    protected List<Camera> Cameras;
    private SpriteFont font;
    private FrameCounter frameCounter;
    private int fixedUpdateRate;
    protected SceneManager SceneManager;
    protected CameraMode CameraMode;
    private readonly int DEFAULT_FIXED_UPDATE_RATE = 30;
    private static MonolithEngine.Platform platform;
    public static bool IsGameStarted;
    private float fixedUpdateElapsedTime;
    private float fixedUpdateDelta = 0.33f;
    private float previousT;
    private float accumulator;
    private float maxFrameTime = 250f;
    private float lastPrint;
    private string fps = "";

    public static MonolithEngine.Platform Platform => MonolithGame.platform;

    public MonolithGame()//(MonolithEngine.Platform targetPlatform)
    {
      MonolithGame.platform = Platform.UWP;//targetPlatform;
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
      this.IsMouseVisible = true;
      Config.GRAVITY_ON = true;
      Config.GRAVITY_FORCE = 0.3f;
      Config.JUMP_FORCE = 1f;
      Config.INCREASING_GRAVITY = true;
      Config.FIXED_UPDATE_FPS = 30;
      Globals.FixedUpdateMultiplier = 1f;
    }

    public void ApplyVideoConfiguration()
    {
      if (VideoConfiguration.FRAME_LIMIT == 0)
      {
        this.IsFixedTimeStep = false;
      }
      else
      {
        this.IsFixedTimeStep = true;
        this.TargetElapsedTime = TimeSpan.FromTicks(10000000L / (long) VideoConfiguration.FRAME_LIMIT);
      }

      //this.graphics.SynchronizeWithVerticalRetrace = VideoConfiguration.VSYNC;
      this.graphics.PreferredBackBufferWidth = VideoConfiguration.RESOLUTION_WIDTH;
      this.graphics.PreferredBackBufferHeight = VideoConfiguration.RESOLUTION_HEIGHT;
      this.graphics.IsFullScreen = VideoConfiguration.FULLSCREEN;

       //RnD
       Config.SCALE_X = (1 == 1)//!MonolithGame.Platform.IsMobile() 
        ? (float) ((double) VideoConfiguration.RESOLUTION_WIDTH / 1920.0  * 1.4/*2.0*/)
        : (float) ((double) VideoConfiguration.RESOLUTION_HEIGHT / 1920.0 * 2.5/*2.7999999523162842*/);

       Config.SCALE_Y = (1 == 1)//!MonolithGame.Platform.IsMobile() 
        ? (float)((double)VideoConfiguration.RESOLUTION_WIDTH / 1920.0 * 4.2/*2.0*/)
        : (float)((double)VideoConfiguration.RESOLUTION_HEIGHT / 1920.0 * 3/*2.7999999523162842*/);

      try
      {
        this.graphics.ApplyChanges();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] graphics.ApplyChanges error: " + ex.Message);
      }
    }

    protected override sealed void Initialize()
    {
      AssetUtil.Content = this.Content;
      AssetUtil.GraphicsDeviceManager = this.graphics;
      MonolithTexture.GraphicsDevice = this.graphics.GraphicsDevice;
      Layer.GraphicsDeviceManager = this.graphics;
      TileGroup.GraphicsDevice = this.graphics.GraphicsDevice;
      VideoConfiguration.GameInstance = this;
      this.Init();
      Logger.Info("Engine initialized with " + Config.FIXED_UPDATE_FPS.ToString() + " FPS");
      this.fixedUpdateRate = Config.FIXED_UPDATE_FPS == 0 ? 0 : (int) (1000.0 / (double) Config.FIXED_UPDATE_FPS);
      Globals.FixedUpdateRate = TimeSpan.FromTicks(10000000L / (long) Config.FIXED_UPDATE_FPS);
      Globals.FixedUpdateMultiplier = (float) this.DEFAULT_FIXED_UPDATE_RATE / (float) Config.FIXED_UPDATE_FPS;
      base.Initialize();
    }

    protected abstract void Init();

    protected override sealed void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      if (VideoConfiguration.RESOLUTION_WIDTH == 0 || VideoConfiguration.RESOLUTION_HEIGHT == 0)
      {
        VideoConfiguration.RESOLUTION_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        VideoConfiguration.RESOLUTION_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
      }
      this.ApplyVideoConfiguration();
      Config.ExitAction = new Action(((Game) this).Exit);
      this.Cameras = new List<Camera>();
      if (this.CameraMode == CameraMode.SINGLE)
      {
        this.Cameras.Add(new Camera(this.graphics));
      }
      else
      {
        this.Cameras.Add(new Camera(this.graphics, this.CameraMode));
        this.Cameras.Add(new Camera(this.graphics, this.CameraMode, 1));
      }
      this.SceneManager = new SceneManager(this.Cameras, this.graphics.GraphicsDevice);
      this.font = this.Content.Load<SpriteFont>("Fonts/DefaultFont");
      this.frameCounter = new FrameCounter();
      this.LoadGameContent();
      if (this.SceneManager.IsEmpty())
        throw new Exception("No scene added to the game!");
      MonolithGame.IsGameStarted = true;
    }

    protected abstract void LoadGameContent();

    protected override void Update(GameTime gameTime)
    {
      if (gameTime.ElapsedGameTime.TotalSeconds > 0.1)
      {
        this.accumulator = 0.0f;
        this.previousT = 0.0f;
      }
      else
      {
        TimeSpan timeSpan;
        if ((double) this.previousT == 0.0)
        {
          this.fixedUpdateDelta = (float) this.fixedUpdateRate;
          timeSpan = gameTime.TotalGameTime;
          this.previousT = (float) timeSpan.TotalMilliseconds;
        }
        timeSpan = gameTime.ElapsedGameTime;
        double totalMilliseconds1;
        Globals.ElapsedTime = (float) (totalMilliseconds1 = timeSpan.TotalMilliseconds);
        Globals.GameTime = gameTime;
        Timer.Update((float) totalMilliseconds1);
        foreach (Camera camera in this.Cameras)
          camera.Update();
        float totalMilliseconds2 = (float) gameTime.TotalGameTime.TotalMilliseconds;
        float num = totalMilliseconds2 - this.previousT;
        if ((double) num > (double) this.maxFrameTime)
          num = this.maxFrameTime;
        this.previousT = totalMilliseconds2;
        for (this.accumulator += num; (double) this.accumulator >= (double) this.fixedUpdateDelta; this.accumulator -= this.fixedUpdateDelta)
        {
          this.FixedUpdate();
          this.fixedUpdateElapsedTime += this.fixedUpdateDelta;
        }
        Globals.FixedUpdateAlpha = this.accumulator / this.fixedUpdateDelta;
        this.SceneManager.Update();
        base.Update(gameTime);
      }
    }

    protected void FixedUpdate() => this.SceneManager.FixedUpdate();

    protected override void Draw(GameTime gameTime)
    {
      this.SceneManager.Draw(this.spriteBatch);
      base.Draw(gameTime);
    }
  }
}
