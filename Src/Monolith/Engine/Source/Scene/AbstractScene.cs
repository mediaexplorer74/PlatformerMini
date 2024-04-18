// Type: MonolithEngine.AbstractScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public abstract class AbstractScene : IScene
  {
    protected SceneManager SceneManager;
    protected string SceneName;
    internal bool Preload;
    internal bool AlwaysActive;
    private UserInterface ui;
    public CollisionEngine CollisionEngine;
    public GridCollisionChecker GridCollisionChecker;
    public List<Camera> Cameras;
    public Camera CurrentCamera;
    private int width;
    private int height;
    public bool UseLoadingScreen;
    public Color BackgroundColor = Color.White;

    public UserInterface UI => this.ui;

    public LayerManager LayerManager { get; }

    public int Width => this.width;

    public int Height => this.height;

    public AbstractScene(string sceneName, bool preload = false, bool alwaysActive = false, bool useLoadingScreen = false)
    {
      this.SceneName = sceneName != null && sceneName.Length != 0 ? sceneName : throw new Exception("The scene must have a non-null, non-empty unique name!");
      this.Preload = preload;
      this.AlwaysActive = alwaysActive;
      this.UseLoadingScreen = useLoadingScreen;
      this.ui = new UserInterface();
      this.LayerManager = new LayerManager(this);
      this.CollisionEngine = new CollisionEngine();
      this.GridCollisionChecker = new GridCollisionChecker();
    }

    internal void InternalLoad()
    {
      this.LayerManager.InitLayers();
      this.Load();
      this.UI.HandleNewElements();
    }

    public abstract void Load();

    public abstract void OnEnd();

    public abstract void OnStart();

    public abstract void OnFinished();

    public virtual void Unload()
    {
      this.LayerManager.Destroy();
      this.CollisionEngine.Destroy();
      this.GridCollisionChecker.Destroy();
      this.UI.Clear();
      Timer.Clear();
    }

    public abstract ICollection<object> ExportData();

    public void Finish() => this.OnFinished();

    public void OnResolitionChanged() => this.UI.OnResolutionChanged();

    public abstract ISceneTransitionEffect GetTransitionEffect();

    public abstract void ImportData(ICollection<object> state);

    internal void SetSceneManager(SceneManager sceneManager) => this.SceneManager = sceneManager;

    public string GetName() => this.SceneName;

    public virtual void Update()
    {
      this.LayerManager.UpdateAll();
      this.UI.Update();
    }

    public void FixedUpdate()
    {
      this.LayerManager.FixedUpdateAll();
      this.CollisionEngine.PostUpdate();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.LayerManager.DrawAll(spriteBatch);
      foreach (Camera camera in this.Cameras)
      {
        this.CurrentCamera = camera;
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: new Matrix?(camera.GetUITransformMatrix()));
        this.UI.Draw(spriteBatch);
        spriteBatch.End();
      }
    }

    internal void SetWidth(int width) => this.width = width;

    internal void SetHeight(int height) => this.height = height;
  }
}
