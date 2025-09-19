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
}
