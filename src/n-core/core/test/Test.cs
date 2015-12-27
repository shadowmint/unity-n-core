using System.Collections.Generic;
using System;
using N.Tests;

namespace N.Tests {

  /// Basic genric test type
  public class Test {

    /// Assert something is true in a test
    public void Assert(bool value) {
      if (!value) {
        throw new Exception("Test failed");
      }
    }

    /// Assert something is true in a test
    public void Assert(bool value, string msg) {
      if (!value) {
        throw new Exception(string.Format("Test failed: {}", msg));
      }
    }

    /// Assert the current block of code is never reached
    public void Unreachable() {
      throw new Exception("Test failed: Entered unreachable block");
    }

    /// Assert the current block of code is never reached
    public void Unreachable(string msg) {
      throw new Exception(string.Format("Test failed: {}", msg));
    }
  }
}
