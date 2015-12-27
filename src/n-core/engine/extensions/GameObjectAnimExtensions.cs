using UnityEngine;
using UnityEngine.UI;
using N;

namespace N
{
    /// Animation extension methods
    public static class GameObjectAnimExtensions
    {
        /// Trigger an animation
        public static void AnimationTrigger(this GameObject target, string trigger)
        {
            var animator = target.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(trigger);
            }
        }

        /// Set an animation state
        public static void AnimationState(this GameObject target, string trigger, bool value)
        {
            var animator = target.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetBool(trigger, value);
            }
        }
    }
}
