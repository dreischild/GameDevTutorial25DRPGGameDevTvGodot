namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using Godot;

public partial class StateMachine : Node
{
    public const int START_ANIMATION_NOTIFICATION = 50001;

    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        /** Custom notification for start animation */
        currentState.Notification(START_ANIMATION_NOTIFICATION);
    }

    public void SwitchCurrentState<T>()
    {
        Node newState = null;
        foreach (Node state in states)
        {
            if (state is T)
            {
                newState = state;
                break;
            }
        }

        if (newState == null)
        {
            GD.PrintErr("StateMachine: SwitchCurrentState: State of type " + typeof(T) + " not found.");
            return;
        }

        currentState = newState;
        currentState.Notification(START_ANIMATION_NOTIFICATION);
    }
}
