
// Type: MonolithEngine.Layer
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class Layer
  {
    private List<Entity> activeObjects = new List<Entity>();
    private List<Entity> visibleObjects = new List<Entity>();
    private List<Entity> changedObjects = new List<Entity>();
    private float scrollSpeedModifier;
    private bool lockY;
    private bool ySorting;
    public bool Visible = true;
    public bool Active = true;
    internal bool Pausable = true;
    public static GraphicsDeviceManager GraphicsDeviceManager;
    public int Priority;
    public AbstractScene Scene;

    public float Depth { get; set; }

    internal Layer(
      AbstractScene scene,
      int priority = 0,
      bool ySorting = false,
      float scrollSpeedModifier = 1f,
      bool lockY = true)
    {
      this.scrollSpeedModifier = scrollSpeedModifier;
      this.Priority = priority;
      this.lockY = lockY;
      this.ySorting = ySorting;
      this.Scene = scene;
    }

    public void OnObjectChanged(Entity gameObject) => this.changedObjects.Add(gameObject);

    public IEnumerable<Entity> GetAll() => (IEnumerable<Entity>) this.visibleObjects;

    public void SortByPriority()
    {
      if (this.visibleObjects.Count == 0)
        return;
      this.visibleObjects.Sort((Comparison<Entity>) ((a, b) => a.DrawPriority.CompareTo(b.DrawPriority)));
    }

    public void DrawAll(SpriteBatch spriteBatch)
    {
      if (this.Visible)
      {
        if (this.ySorting)
          this.visibleObjects.Sort((Comparison<Entity>) ((a, b) =>
          {
            int num = a.DrawPriority.CompareTo(b.DrawPriority);
            return num != 0 ? num : a.Transform.Y.CompareTo(b.Transform.Y);
          }));
        Viewport viewport = Layer.GraphicsDeviceManager.GraphicsDevice.Viewport;
        foreach (Camera camera in this.Scene.Cameras)
        {
          Layer.GraphicsDeviceManager.GraphicsDevice.Viewport = camera.Viewport;
          this.Scene.CurrentCamera = camera;

          spriteBatch.Begin(samplerState: SamplerState.PointClamp, 
              transformMatrix: new Matrix?(camera.GetWorldTransformMatrix(this.scrollSpeedModifier, 
              this.lockY)));

          foreach (Entity visibleObject in this.visibleObjects)
          {
            if (visibleObject.Visible)
              visibleObject.Draw(spriteBatch);
          }
          spriteBatch.End();
        }
        Layer.GraphicsDeviceManager.GraphicsDevice.Viewport = viewport;
      }
      this.HandleChangedObjects();
    }

    public void UpdateAll()
    {
      if ((double) Globals.ElapsedTime > 1000.0)
        return;
      if (this.Active)
      {
        foreach (Entity activeObject in this.activeObjects)
        {
          if (activeObject.Active)
          {
            activeObject.PreUpdate();
            activeObject.Update();
            activeObject.PostUpdate();
          }
        }
      }
      this.HandleChangedObjects();
    }

    public void FixedUpdateAll()
    {
      if ((double) Globals.ElapsedTime > 1000.0)
        return;
      if (this.Active)
      {
        foreach (Entity activeObject in this.activeObjects)
        {
          if (activeObject.Active)
          {
            activeObject.PreFixedUpdate();
            activeObject.FixedUpdate();
          }
        }
      }
      this.HandleChangedObjects();
    }

    private void HandleChangedObjects()
    {
      if (this.changedObjects.Count <= 0)
        return;
      foreach (Entity changedObject in this.changedObjects)
      {
        if (changedObject.Visible)
        {
          if (changedObject.Parent == null && !this.visibleObjects.Contains(changedObject))
            this.visibleObjects.Add(changedObject);
          else if (changedObject.Parent != null && this.visibleObjects.Contains(changedObject))
            this.visibleObjects.Remove(changedObject);
        }
        else
          this.visibleObjects.Remove(changedObject);
        if (changedObject.Active)
        {
          if (changedObject.Parent == null && !this.activeObjects.Contains(changedObject))
            this.activeObjects.Add(changedObject);
          else if (changedObject.Parent != null && this.activeObjects.Contains(changedObject))
            this.activeObjects.Remove(changedObject);
        }
        else
          this.activeObjects.Remove(changedObject);
      }
      this.changedObjects.Clear();
      this.SortByPriority();
    }

    public void Destroy()
    {
      foreach (GameObject activeObject in this.activeObjects)
        activeObject.Destroy();
      foreach (GameObject visibleObject in this.visibleObjects)
        visibleObject.Destroy();
      foreach (GameObject changedObject in this.changedObjects)
        changedObject.Destroy();
      this.HandleChangedObjects();
    }
  }
}
