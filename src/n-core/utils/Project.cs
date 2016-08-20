#if UNITY_EDITOR
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Core
{
  /// A variety of utility functions for use in editor scripts
  public class Project
  {
    /// If you need a higher limit, set this
    public static int MaxOutputCount = 5000;

    /// Find folders
    public static string[] Files(string pattern)
    {
      return Find(Assets(), pattern, true, false, false);
    }

    /// Find folders
    public static string[] Files(string root, string pattern)
    {
      return Find(root, pattern, true, false, false);
    }

    /// Find directories
    public static string[] Dirs(string pattern)
    {
      return Find(Assets(), pattern, false, true, false);
    }

    /// Find directories
    public static string[] Dirs(string root, string pattern)
    {
      return Find(root, pattern, false, true, false);
    }

    /// Find folders
    public static string[] AllFiles(string pattern)
    {
      return Find(Assets(), pattern, true, false, true);
    }

    /// Find folders
    public static string[] AllFiles(string root, string pattern)
    {
      return Find(root, pattern, true, false, true);
    }

    /// Find directories
    public static string[] AllDirs(string pattern)
    {
      return Find(Assets(), pattern, false, true, true);
    }

    /// Find directories
    public static string[] AllDirs(string root, string pattern)
    {
      return Find(root, pattern, false, true, true);
    }

    /// Search through files for matches, however, notice although this
    /// may work at runtime, it only works in play mode, not in built versions.
    public static string[] Find(string path, string pattern, bool files, bool dirs, bool recurse)
    {
      var matcher = new Regex(pattern, RegexOptions.IgnoreCase);
      var list = new List<string>();
      Find(list, path, matcher, files, dirs, recurse);
      return list.ToArray();
    }

    /// Search through files in the Assets folder
    public static void Find(List<string> container, string path, Regex matcher, bool files, bool dirs, bool recurse)
    {
      foreach (var target in File.EnumerateFiles(path, files, dirs, recurse, MaxOutputCount))
      {
        if (!target.EndsWith(".meta"))
        {
          if (matcher.Matches(target).Count > 0)
          {
            container.Add(target);
          }
        }
      }
    }

    /// Find the assets folder, as an absolute path
    public static string Assets(bool absolutePath = false)
    {
      if (absolutePath)
      {
        return Application.dataPath;
      }
      return "Assets";
    }
  }
}

#endif