using N.Package.Core.Tests;

namespace N.Package.Core.Tween
{
  /// Types of time StepPerSecond
  public enum TimeStepMode
  {
    ForwardsOnly,
    ForwardsAndBack
  }

  /// Step a value between a and b over a duration
  public class TimeStep
  {
    /// Bottom end cap
    private readonly float _start;

    /// Top end cap
    private readonly float _end;

    /// The duration
    private readonly float _duration;

    /// Elapsed time
    private float _elapsed;

    /// Tweening mode
    private readonly TimeStepMode _mode;

    /// Return the current value
    public float Value { get; private set; }

    /// Create a new value
    /// @param start The start value
    /// @param end The end value
    /// @param duration The duration
    public TimeStep(float start, float end, float duration, TimeStepMode mode = TimeStepMode.ForwardsOnly)
    {
      _start = start;
      _end = end;
      _duration = duration;
      _elapsed = 0f;
      _mode = mode;
      if (duration <= 0f)
      {
        _duration = 0.1f;
        _elapsed = 0.1f;
      }
      Step(0f);
    }

    /// Update the StepPerSecond by some interval
    public void Step(float interval)
    {
      _elapsed += interval;
      if (_elapsed > _duration)
      {
        _elapsed = _duration;
      }

      // Just normal linear tween
      if (_mode == TimeStepMode.ForwardsOnly)
      {
        Value = _start + (_end - _start)*(_elapsed/_duration);
      }

      // Half way StepPerSecond and back
      else if (_mode == TimeStepMode.ForwardsAndBack)
      {
        float fraction;
        if (_elapsed > (_duration/2f))
        {
          fraction = 1.0f - ((_elapsed - _duration/2f)/(_duration/2f));
        }
        else
        {
          fraction = _elapsed/(_duration/2f);
        }
        Value = _start + (_end - _start)*fraction;
      }
    }

    /// Is this tween done?
    public bool Complete
    {
      get { return _elapsed >= _duration; }
    }
  }
}