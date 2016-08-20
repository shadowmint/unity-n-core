#if N_CORE_TESTS
using System.Collections.Generic;
using N.Package.Core;
using N.Package.Core.Tests;
using NUnit.Framework;

public class ConsoleTests : TestCase
{
  [Test]
  public void test_log()
  {
    Console.Debug();
    Console.Log("Console log string");
    Console.Log(this);
    Console.Log(new int[3] {1, 2, 3});
    var x = new List<int>();
    x.Add(1);
    x.Add(2);
    x.Add(3);
    Console.Log(x);
    Console.Quiet();
  }
}
#endif