// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.AbstractInteractive
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;

#nullable disable
namespace ForestPlatformerExample
{
  internal class AbstractInteractive : AbstractDestroyable
  {
    protected new Random random;

    public AbstractInteractive(AbstractScene scene, Vector2 position)
      : base(scene, position)
    {
      this.AddTag("Interactive");
      this.random = new Random();
    }
  }
}
