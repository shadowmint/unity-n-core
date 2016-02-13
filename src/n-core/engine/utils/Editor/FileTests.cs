#if N_CORE_TESTS
using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using N;

public class FileTests : N.Tests.Test
{
    [Test]
    public void test_iterate_files()
    {
        var count = 0;
        foreach (var _ in File.EnumerateFiles("Assets", false, true, true, 10))
        { count += 1; Assert(_ != null); }
        Assert(count == 10);
    }

    [Test]
    public void test_iterate_files_and_dirs()
    {
        var count = 0;
        foreach (var _ in File.EnumerateFiles("Assets", true, true, true, 10))
        { count += 1; Assert(_ != null); }
        Assert(count == 10);
    }
}
#endif
