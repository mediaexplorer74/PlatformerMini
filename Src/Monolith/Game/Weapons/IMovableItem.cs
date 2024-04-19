
// Type: ForestPlatformerExample.IMovableItem
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal interface IMovableItem
  {
    void Lift(Entity entity, Vector2 newPosition);

    void PutDown(Entity entity, Vector2 newPosition);

    void Throw(Entity entity, Vector2 force);
  }
}
