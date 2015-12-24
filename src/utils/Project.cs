#if UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using N;
using N.Tests;

namespace N {

  /// A variety of utility functions for use in editor scripts
  public class Project {

    /// Find folders
    public static string[] Files(string pattern) {
      return Project.Find(Project.Assets(), pattern, true, false, false);
    }

    /// Find folders
    public static string[] Files(string root, string pattern) {
      return Project.Find(root, pattern, true, false, false);
    }

    /// Find directories
    public static string[] Dirs(string pattern) {
      return Project.Find(Project.Assets(), pattern, false, true, false);
    }

    /// Find directories
    public static string[] Dirs(string root, string pattern) {
      return Project.Find(root, pattern, false, true, false);
    }

    /// Find folders
    public static string[] AllFiles(string pattern) {
      return Project.Find(Project.Assets(), pattern, true, false, true);
    }

    /// Find folders
    public static string[] AllFiles(string root, string pattern) {
      return Project.Find(root, pattern, true, false, true);
    }

    /// Find directories
    public static string[] AllDirs(string pattern) {
      return Project.Find(Project.Assets(), pattern, false, true, true);
    }

    /// Find directories
    public static string[] AllDirs(string root, string pattern) {
      return Project.Find(root, pattern, false, true, true);
    }

    /// Search through files for matches, however, notice although this
    /// may work at runtime, it only works in play mode, not in built versions.
    public static string[] Find(string path, string pattern, bool files, bool dirs, bool recurse) {
      var matcher = new Regex(pattern, RegexOptions.IgnoreCase);
      var list = new List<string>();
      Project.Find(list, path, matcher, files, dirs, recurse);
      return list.ToArray();
    }

    /// Search through files in the Assets folder
    public static void Find(List<string> container, string path, Regex matcher, bool files, bool dirs, bool recurse) {
      var di = new DirectoryInfo(path);
      foreach (var info in di.GetDirectories()) {
        var target = info.ToString();
        if (recurse) {
          Project.Find(container, target, matcher, files, dirs, recurse);
        }
        if (dirs) {
          if (matcher.Matches(target).Count > 0) {
            container.Add(target);
          }
        }
      }
      if (files) {
        foreach (var info in di.GetFiles()) {
          var target = info.ToString();
          if (!target.EndsWith(".meta")) {
            if (matcher.Matches(target).Count > 0) {
              container.Add(target);
            }
          }
        }
      }
    }

    /// Find the assets folder, as an absolute path
    public static string Assets(bool absolutePath = false) {
      if (absolutePath) {
        return Application.dataPath;
      }
      return "Assets";
    }
  }

  /// Tests
  public class ProjectTests : TestSuite {

    public void test_files() {
      var resources = Project.Dirs(".*Resources.*");
      this.Assert(resources != null);
      foreach (var path in resources) {
        Project.Files(path, ".*");
        Project.Files(path, ".*");
      }
    }
  }
}
#endif
