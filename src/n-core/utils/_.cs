using N.Package.Core;

/// Magical utility class~
public class _
{
  /// Return a new exception
  public static System.Exception Error(string format, params object[] args)
  {
    var msg = string.Format(format, args);
    return new System.Exception(msg);
  }

  /// Log a message
  public static void Log(string format, params object[] args)
  {
    var msg = string.Format(format, args);
    Console.Log(msg);
  }

  /// Log a message
  public static void Log(object target)
  {
    Console.Log(target);
  }
}