// Decompiled with JetBrains decompiler
// Type: MonolithEngine.TileGroup
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace MonolithEngine
{
  public class TileGroup
  {
    private Dictionary<Vector2, Color[]> tiles = new Dictionary<Vector2, Color[]>();
    public static GraphicsDevice GraphicsDevice;
    private RenderTarget2D renderTarget;
    private SpriteBatch spriteBatch;
    private Dictionary<Texture2D, List<TileGroup.TileGroupEntry>> textures = new Dictionary<Texture2D, List<TileGroup.TileGroupEntry>>();

    public TileGroup(int width, int height)
    {
            //RnD
            try
            {
                this.renderTarget = new RenderTarget2D(TileGroup.GraphicsDevice,
                    width <= 4000 ? width : 4000,
                    height,
                    false,
                    TileGroup.GraphicsDevice.PresentationParameters.BackBufferFormat,
                    DepthFormat.Depth24
                    );
            }
            catch (Exception ex)
            {
                //Plan B
                this.renderTarget = new RenderTarget2D(TileGroup.GraphicsDevice,
                   10,
                   10,
                   false,
                   TileGroup.GraphicsDevice.PresentationParameters.BackBufferFormat,
                   DepthFormat.Depth24);
                Debug.WriteLine("[ex] RenderTarget2D(TileGroup ex: " + ex.Message);
            }
    }

    public void AddTile(
      Texture2D texture,
      Vector2 position,
      Rectangle sourceRectangle = default (Rectangle),
      SpriteEffects spriteEffects = SpriteEffects.None)
    {
      List<TileGroup.TileGroupEntry> tileGroupEntryList = new List<TileGroup.TileGroupEntry>();
      if (this.textures.ContainsKey(texture))
        tileGroupEntryList = this.textures[texture];
      Rectangle sourceRectangle1 = sourceRectangle == new Rectangle() ? new Rectangle(0, 0, texture.Width, texture.Height) : sourceRectangle;
      tileGroupEntryList.Add(new TileGroup.TileGroupEntry(position, sourceRectangle1, spriteEffects));
      this.textures[texture] = tileGroupEntryList;
    }

    public Texture2D GetTexture()
    {
      if (this.textures.Count == 0)
        throw new Exception("Attempted to create empty TileGroup...");

      this.spriteBatch = new SpriteBatch(TileGroup.GraphicsDevice);
      TileGroup.GraphicsDevice.SetRenderTarget(this.renderTarget);
      TileGroup.GraphicsDevice.Clear(Color.Transparent);
      this.spriteBatch.Begin();
      
      foreach (Texture2D key in this.textures.Keys)
      {
            foreach (TileGroup.TileGroupEntry tileGroupEntry in this.textures[key])
            {
                this.spriteBatch.Draw(key, tileGroupEntry.Position,
                    new Rectangle?(tileGroupEntry.SourceRectangle),
                    Color.White, 0.0f, Vector2.Zero,
                    1f,
                    tileGroupEntry.SpriteEffects,
                    0.0f);
            }
      }
      this.spriteBatch.End();

      TileGroup.GraphicsDevice.SetRenderTarget((RenderTarget2D) default);
      return (Texture2D) this.renderTarget;
    }

    public bool IsEmpty() => this.textures.Count == 0;

    private struct TileGroupEntry
    {
      public Vector2 Position;
      public Rectangle SourceRectangle;
      public SpriteEffects SpriteEffects;

      public TileGroupEntry(
        Vector2 position,
        Rectangle sourceRectangle,
        SpriteEffects spriteEffects)
      {
        this.Position = position;
        this.SourceRectangle = sourceRectangle;
        this.SpriteEffects = spriteEffects;
      }
    }
  }
}
