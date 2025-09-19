namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using Godot;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        /** Custom notification for start animation */
        currentState.Notification(50001);
    }
}
