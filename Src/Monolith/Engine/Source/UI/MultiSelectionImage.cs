// Decompiled with JetBrains decompiler
// Type: MonolithEngine.MultiSelectionImage
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class MultiSelectionImage : Image
  {
    private List<MultiSelectionImage.Option> options = new List<MultiSelectionImage.Option>();
    private MultiSelectionImage.Option selected;

    public MultiSelectionImage(
      Vector2 position = default (Vector2),
      Rectangle sourceRectangle = default (Rectangle),
      float scale = 1f,
      float rotation = 0.0f,
      int depth = 1,
      Color color = default (Color))
      : base((Texture2D) null, position, sourceRectangle, scale, rotation, depth, color)
    {
    }

    public void AddOption(string label, Texture2D texture)
    {
      MultiSelectionImage.Option option = new MultiSelectionImage.Option(texture, label);
      this.options.Add(option);
      if (this.selected != null)
        return;
      this.SetSelected(option);
    }

    private void SetSelected(MultiSelectionImage.Option option)
    {
      this.selected = option;
      this.ImageTexture = option.texture;
      this.SourceRectangle = new Rectangle(0, 0, this.ImageTexture.Width, this.ImageTexture.Height);
    }

    public string GetSelection() => this.selected.label;

    public void SetSelected(string label)
    {
      foreach (MultiSelectionImage.Option option in this.options)
      {
        if (option.label.Equals(label))
        {
          this.SetSelected(option);
          return;
        }
      }
      throw new Exception("Element not found: " + label);
    }

    public void Next()
    {
      this.SetSelected(this.options[this.GetNextIndex(this.options.IndexOf(this.selected) + 1)]);
    }

    public void Previous()
    {
      this.SetSelected(this.options[this.GetNextIndex(this.options.IndexOf(this.selected) - 1)]);
    }

    private int GetNextIndex(int index)
    {
      if (index < 0)
        return this.options.Count - 1;
      return index == this.options.Count ? 0 : index;
    }

    private class Option
    {
      public Texture2D texture;
      public string label;

      public Option(Texture2D texture, string label)
      {
        this.texture = texture;
        this.label = label;
      }
    }
  }
}
