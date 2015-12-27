using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;

/// Tests
public class ConsoleTests : N.Tests.Test
{
    [Test]
    public void test_log()
    {
        N.Console.DEBUG = true;
        N.Console.Log("Console log string");
        N.Console.Log(this);
        N.Console.Log(new int[3] { 1, 2, 3 });
        var x = new List<int>();
        x.Add(1);
        x.Add(2);
        x.Add(3);
        N.Console.Log(x);
        N.Console.DEBUG = false;
    }
}
