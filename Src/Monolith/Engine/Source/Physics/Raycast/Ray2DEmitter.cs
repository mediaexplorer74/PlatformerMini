// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Experimental.Ray2DEmitter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace MonolithEngine.Experimental
{
  public class Ray2DEmitter
  {
    private List<Ray2D> rays;
    private Entity owner;
    private Vector2 closestIntersection;
    public float closestDistance;
    private Vector2 intersection = Vector2.Zero;
    private float delay;
    public Dictionary<Entity, Vector2> ClosestIntersections = new Dictionary<Entity, Vector2>();

    public Ray2DEmitter(Entity owner, float startDegree, float endDegree, int step, int delayMs = 0)
    {
      this.owner = owner;
      double num1 = (double) Math.Min(startDegree, endDegree);
      float num2 = Math.Max(startDegree, endDegree);
      this.rays = new List<Ray2D>();
      for (float angle = (float) num1; (double) angle <= (double) num2; angle += (float) step)
        this.rays.Add(new Ray2D(owner.Transform.Position, MathUtil.DegreesToRad(angle)));
      this.delay = (float) delayMs;
    }

    public void UpdateRays()
    {
      foreach (Ray2D ray in this.rays)
      {
        if ((double) this.delay != 0.0 && Timer.IsSet("RayCastDelay"))
          return;
        ray.Position = this.owner.Transform.Position;
        this.closestIntersection.X = this.closestIntersection.Y = (float) int.MaxValue;
        this.closestDistance = float.MaxValue;
        foreach (Entity key in new List<Entity>())
        {
          if (key.BlocksRay && !key.Equals((object) this.owner))
          {
            foreach ((Vector2 start, Vector2 end) rayBlockerLine in key.GetRayBlockerLines())
              ray.Cast(rayBlockerLine, ref this.intersection);
            if ((double) this.closestDistance < 3.4028234663852886E+38)
              this.ClosestIntersections[key] = this.closestIntersection;
            else if (this.ClosestIntersections.ContainsKey(key))
              this.ClosestIntersections.Remove(key);
          }
        }
      }
      if ((double) this.delay == 0.0)
        return;
      Timer.SetTimer("RayCastDelay", this.delay);
    }
  }
}
