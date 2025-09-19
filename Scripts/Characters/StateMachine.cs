namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using Godot;

public partial class StateMachine : Node
{
    // Notification zum Start der Animation
    public const int START_ANIMATION_NOTIFICATION = 50001;
    // Notification aktivieren des States (z.B. Start der PhysicsProcess Methode)
    public const int ACTIVATE_STATE_NOTIFICATION = 50002;
    // Notification deaktivieren des States (z.B. Stop der PhysicsProcess Methode)
    public const int DEACTIVATE_STATE_NOTIFICATION = 50003;

    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {    
        StartCurrentState();
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

        // PhysicsProcess des alten States stoppen
        StopCurrentState();

        currentState = newState;
        StartCurrentState();
    }

    private void StartCurrentState()
    {
        // State aktivieren
        currentState.Notification(ACTIVATE_STATE_NOTIFICATION);

        // Animation des States starten
        currentState.Notification(START_ANIMATION_NOTIFICATION);
    }

    private void StopCurrentState()
    {
        // State deaktivieren
        currentState.Notification(DEACTIVATE_STATE_NOTIFICATION);
    }
}
