using N.Tween;
using NUnit.Framework;

public class StepTests : N.Tests.Test
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
        step.end = -10f;
        step.Update(1f);
        step.Update(1f);
        step.Update(1f);
        step.Update(1f);
        step.Update(1f);
        Assert(step.Value == 0f);
    }
}
