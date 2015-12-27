using N;
using NUnit.Framework;

public class TestExtensionsTests : N.Tests.Test
{
    [Test]
    public void test_screen_coordinates_by_position()
    {
        var obj = this.SpawnBlank();
        Assert(obj != null);
        this.TearDown();
    }
}
