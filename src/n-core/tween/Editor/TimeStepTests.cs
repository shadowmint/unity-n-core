#if N_CORE_TESTS
using N.Package.Core.Tests;
using N.Package.Core.Tween;
using NUnit.Framework;

public class TimeStepTests : TestCase
{
  [Test]
  public void test_step()
  {
    var step = new TimeStep(5f, 10f, 10f);
    Assert(step.Value == 5f);

    step.Step(5f);
    Assert(step.Value == 7.5f);

    step.Step(5f);
    Assert(step.Value == 10f);
  }

  [Test]
  public void test_step_and_back()
  {
    var step = new TimeStep(5f, 10f, 10f, TimeStepMode.ForwardsAndBack);
    Assert(step.Value == 5f);

    step.Step(2.5f);
    Assert(step.Value == 7.5f);

    step.Step(2.5f);
    Assert(step.Value == 10f);

    step.Step(2.5f);
    Assert(step.Value == 7.5f);

    step.Step(2.5f);
    Assert(step.Value == 5f);
  }
}
#endif