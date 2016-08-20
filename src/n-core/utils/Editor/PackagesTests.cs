#if N_CORE_TESTS
using N;
using N.Package.Core;
using N.Package.Core.Tests;
using NUnit.Framework;

public class PackagesTests : TestCase
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
#endif