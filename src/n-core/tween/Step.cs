namespace N.Package.Core.Tween
{
  /// Step a value between a and b
  public class Step
  {
    /// Top end cap
    public float End;

    /// The StepPerSecond per second
    public float StepPerSecond;

    /// Return the current value
    public float Value { get; private set; }

    /// Return the next StepPerSecond value
    public float Next
    {
      get { return Bounded(Value) ? Sign()*StepPerSecond : 0f; }
    }

    /// Create a new value
    /// @param start The start value
    /// @param end The end value
    /// @param StepPerSecond The StepPerSecond to apply per second
    public Step(float start, float end, float stepPerSecond)
    {
      Value = start;
      End = end;
      StepPerSecond = stepPerSecond;
    }

    /// Check the sign of the StepPerSecond
    protected float Sign()
    {
      return End > Value ? 1.0f : -1.0f;
    }

    /// Check if value is within bounds
    protected bool Bounded(float value)
    {
      if (Sign() > 0f)
      {
        return value < End;
      }
      return value > End;
    }

    /// Step to the next tween value.
    /// @param dt The time delta.
    public void Update(float dt)
    {
      var val = Value + dt*StepPerSecond*Sign();
      Value = Bounded(val) ? val : End;
    }
  }
}