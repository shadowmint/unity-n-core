using System;

namespace N.Package.Core
{
  public class Console
  {
    /// Debugging mode for tests
    private static bool _debug;

    // Turn debug mode on
    public static void Debug()
    {
      _debug = true;
    }

    // Stop debugging
    public static void Quiet()
    {
      _debug = false;
    }

    // Debug message even if debug mode is off
    public static void Debug(string format, params object[] args)
    {
      _debug = true;
      Log(format, args);
    }

    /// Clear the Console
    public static void Clear()
    {
      var logEntries = Reflect.Type.Resolve("UnityEditorInternal.LogEntries");
      if (logEntries)
      {
        var clearMethod = logEntries.Unwrap().GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
      }
    }

    public static void Debug(object msg)
    {
      Debug("{0}", msg);
    }

    public static void Log(string format, params object[] args)
    {
      var msg = string.Format(format, args);
      Log(msg);
    }

    public static void Log(object msg)
    {
      Log("{0}", msg);
    }

    public static void Log(string msg)
    {
      if (_debug)
      {
        UnityEngine.Debug.Log(" ------- DEBUG ------- " + msg);
      }
      else
      {
        UnityEngine.Debug.Log(msg);
      }
    }

    public static void Error(string msg)
    {
      UnityEngine.Debug.Log(" ------- ERROR ------- " + msg);
    }

    public static void Error(Exception msg)
    {
      Error(msg.ToString());
      if (msg.StackTrace != null)
      {
        foreach (var line in msg.StackTrace.Split('\n'))
        {
          Error(line);
        }
      }
      UnityEngine.Debug.LogException(msg);
    }

    public static void Error(string format, params object[] args)
    {
      var msg = string.Format(format, args);
      Error(msg);
    }
  }
}