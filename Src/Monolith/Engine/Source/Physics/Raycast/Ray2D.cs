// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Experimental.Ray2D
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine.Experimental
{
  public class Ray2D
  {
    public Vector2 Position;
    private Vector2 direction;
    private float angleRad;
    private float x4;
    private float y4;
    private float den;
    private float t;
    private float u;

    public Ray2D(Vector2 position, Vector2 direction)
    {
      this.Position = position;
      this.direction = direction;
      this.angleRad = MathUtil.RadFromVectors(position, direction);
      this.angleRad = (float) Math.Atan2((double) direction.Y - (double) position.Y, (double) direction.X - (double) position.X);
    }

    public Ray2D(Vector2 position, float angleRad = 0.0f)
    {
      this.Position = position;
      this.angleRad = angleRad;
      this.direction = MathUtil.RadToVector(angleRad);
    }

    public void Cast((Vector2 from, Vector2 to) target, ref Vector2 result)
    {
      this.x4 = this.Position.X + this.direction.X;
      this.y4 = this.Position.Y + this.direction.Y;
      this.den = (float) (((double) target.from.X - (double) target.to.X) * ((double) this.Position.Y - (double) this.y4) - ((double) target.from.Y - (double) target.to.Y) * ((double) this.Position.X - (double) this.x4));
      if ((double) this.den == 0.0)
      {
        result.X = result.Y = 0.0f;
      }
      else
      {
        this.t = (float) (((double) target.from.X - (double) this.Position.X) * ((double) this.Position.Y - (double) this.y4) - ((double) target.from.Y - (double) this.Position.Y) * ((double) this.Position.X - (double) this.x4)) / this.den;
        this.u = (float) -(((double) target.from.X - (double) target.to.X) * ((double) target.from.Y - (double) this.Position.Y) - ((double) target.from.Y - (double) target.to.Y) * ((double) target.from.X - (double) this.Position.X)) / this.den;
        if ((double) this.t > 0.0 && (double) this.t < 1.0 && (double) this.u > 0.0)
        {
          result.X = target.from.X + this.t * (target.to.X - target.from.X);
          result.Y = target.from.Y + this.t * (target.to.Y - target.from.Y);
        }
        else
          result.X = result.Y = 0.0f;
      }
    }
  }
}
