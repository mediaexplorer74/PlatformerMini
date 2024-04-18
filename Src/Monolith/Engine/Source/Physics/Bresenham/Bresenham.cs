// Decompiled with JetBrains decompiler
// Type: MonolithEngine.Bresenham
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace MonolithEngine
{
  public class Bresenham
  {
    public static void GetLine(Vector2 a, Vector2 b, List<Vector2> result)
    {
      int num1 = (int) a.X;
      int num2 = (int) a.Y;
      int num3 = (int) b.X;
      int num4 = (int) b.Y;
      if (a == b)
        return;
      int num5 = Bresenham.FastAbs(num4 - num2) > Bresenham.FastAbs(num3 - num1) ? 1 : 0;
      if (num5 != 0)
      {
        int num6 = num1;
        num1 = num2;
        num2 = num6;
        int num7 = num3;
        num3 = num4;
        num4 = num7;
      }
      if (num1 > num3)
      {
        int num8 = num1;
        num1 = num3;
        num3 = num8;
        int num9 = num2;
        num2 = num4;
        num4 = num9;
      }
      int num10 = num3 - num1;
      int num11 = Bresenham.FastAbs(num4 - num2);
      int num12 = num10 / 2;
      int num13 = num2;
      int num14 = num2 < num4 ? 1 : -1;
      if (num5 != 0)
      {
        for (int y = num1; y <= num3; ++y)
        {
          result.Add(new Vector2((float) num13, (float) y));
          num12 -= num11;
          if (num12 < 0)
          {
            num13 += num14;
            num12 += num10;
          }
        }
      }
      else
      {
        for (int x = num1; x <= num3; ++x)
        {
          result.Add(new Vector2((float) x, (float) num13));
          num12 -= num11;
          if (num12 < 0)
          {
            num13 += num14;
            num12 += num10;
          }
        }
      }
    }

    public static bool CanLinePass(Vector2 a, Vector2 b, Func<int, int, bool> isRayBlocked)
    {
      int num1 = (int) a.X;
      int num2 = (int) a.Y;
      int num3 = (int) b.X;
      int num4 = (int) b.Y;
      if (a == b)
        return false;
      int num5 = Bresenham.FastAbs(num4 - num2) > Bresenham.FastAbs(num3 - num1) ? 1 : 0;
      if (num5 != 0)
      {
        int num6 = num1;
        num1 = num2;
        num2 = num6;
        int num7 = num3;
        num3 = num4;
        num4 = num7;
      }
      if (num1 > num3)
      {
        int num8 = num1;
        num1 = num3;
        num3 = num8;
        int num9 = num2;
        num2 = num4;
        num4 = num9;
      }
      int num10 = num3 - num1;
      int num11 = Bresenham.FastAbs(num4 - num2);
      int num12 = num10 / 2;
      int num13 = num2;
      int num14 = num2 < num4 ? 1 : -1;
      if (num5 != 0)
      {
        for (int index = num1; index <= num3; ++index)
        {
          if (isRayBlocked(num13, index))
            return false;
          num12 -= num11;
          if (num12 < 0)
          {
            num13 += num14;
            num12 += num10;
          }
        }
      }
      else
      {
        for (int index = num1; index <= num3; ++index)
        {
          if (isRayBlocked(index, num13))
            return false;
          num12 -= num11;
          if (num12 < 0)
          {
            num13 += num14;
            num12 += num10;
          }
        }
      }
      return true;
    }

    private static int FastAbs(int v) => (v ^ v >> 31) - (v >> 31);
  }
}
