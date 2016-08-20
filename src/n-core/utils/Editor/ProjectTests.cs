#if N_CORE_TESTS
using N;
using N.Package.Core;
using N.Package.Core.Tests;
using NUnit.Framework;

public class ProjectTests : TestCase
{
  [Test]
  public void test_files()
  {
    var resources = Project.Dirs(".*Resources.*");
    this.Assert(resources != null);
    foreach (var path in resources)
    {
      Project.Files(path, ".*");
      Project.Files(path, ".*");
    }
  }
}
#endif