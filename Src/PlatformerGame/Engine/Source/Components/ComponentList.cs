// Decompiled with JetBrains decompiler
// Type: MonolithEngine.ComponentList
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public class ComponentList
  {
    private IGameObject owner;
    private Dictionary<Type, List<IComponent>> components = new Dictionary<Type, List<IComponent>>();

    public ComponentList(IGameObject owner) => this.owner = owner;

    public T GetComponent<T>() where T : IComponent
    {
      return !this.components.ContainsKey(typeof (T)) ? default (T) : (T) this.components[typeof (T)][0];
    }

    public List<T> GetComponents<T>() where T : IComponent
    {
      if (!this.components.ContainsKey(typeof (T)))
        return new List<T>();
      List<T> components = new List<T>(this.components[typeof (T)].Count);
      foreach (IComponent component in this.components[typeof (T)])
        components.Add((T) component);
      return components;
    }

    public void AddComponent<T>(T newComponent) where T : IComponent
    {
      if (newComponent.UniquePerEntity && this.components.ContainsKey(newComponent.GetComponentType()) && this.components[newComponent.GetComponentType()].Count > 0)
        throw new Exception("Can't add more than on of the following component type: " + typeof (T).Name);
      if (!this.components.ContainsKey(typeof (T)))
        this.components[newComponent.GetComponentType()] = new List<IComponent>();
      this.components[newComponent.GetComponentType()].Add((IComponent) newComponent);
    }

    public void RemoveComponent<T>(T component) where T : IComponent
    {
      this.components[typeof (T)].Remove((IComponent) component);
    }

    public void RemoveComponent<T>() where T : IComponent => this.components[typeof (T)].Clear();

    public void Clear<T>() where T : IComponent => this.components.Remove(typeof (T));

    public void ClearAll() => this.components.Clear();

    public void DrawAll(SpriteBatch spriteBatch)
    {
      int count1 = this.components.Count;
      foreach (List<IComponent> componentList in this.components.Values)
      {
        int count2 = componentList.Count;
        foreach (IComponent component in componentList)
        {
          if (component is IDrawableComponent)
          {
            (component as IDrawableComponent).Draw(spriteBatch);
            if (count2 != componentList.Count)
              return;
          }
        }
        if (count1 != this.components.Count)
          break;
      }
    }

    public void UpdateAll()
    {
      int count1 = this.components.Count;
      foreach (List<IComponent> componentList in this.components.Values)
      {
        int count2 = componentList.Count;
        foreach (IComponent component in componentList)
        {
          if (component is IUpdatableComponent)
          {
            (component as IUpdatableComponent).Update();
            if (count2 != componentList.Count)
              return;
          }
        }
        if (count1 != this.components.Count)
          break;
      }
    }

    public void PreUpdateAll()
    {
      int count1 = this.components.Count;
      foreach (List<IComponent> componentList in this.components.Values)
      {
        int count2 = componentList.Count;
        foreach (IComponent component in componentList)
        {
          if (component is IUpdatableComponent)
          {
            (component as IUpdatableComponent).PreUpdate();
            if (count2 != componentList.Count)
              return;
          }
        }
        if (count1 != this.components.Count)
          break;
      }
    }

    public void PostUpdateAll()
    {
      int count1 = this.components.Count;
      foreach (List<IComponent> componentList in this.components.Values)
      {
        int count2 = componentList.Count;
        foreach (IComponent component in componentList)
        {
          if (component is IUpdatableComponent)
          {
            (component as IUpdatableComponent).PostUpdate();
            if (count2 != componentList.Count)
              return;
          }
        }
        if (count1 != this.components.Count)
          break;
      }
    }
  }
}
