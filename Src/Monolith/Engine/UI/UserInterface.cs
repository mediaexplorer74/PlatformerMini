
// Type: MonolithEngine.UserInterface
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class UserInterface
  {
    private List<IUIElement> elements = new List<IUIElement>();
    private List<IUIElement> newElements = new List<IUIElement>();
    private List<IUIElement> removedElements = new List<IUIElement>();
    private IUIElement selectedElement;

    private TouchCollection currentTouchState;
    private TouchCollection prevTouchState;

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
      //RnD : TouchPanel handling
      if (1==1)//(MonolithGame.Platform.IsMobile())
      {
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        this.currentTouchState = TouchPanel.GetState();            

        if (/*this.selectedElement != null &&*/ this.currentTouchState.Count >= 1)
        {
            TouchCollection prevTouchState = this.prevTouchState;

            if( this.prevTouchState.Count == 0
                && this.selectedElement is SelectableUIElement  )
                (this.selectedElement as SelectableUIElement).OnClick();

            foreach (IUIElement element in this.elements)
                element.Update(this.currentTouchState
                        /*new Microsoft.Xna.Framework.Point(
                            (int)this.currentTouchState[0].Position.X,
                            (int)this.currentTouchState[0].Position.Y)*/);

                    
         }
         //this.HandleNewElements();
         //this.prevTouchState = this.currentTouchState;
      }        
                 
            
      // Mouse handling
      if (1==1)//else
      {
        this.currentMouseState = Mouse.GetState();
        if (this.selectedElement != null && this.currentMouseState.LeftButton == ButtonState.Pressed)
        {
          MouseState prevMouseState = this.prevMouseState;

          if 
          (
            this.prevMouseState.LeftButton != ButtonState.Pressed 
            && this.selectedElement is SelectableUIElement
          )
            (this.selectedElement as SelectableUIElement).OnClick();
        }

        foreach (IUIElement element in this.elements)
          element.Update(this.currentMouseState.Position);

        //this.HandleNewElements();
        //this.prevMouseState = this.currentMouseState;
      }
        //TEST IT
      //  foreach (IUIElement element in this.elements)
      //      element.Update(this.currentMouseState.Position);

       this.HandleNewElements();
       this.prevMouseState = this.currentMouseState;
       this.prevTouchState = this.currentTouchState;
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
