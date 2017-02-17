using System;

namespace N.Package.Core.Tests
{
  /// Basic genric test type
  public class TestCase
  {
    /// Assert something is true in a test
    public void Assert(bool value)
    {
      if (!value)
      {
        throw new Exception("TestCase failed");
      }
    }

    /// Assert something is true in a test
    public void Assert(bool value, string msg)
    {
      if (!value)
      {
        throw new Exception(string.Format("TestCase failed: {}", msg));
      }
    }

    /// Assert the current block of code is never reached
    public void Unreachable()
    {
      throw new Exception("TestCase failed: Entered unreachable block");
    }

    /// Assert the current block of code is never reached
    public void Unreachable(string msg)
    {
      throw new Exception(string.Format("TestCase failed: {}", msg));
    }
  }
}