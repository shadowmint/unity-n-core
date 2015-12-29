using N;
using NUnit.Framework;

public class PackagesTests : N.Tests.Test
{
    [Test]
    public void test_root()
    {
        Packages.Root(true);
        Packages.Root();
    }

    [Test]
    public void test_relative()
    {
        Packages.Relative("package-assets/tests/", true);
        Packages.Relative("package-assets/tests/");
    }
}
