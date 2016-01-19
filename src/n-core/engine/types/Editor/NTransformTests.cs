#if N_CORE_TESTS
using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using N;

/// Tests
public class NTransformTests : N.Tests.Test
{
    [Test]
    public void test_create_transform()
    {
        var blank = this.SpawnBlank();
        new NTransform(blank);
        new NTransform(blank.transform);
        this.TearDown();
    }

    [Test]
    public void test_creates_copy()
    {
        var blank = this.SpawnBlank();
        blank.Move(new Vector3(1f, 1f, 1f));
        var transform = new NTransform(blank);
        blank.Move(new Vector3(2f, 2f, 2f));
        Assert(transform.position == new Vector3(1f, 1f, 1f));
        this.TearDown();
    }
}
#endif
