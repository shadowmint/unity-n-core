using UnityEngine;

namespace N.Package.Core.Tween
{
  /// <summary>
  /// This provides a convenient way of doing a reversible lerp in either direction
  /// Provides information on when the movement has stopped, and allows the movement to be reversed
  /// </summary>
  public class StateTransitioner
  {
    /// <summary>
    /// How long should interpolation between the two values take
    /// </summary>
    public float transitionTime = 5.0f;

    /// <summary>
    /// the direction we can travel in
    /// </summary>
    public enum Direction
    {
      forward,
      backward,
      stopped
    }

    /// <summary>
    /// What our current interpolated value is
    /// </summary>
    public float currentValue;

    /// <summary>
    /// What direction are we travelling in
    /// </summary>
    public Direction CurrentDirection
    {
      get { return currentDirection; }
      set { handleDirectionChange(value); }
    }

    private Direction currentDirection = Direction.stopped;
    private float normalizedTime;
    private float transitionEnd = -1.0f;

    /// <summary>
    /// Update the value as time has passed.
    /// </summary>
    /// <returns>the new current value</returns>
    public float updateValue()
    {
      if (currentDirection == Direction.stopped)
      {
        transitionEnd = -1.0f;
        return currentValue;
      }

      normalizedTime = (transitionEnd - Time.time)/transitionTime;

      if (currentDirection == Direction.forward)
      {
        currentValue = Mathf.Lerp(1.0f, 0.0f, normalizedTime);
        if (currentValue > 0.99f)
        {
          currentDirection = Direction.stopped;
        }
      }
      else
      {
        currentValue = Mathf.Lerp(0.0f, 1.0f, normalizedTime);
        if (currentValue < 0.01f)
        {
          currentDirection = Direction.stopped;
        }
      }

      if (currentDirection == Direction.stopped)
      {
        transitionEnd = -1.0f;
      }
      return currentValue;
    }

    /// <summary>
    /// Change the direction we are moving in.
    /// </summary>
    /// <param name="newDirection">the new direction to move in</param>
    [System.Obsolete("Use the Direction Property instead")]
    public void transitionDirectionTo(Direction newDirection)
    {
      handleDirectionChange(newDirection);
    }

    private void handleDirectionChange(Direction newDirection)
    {
      if (currentDirection != newDirection)
      {
        if (transitionEnd < 0.0f)
        {
          transitionEnd = Time.time + transitionTime;
        }
        else
        {
          normalizedTime = 1.0f - (transitionEnd - Time.time)/transitionTime;
          transitionEnd = Time.time + transitionTime*normalizedTime;
        }
        currentDirection = newDirection;
      }
    }

    /// <summary>
    /// Reset the value and stop the direction of movement
    /// </summary>
    public void Reset()
    {
      currentValue = 0.0f;
      currentDirection = Direction.stopped;
    }
  }
}