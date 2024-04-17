// Decompiled with JetBrains decompiler
// Type: MonolithEngine.LayerManager
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public class LayerManager
  {
    private List<Layer> parallaxLayers = new List<Layer>();
    private List<List<Layer>> allLayers = new List<List<Layer>>();
    public Layer EntityLayer;
    internal Layer UILayer;
    private List<Layer> foregroundLayers = new List<Layer>();
    private List<Layer> backgroundLayers = new List<Layer>();
    private AbstractScene scene;
    internal bool Paused;

    public LayerManager(AbstractScene scene) => this.scene = scene;

    public void InitLayers()
    {
      this.EntityLayer = new Layer(this.scene, 10);
      this.UILayer = new Layer(this.scene, 10);
      this.UILayer.Pausable = false;
      this.allLayers.Add(this.parallaxLayers);
      this.allLayers.Add(this.backgroundLayers);
      this.allLayers.Add(new List<Layer>()
      {
        this.EntityLayer
      });
      this.allLayers.Add(this.foregroundLayers);
      this.allLayers.Add(new List<Layer>() { this.UILayer });
    }

    public void Destroy()
    {
      foreach (List<Layer> allLayer in this.allLayers)
      {
        foreach (Layer layer in allLayer)
          layer.Destroy();
        allLayer.Clear();
      }
      this.allLayers.Clear();
    }

    public void DrawAll(SpriteBatch spriteBatch)
    {
      foreach (List<Layer> allLayer in this.allLayers)
      {
        foreach (Layer layer in allLayer)
          layer.DrawAll(spriteBatch);
      }
    }

    public void UpdateAll()
    {
      foreach (List<Layer> allLayer in this.allLayers)
      {
        foreach (Layer layer in allLayer)
        {
          if (!this.Paused || !layer.Pausable)
            layer.UpdateAll();
        }
      }
    }

    public void FixedUpdateAll()
    {
      foreach (List<Layer> allLayer in this.allLayers)
      {
        foreach (Layer layer in allLayer)
        {
          if (!this.Paused || !layer.Pausable)
            layer.FixedUpdateAll();
        }
      }
    }

    public Layer CreateForegroundLayer(int priority = 0)
    {
      Layer newLayer = new Layer(this.scene, priority);
      this.AddLayer(this.foregroundLayers, newLayer);
      return newLayer;
    }

    public Layer CreateBackgroundLayer(int priority = 0)
    {
      Layer newLayer = new Layer(this.scene, priority);
      this.AddLayer(this.backgroundLayers, newLayer);
      return newLayer;
    }

    public Layer CreateParallaxLayer(int priority = 0, float scrollSpeedMultiplier = 1f, bool lockY = false)
    {
      Layer newLayer = new Layer(this.scene, priority, scrollSpeedModifier: scrollSpeedMultiplier, lockY: lockY);
      this.AddLayer(this.parallaxLayers, newLayer);
      return newLayer;
    }

    private void AddLayer(List<Layer> layer, Layer newLayer)
    {
      layer.Add(newLayer);
      layer.Sort((Comparison<Layer>) ((a, b) => a.Priority.CompareTo(b.Priority)));
    }

    private void RemoveLayer(List<Layer> layer, Layer toRemove)
    {
      layer.Remove(toRemove);
      layer.Sort((Comparison<Layer>) ((a, b) => a.Priority.CompareTo(b.Priority)));
    }
  }
}
