#if N_CORE_TESTS
using N.Package.Core.Tests;
using N.Package.Core.Tween;
using NUnit.Framework;

public class StepTests : TestCase
{
  [Test]
  public void test_step()
  {
    var step = new Step(0f, 10f, 1.0f);
    Assert(step.Value == 0f);

    step.Update(5.0f);
    Assert(step.Value == 5f);
    Assert(step.Next == 1f);

    step.Update(10.0f);
    Assert(step.Value == 10f);
    Assert(step.Next == 0f);
  }

  [Test]
  public void test_step_down()
  {
    var step = new Step(0f, -10f, 1.0f);
    Assert(step.Value == 0f);
    Assert(step.Next == -1f);

    step.Update(5.0f);
    Assert(step.Value == -5f);
    Assert(step.Next == -1f);

    step.Update(10.0f);
    Assert(step.Value == -10f);
    Assert(step.Next == 0f);
  }

  [Test]
  public void test_step_new_target()
  {
    var step = new Step(0f, 10f, 1.0f);
    step.Update(5.0f);
    step.End = -10f;
    step.Update(1f);
    step.Update(1f);
    step.Update(1f);
    step.Update(1f);
    step.Update(1f);
    Assert(step.Value == 0f);
  }
}
#endif