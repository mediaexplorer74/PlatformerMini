// Type: MonolithEngine.SceneManager
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace MonolithEngine
{
  public class SceneManager
  {
    private Dictionary<string, AbstractScene> scenes = new Dictionary<string, AbstractScene>();
    private HashSet<AbstractScene> activeScenes = new HashSet<AbstractScene>();
    private AbstractScene currentScene;
    private AbstractScene loadingScreen;
    private AbstractScene nextSceneToLoad;
    private AbstractScene nextSceneToStart;
    private List<Camera> cameras;
    private GraphicsDevice graphicsDevice;
    private bool isLoading;
    private bool useLoadingScreen;

    public SceneManager(List<Camera> cameras, GraphicsDevice graphicsDevice)
    {
      this.cameras = cameras;
      this.graphicsDevice = graphicsDevice;
    }

    public void AddScene(AbstractScene scene)
    {
      if (this.scenes.ContainsKey(scene.GetName()))
        throw new Exception("Scene name already exists!");
      this.scenes.Add(scene.GetName(), scene);
      scene.Cameras = this.cameras;
      scene.CurrentCamera = this.cameras[0];
      scene.SetSceneManager(this);
      if (!scene.Preload)
        return;
      scene.InternalLoad();
    }

    public void SetLoadingScene(AbstractScene loadingScene) => this.loadingScreen = loadingScene;

    public void OnResolutionChanged()
    {
      foreach (AbstractScene abstractScene in this.scenes.Values)
        abstractScene.OnResolitionChanged();
    }

    public void RemoveScene(AbstractScene scene)
    {
        this.scenes.Remove(scene.GetName());
    }

    public void LoadScene(string sceneName)
    {
        try
        {
            this.nextSceneToLoad = this.scenes[sceneName];
        }
        catch { }
    }

    private void LoadNextScene()
    {
      ICollection<object> state = (ICollection<object>) null;

      if (this.currentScene != null)
      {
        AudioEngine.StopSoundEffects();
        state = this.currentScene.ExportData();
        this.currentScene.OnEnd();
        this.currentScene.Unload();
        if (!this.currentScene.AlwaysActive)
          this.activeScenes.RemoveIfExists<AbstractScene>(this.currentScene);
        this.scenes.Remove(this.currentScene.GetName());
      }

      this.currentScene = this.nextSceneToLoad;
      this.nextSceneToLoad = (AbstractScene) null;
      this.activeScenes.AddIfMissing<AbstractScene>(this.currentScene);
      this.currentScene.InternalLoad();
      this.currentScene.ImportData(state);
      this.currentScene.OnStart();
      this.isLoading = false;
      this.useLoadingScreen = false;
    }

    public void StartScene(string sceneName)
    {
        try
        {
            this.nextSceneToStart = this.scenes[sceneName];
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] SceneManager - StartScene bug: " + ex.Message);
        }
    }

    private void StartNextScene()
    {
      ICollection<object> state = (ICollection<object>) null;
      if (this.currentScene != null)
      {
        AudioEngine.StopSoundEffects();
        state = this.currentScene.ExportData();
        this.currentScene.OnEnd();
        if (!this.currentScene.AlwaysActive)
          this.activeScenes.RemoveIfExists<AbstractScene>(this.currentScene);
      }
      this.currentScene = this.nextSceneToStart;
      this.nextSceneToStart = (AbstractScene) null;
      this.activeScenes.AddIfMissing<AbstractScene>(this.currentScene);
      this.currentScene.ImportData(state);
      this.currentScene.OnStart();
      this.isLoading = false;
      this.useLoadingScreen = false;
    }

    public void StartScene(AbstractScene scene) => this.StartScene(scene.GetName());

    public void LoadScene(AbstractScene scene) => this.LoadScene(scene.GetName());

    internal bool IsEmpty() => this.scenes.Count == 0;

    public void FixedUpdate()
    {
      foreach (AbstractScene activeScene in this.activeScenes)
        activeScene.FixedUpdate();
      this.HandleSceneTransition();
    }

    internal void Update()
    {
      foreach (AbstractScene activeScene in this.activeScenes)
        activeScene.Update();
      this.HandleSceneTransition();
    }

    private void HandleSceneTransition()
    {
      if (this.nextSceneToLoad != null)
      {
        if (this.nextSceneToLoad.UseLoadingScreen && !this.isLoading && this.loadingScreen != null)
        {
          this.useLoadingScreen = true;
          return;
        }
        this.LoadNextScene();
      }
      if (this.nextSceneToStart == null)
        return;
      if (this.nextSceneToStart.UseLoadingScreen && !this.isLoading && this.loadingScreen != null)
        this.useLoadingScreen = true;
      else
        this.StartNextScene();
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
      this.graphicsDevice.Clear(this.currentScene.BackgroundColor);
      if (this.useLoadingScreen && this.loadingScreen != null)
      {
        this.isLoading = true;
        this.loadingScreen.Draw(spriteBatch);
      }
      else
        this.currentScene.Draw(spriteBatch);
    }
  }
}
