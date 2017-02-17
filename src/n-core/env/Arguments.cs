using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace N.Package.Core.Env
{
  /// Parses command line Args
  public class Arguments
  {
    /// The set of loaded Args
    public string[] Args;

    /// Create a new argument parser and read the command line Args
    public Arguments()
    {
      Args = Environment.GetCommandLineArgs();
    }

    /// Check if an argument with the given pattern exists
    /// @param pattern The regex to check for
    public bool Has(string pattern)
    {
      return Get(pattern) != null;
    }

    /// Return an argument value from an argument that matches a given pattern
    /// @param pattern The regex to match
    public string Get(string pattern)
    {
      return Args.FirstOrDefault(s => Regex.IsMatch(s, pattern));
    }

    /// Special helper for getting the value of an argument in the form --foo=bar
    /// @param name The foo part of the --foo=bar
    public string Named(string name)
    {
      var value = Get("--" + name + "=.*");
      return value != null ? value.Split('=')[1] : null;
    }
  }
}