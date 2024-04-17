// Decompiled with JetBrains decompiler
// Type: MonolithEngine.UserInterface
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public class UserInterface
  {
    private List<IUIElement> elements = new List<IUIElement>();
    private List<IUIElement> newElements = new List<IUIElement>();
    private List<IUIElement> removedElements = new List<IUIElement>();
    private IUIElement selectedElement;
    private MouseState currentMouseState;
    private MouseState prevMouseState;

    public void OnResolutionChanged()
    {
      foreach (IUIElement element in this.elements)
      {
        if (element is SelectableUIElement)
          (element as SelectableUIElement).OnResolutionChanged();
      }
    }

    public void AddUIElement(IUIElement newElement)
    {
      this.newElements.Add(newElement);
      if (!(newElement is SelectableUIElement))
        return;
      (newElement as SelectableUIElement).SetUserInterface(this);
    }

    public void RemoveUIElement(IUIElement toRemove) => this.removedElements.Add(toRemove);

    public void Draw(SpriteBatch spriteBatch)
    {
      this.HandleNewElements();
      foreach (IUIElement element in this.elements)
        element.Draw(spriteBatch);
    }

    public void SelectElement(IUIElement selectedElement) => this.selectedElement = selectedElement;

    public void DeselectElement(IUIElement deselected)
    {
      if (this.selectedElement == null || !this.selectedElement.Equals((object) deselected))
        return;
      this.selectedElement = (IUIElement) null;
    }

    public void Update()
    {
            //TEMP
      if (1 == 0)//(MonolithGame.Platform.IsMobile())
      {
        foreach (IUIElement element in this.elements)
          element.Update(TouchPanel.GetState());
        this.HandleNewElements();
      }
      else
      {
        this.currentMouseState = Mouse.GetState();
        if (this.selectedElement != null && this.currentMouseState.LeftButton == ButtonState.Pressed)
        {
          MouseState prevMouseState = this.prevMouseState;
          if (this.prevMouseState.LeftButton != ButtonState.Pressed && this.selectedElement is SelectableUIElement)
            (this.selectedElement as SelectableUIElement).OnClick();
        }
        foreach (IUIElement element in this.elements)
          element.Update(this.currentMouseState.Position);
        this.HandleNewElements();
        this.prevMouseState = this.currentMouseState;
      }
    }

    internal void HandleNewElements()
    {
      if (this.newElements.Count > 0)
      {
        foreach (IUIElement newElement in this.newElements)
          this.elements.Add(newElement);
        this.newElements.Clear();
      }
      if (this.removedElements.Count <= 0)
        return;
      foreach (IUIElement removedElement in this.removedElements)
        this.elements.Remove(removedElement);
      this.removedElements.Clear();
    }

    public void Clear()
    {
      this.elements.Clear();
      this.newElements.Clear();
      this.removedElements.Clear();
    }
  }
}
