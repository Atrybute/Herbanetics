using UnityEngine;

/// <summary>
/// Abstract base class for all state machines. Manages states and state transitions for an entity.
/// </summary>
public abstract class StateMachine : MonoBehaviour
{
    /// <summary>
    /// The current active state of the state machine.
    /// </summary>
    public State currentState;

    /// <summary>
    /// Switches the state machine to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void SwitchState(State newState)
    {
        currentState?.Exit(); // Call Exit on the current state if it exists
        currentState = newState; // Update the current state to the new state
        currentState?.Enter(); // Call Enter on the new current state if it exists
    }

    /// <summary>
    /// Called once per frame. Updates the current state.
    /// </summary>
    protected virtual void Update()
    {
        currentState?.Tick(Time.deltaTime); // Call Tick on the current state with the time since the last frame
    }
}

