// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.CoinPickupEffect
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonolithEngine;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  public class CoinPickupEffect : Image
  {
    private readonly Vector2 TARGET = new Vector2(5f, 5f);
    private readonly float DURATION_MS = 500f;
    private AbstractScene scene;
    private Queue<(Vector2, float)> startPositions = new Queue<(Vector2, float)>();

    public CoinPickupEffect(AbstractScene scene)
      : base(Assets.GetTexture2D("CoinEffect"), Vector2.Zero, scale: 2f)
    {
      scene.UI.AddUIElement((IUIElement) this);
      this.scene = scene;
      Logger.Warn(" ### Coin pickup effect is still implemented as UI element, create proper particle system and replace it! ###");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.startPositions.Count == 0)
        return;
      for (int index = 0; index < this.startPositions.Count; ++index)
      {
        (Vector2, float) tuple = this.startPositions.Dequeue();
        if ((double) tuple.Item2 >= (double) this.DURATION_MS)
        {
          ++PlatformerGame.CoinCount;
          break;
        }
        Vector2 position = Vector2.Lerp(tuple.Item1, this.TARGET, tuple.Item2 / this.DURATION_MS);
        this.Scale = MathHelper.Lerp(2f, 1f, tuple.Item2 / this.DURATION_MS);
        tuple.Item2 += Globals.ElapsedTime;
        spriteBatch.Draw(this.ImageTexture, position, new Rectangle?(this.SourceRectangle), this.Color, this.Rotation, Vector2.Zero, this.Scale, this.SpriteEffect, (float) this.Depth);
        this.startPositions.Enqueue(tuple);
      }
    }

    public void AddCoin(Vector2 position)
    {
      this.startPositions.Enqueue((this.scene.Cameras[0].WorldToScreenSpace(position), 0.0f));
    }
  }
}
