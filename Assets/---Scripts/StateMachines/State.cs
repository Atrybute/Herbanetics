using UnityEngine;

/// <summary>
/// Abstract base class for a state in a state machine.
/// Each state has three core lifecycle methods: Enter, Tick, and Exit.
/// </summary>
public abstract class State
{
    /// <summary>
    /// Called when the state machine transitions into this state.
    /// Use this method to setup or initialize state-specific variables and game objects.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Called every frame the state is active.
    /// Use this method for all updates such as checking transitions and updating the behavior of the entity.
    /// </summary>
    /// <param name="deltaTime">The time in seconds it took to complete the last frame.</param>
    public abstract void Tick(float deltaTime);

    /// <summary>
    /// Called when the state machine transitions away from this state.
    /// Use this method to clean up or reset variables and game objects that are no longer needed.
    /// </summary>
    public abstract void Exit();



    /// <summary>
    /// Utility method to retrieve the normalized time of the currently playing animation that matches the specified tag.
    /// This is useful for synchronizing game logic with animations.
    /// </summary>
    /// <param name="animator">The Animator component of the game entity.</param>
    /// <param name="animationTag">The tag assigned to the animation clip in the Animator.</param>
    /// <returns>
    /// The normalized time of the current or next animation state that matches the tag;
    /// returns 0 if no matching animation is found or if the animation is not in transition.
    /// </returns>
    protected float GetNormalizedTime(Animator animator, string animationTag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(animationTag))
        {
            // Return the normalized time of the next animation if it's transitioning and matches the tag
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(animationTag))
        {
            // Return the normalized time of the current animation if it matches the tag
            return currentInfo.normalizedTime;
        }
        else
        {
            // Return 0 if no suitable animation is active or in transition
            return 0;
        }
    }
}

