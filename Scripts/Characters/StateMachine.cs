namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;
using Godot;

public partial class StateMachine : Node
{
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
        // State aktivieren und Animation des States starten
        currentState.Notification(AbstractState.ACTIVATE_STATE__NOTIFICATION);
    }

    private void StopCurrentState()
    {
        // State deaktivieren
        currentState.Notification(AbstractState.DEACTIVATE_STATE__NOTIFICATION);
    }
}
