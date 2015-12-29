#if N_CORE_TESTS
using N;
using NUnit.Framework;

public class ProjectTests : N.Tests.Test
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
