// Decompiled with JetBrains decompiler
// Type: MonolithEngine.MathUtil
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace MonolithEngine
{
  public class MathUtil
  {
    public static Vector2 RadToVector(float angleRad)
    {
      return new Vector2((float) Math.Cos((double) angleRad), (float) Math.Sin((double) angleRad));
    }

    public static Vector2 EndPointOfLine(Vector2 start, float length, float angleRad)
    {
      return new Vector2(start.X + length * (float) Math.Cos((double) angleRad), start.Y + length * (float) Math.Sin((double) angleRad));
    }

    public static float DegreesToRad(float angle) => (float) Math.PI / 180f * angle;

    public static float RadFromVectors(Vector2 v1, Vector2 v2)
    {
      return (float) Math.Atan2((double) v2.Y - (double) v1.Y, (double) v2.X - (double) v1.X);
    }

    public static float DegreeFromVectors(Vector2 v1, Vector2 v2)
    {
      return (float) ((double) MathUtil.RadFromVectors(v1, v2) * 180.0 / Math.PI);
    }

    public static Vector2 Abs(Vector2 v) => new Vector2(Math.Abs(v.X), Math.Abs(v.Y));

    public static bool SmallerEqualAbs(Vector2 a, Vector2 b)
    {
      Vector2 vector2_1 = MathUtil.Abs(a);
      Vector2 vector2_2 = MathUtil.Abs(b);
      return (double) vector2_1.X <= (double) vector2_2.X && (double) vector2_1.Y < (double) vector2_2.Y;
    }

    public static Vector2 Round(Vector2 v)
    {
      return new Vector2((float) Math.Round((double) v.X), (float) Math.Round((double) v.Y));
    }

    public static float Clamp(float x, float min, float max)
    {
      if ((double) x < (double) min)
        return min;
      return (double) x <= (double) max ? x : max;
    }

    internal static Vector2 CalculateGridCoordintes(Vector2 position)
    {
      return new Vector2((float) (int) Math.Floor((double) position.X / (double) Config.GRID), (float) (int) Math.Floor((double) position.Y / (double) Config.GRID));
    }

    internal static Vector2 CalculateInCellLocation(Vector2 position)
    {
      Vector2 vector2 = position / (float) Config.GRID;
      return new Vector2(vector2.X - (float) Math.Truncate((double) vector2.X), vector2.Y - (float) Math.Truncate((double) vector2.Y));
    }

    public static float LerpRotationDegrees(float from, float to, float alpha)
    {
      if ((double) alpha == 0.0)
        return from;
      if ((double) from == (double) to || (double) alpha == 1.0)
        return to;
      Vector2 vector2 = MathUtil.LerpRorationVectors(new Vector2((float) Math.Cos((double) from), (float) Math.Sin((double) from)), new Vector2((float) Math.Cos((double) to), (float) Math.Sin((double) to)), alpha);
      return (float) Math.Atan2((double) vector2.Y, (double) vector2.X);
    }

    public static Vector2 LerpRorationVectors(Vector2 from, Vector2 to, float alpha)
    {
      if ((double) alpha == 0.0)
        return from;
      if (from == to || (double) alpha == 1.0)
        return to;
      double a = Math.Acos((double) Vector2.Dot(from, to));
      if (a == 0.0)
        return to;
      double num = Math.Sin(a);
      return (float) (Math.Sin((1.0 - (double) alpha) * a) / num) * from + (float) (Math.Sin((double) alpha * a) / num) * to;
    }

    public static Vector2 IsoTo2D(Vector2 pt)
    {
      return new Vector2((float) ((2.0 * (double) pt.Y + (double) pt.X) / 2.0), (float) ((2.0 * (double) pt.Y - (double) pt.X) / 2.0));
    }

    public static Vector2 IsoFrom2D(Vector2 pt)
    {
      return new Vector2(pt.X - pt.Y, (float) (((double) pt.X + (double) pt.Y) / 2.0));
    }
  }
}
