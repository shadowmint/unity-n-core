#if UNITY_EDITOR
using System.IO;

namespace N.Package.Core
{
  /// Project helper functions, mainly for in-editor scripts
  public class Packages
  {
    /// Find the root folder of the packages repository
    public static Option<string> Root(bool absolutePath = false)
    {
      var assets = Project.Assets(true);
      foreach (var filename in File.EnumerateFiles(assets))
      {
        if (filename.EndsWith(".nmarker"))
        {
          var path = filename.Substring(0, filename.Length - ".nmarker".Length - 1);
          if (absolutePath)
          {
            return Option.Some(path);
          }
          return Option.Some(ToRelative(path));
        }
      }
      return Option.None<string>();
    }

    /// Return a path relative to the packages root
    public static Option<string> Relative(string path, bool absolutePath = false)
    {
      var root = Packages.Root(absolutePath);
      if (root)
      {
        return Option.Some(OsStr(Path.Combine(root.Unwrap(), path)));
      }
      return Option.None<string>();
    }

    /// Convert an absolute path into a relative path
    static string ToRelative(string value)
    {
      var offset = value.IndexOf("Assets");
      return OsStr(value.Substring(offset));
    }

    public static string OsStr(string value)
    {
      return value.Replace("\\", "/");
    }
  }
}

#endif