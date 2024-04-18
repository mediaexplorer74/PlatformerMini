// Decompiled with JetBrains decompiler
// Type: MonolithEngine.ExtensionMethods
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MonolithEngine
{
  public static class ExtensionMethods
  {
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
      T[] destinationArray = new T[length];
      Array.Copy((Array) array, offset, (Array) destinationArray, 0, length);
      return destinationArray;
    }

    public static void AddIfMissing<T>(this HashSet<T> set, T newElement)
    {
      if (set.Contains(newElement))
        return;
      set.Add(newElement);
    }

    public static void RemoveIfExists<T>(this HashSet<T> set, T newElement)
    {
      if (!set.Contains(newElement))
        return;
      set.Remove(newElement);
    }

    public static void RemoveIfExists<T, V>(this Dictionary<T, V> map, T toRemove)
    {
      if (!map.ContainsKey(toRemove))
        return;
      map.Remove(toRemove);
    }

    public static V GetOrDefault<T, V>(this Dictionary<T, V> map, T key, V defaultValue)
    {
      return map.ContainsKey(key) ? map[key] : defaultValue;
    }

    public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof (T)).Cast<T>();

    public static bool IsMobile(this Platform platform)
    {
            return true;//platform == Platform.ANDROID || platform == Platform.IOS;
    }

        /*
    public static bool IsDesktop(this Platform platform)
    {
      return platform == Platform.MAC_OSX || platform == Platform.WINDOWS || platform == Platform.LINUX;
    }
        */
  }
}
