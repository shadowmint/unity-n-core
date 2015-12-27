using N.Tests;

namespace N.Tween
{

    /// Step a value between a and b
    public class Step
    {

        /// Actual value
        private float value;

        /// Top end cap
        public float end;

        /// The step per second
        public float step;

        /// Return the current value
        public float Value
        {
            get
            {
                return value;
            }
        }

        /// Return the next step value
        public float Next
        {
            get
            {
                return Bounded(value) ? Sign() * step : 0f;
            }
        }

        /// Create a new value
        /// @param start The start value
        /// @param end The end value
        /// @param step The step to apply per second
        public Step(float start, float end, float step)
        {
            value = start;
            this.end = end;
            this.step = step;
        }

        /// Check the sign of the step
        protected float Sign()
        {
            return end > value ? 1.0f : -1.0f;
        }

        /// Check if value is within bounds
        protected bool Bounded(float value)
        {
            if (Sign() > 0f)
            {
                return value < end;
            }
            else
            {
                return value > end;
            }
        }

        /// Step to the next tween value.
        /// @param dt The time delta.
        public void Update(float dt)
        {
            var val = value + dt * step * Sign();
            value = Bounded(val) ? val : end;
        }
    }
}
