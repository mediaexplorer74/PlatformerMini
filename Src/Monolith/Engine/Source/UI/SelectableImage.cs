// Type: MonolithEngine.SelectableImage
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;


namespace MonolithEngine
{
  public class SelectableImage : Image, SelectableUIElement
  {
    public bool IsHoveredOver;// = false;
    public bool IsSelected;
    private Texture2D selectedImageTexture;
    private Rectangle unscaledSelectionBox;
    private Rectangle selectionBox;
    public UserInterface userInterface;
    public Action OnClick;
    public Action OnRelease;
    public Action HoverStartedAction;
    public Action HoverStoppedAction;
    public string HoverSoundEffectName;
    public string SelectSoundEffectName;
    private bool fireOnHold;
    private bool isBeingFired;

    public SelectableImage(
      Texture2D texture,
      Texture2D selectedImage = null,
      Vector2 position = default (Vector2),
      Rectangle sourceRectangle = default (Rectangle),
      float scale = 1f,
      bool fireOnHold = false,
      float rotation = 0.0f,
      int depth = 1,
      Color color = default (Color))
      : base(texture, position, sourceRectangle, scale, rotation, depth, color)
    {
      this.selectedImageTexture = selectedImage;
      this.unscaledSelectionBox = !(sourceRectangle == new Rectangle()) 
                ? new Rectangle((int) position.X + sourceRectangle.X, 
                (int) position.Y + sourceRectangle.Y, 
                (int) ((double) sourceRectangle.Width * (double) scale),
                (int) ((double) sourceRectangle.Height * (double) scale)) 
                : new Rectangle((int) position.X + sourceRectangle.X, 
                (int) position.Y + sourceRectangle.Y, 
                (int) ((double) this.ImageTexture.Width * (double) scale),
                (int) ((double) this.ImageTexture.Height * (double) scale));
      this.fireOnHold = fireOnHold;
      this.OnResolutionChanged();
    }

    public void OnResolutionChanged()
    {
      this.selectionBox = new Rectangle((int) (
          (double) this.unscaledSelectionBox.X * (double) Config.SCALE_X), 
          (int) ((double) this.unscaledSelectionBox.Y * (double) Config.SCALE_Y), 
          (int) ((double) this.unscaledSelectionBox.Width * (double) Config.SCALE_X), 
          (int) ((double) this.unscaledSelectionBox.Height * (double) Config.SCALE_Y));
    }

        private bool IsMouseOver(Point mousePosition)
        {
            return this.selectionBox.Contains(mousePosition);
        }

        private bool IsMouseOver(Vector2 mousePosition)
        {
            return this.selectionBox.Contains(mousePosition);
        }

        public override void Draw(SpriteBatch spriteBatch)
    {
            if (this.IsHoveredOver || this.IsSelected)
            {
                try
                {
                    if (this.selectedImageTexture != null)
                        spriteBatch.Draw(this.selectedImageTexture,
                            this.GetPosition(), new Rectangle?(this.SourceRectangle),
                            Color.White, this.Rotation, Vector2.Zero, this.Scale,
                            this.SpriteEffect, (float)this.Depth);
                    else
                    {
                       base.Draw(spriteBatch);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] SelectableImage bug: " + ex.Message);
                    //PLAN B
                    base.Draw(spriteBatch);
                }
            }
            else
                base.Draw(spriteBatch);
    }

    public override void Update(Point mousePosition = default (Point))
    {
      if (mousePosition != new Point() && this.IsMouseOver(mousePosition))
      {
        if (!this.IsHoveredOver)
        {
          if (this.HoverSoundEffectName != null)
            AudioEngine.Play(this.HoverSoundEffectName);

          Action hoverStartedAction = this.HoverStartedAction;

          if (hoverStartedAction != null)
            hoverStartedAction();
        }
        this.IsHoveredOver = true;
        this.userInterface.SelectElement((IUIElement) this);
      }
      else
      {
        if (this.IsHoveredOver)
        {
          Action hoverStoppedAction = this.HoverStoppedAction;
          if (hoverStoppedAction != null)
            hoverStoppedAction();
        }
        this.IsHoveredOver = false;
        this.userInterface.DeselectElement((IUIElement) this);
      }
    }

    public override void Update(TouchCollection touchLocations)
    {
      bool flag = false;
      foreach (TouchLocation touchLocation in touchLocations)
      {
        if (this.IsMouseOver(touchLocation.Position))
        {
          flag = true;
          if (this.fireOnHold)
            this.OnClick();
          else if (!this.isBeingFired)
            this.OnClick();
          this.isBeingFired = true;
          break;
        }
      }

      if (flag || !this.isBeingFired)
        return;

      Action onRelease = this.OnRelease;
      if (onRelease != null)
        onRelease();
      this.isBeingFired = false;
    }

    void SelectableUIElement.OnClick()
    {
      if (this.SelectSoundEffectName != null)
        AudioEngine.Play(this.SelectSoundEffectName);
      this.OnClick();
    }

        public void SetUserInterface(UserInterface userInterface)
        {
            this.userInterface = userInterface;
        }
    }
}
