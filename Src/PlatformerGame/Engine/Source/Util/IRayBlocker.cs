// Decompiled with JetBrains decompiler
// Type: MonolithEngine.IRayBlocker
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine
{
  public interface IRayBlocker
  {
    bool BlocksRay { get; set; }

    List<(Vector2 start, Vector2 end)> GetRayBlockerLines();
  }
}
