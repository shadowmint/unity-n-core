using N.Tests;

namespace N.Tween {

  /// Types of time step
  public enum TimeStepMode {
    FORWARDS_ONLY,
    FORWARDS_AND_BACK
  }

  /// Step a value between a and b over a duration
  public class TimeStep {

    /// Actual value
    private float value;

    /// Bottom end cap
    private float start;

    /// Top end cap
    private float end;

    /// The duration
    private float duration;

    /// Elapsed time
    private float elapsed;

    /// Tweening mode
    private TimeStepMode mode;

    /// Return the current value
    public float Value {
      get {
        return value;
      }
    }

    /// Create a new value
    /// @param start The start value
    /// @param end The end value
    /// @param duration The duration
    public TimeStep(float start, float end, float duration, TimeStepMode mode = TimeStepMode.FORWARDS_ONLY) {
      this.start = start;
      this.end = end;
      this.duration = duration;
      this.elapsed = 0f;
      this.mode = mode;
      if (duration <= 0f) {
        this.duration = 0.1f;
        elapsed = 0.1f;
      }
      Step(0f);
    }

    /// Update the step by some interval
    public void Step(float interval) {
      elapsed += interval;
      if (elapsed > duration) {
        elapsed = duration;
      }

      // Just normal linear tween
      if (mode == TimeStepMode.FORWARDS_ONLY) {
        value = start + (end - start) * (elapsed / duration);
      }

      // Half way step and back
      else if (mode == TimeStepMode.FORWARDS_AND_BACK) {
        var fraction = 0f;
        if (elapsed > (duration / 2f)) {
          fraction = 1.0f - ((elapsed - duration / 2f) / (duration / 2f));
        }
        else {
          fraction = elapsed / (duration / 2f);
        }
        value = start + (end - start) * fraction;
      }
    }

    /// Is this tween done?
    public bool Complete {
      get {
        return elapsed >= duration;
      }
    }
  }

  public class TimeStepTests : TestSuite {

    public void test_step() {
      var step = new TimeStep(5f, 10f, 10f);
      Assert(step.Value == 5f);

      step.Step(5f);
      Assert(step.Value == 7.5f);

      step.Step(5f);
      Assert(step.Value == 10f);
    }

    public void test_step_and_back() {
      var step = new TimeStep(5f, 10f, 10f, TimeStepMode.FORWARDS_AND_BACK);
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
}
