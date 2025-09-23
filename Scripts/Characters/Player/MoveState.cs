namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class MoveState : Node
{
    private Player player;
    private bool isDashingKeyJustPressed = false;
    
    public override void _Ready()
    {
        // Über die GetOwner-Methode wird der Player-Node (Root-Node) abgerufen,
        // da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
        player = GetOwner<Player>();

        // State standardmäßig deaktivieren.
        Notification(StateMachine.DEACTIVATE_STATE_NOTIFICATION);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player.direction == Vector2.Zero)
        {
            // Wechselt in den IdleState
            player.stateMachine.SwitchCurrentState<IdleState>();
        }
        else if (isDashingKeyJustPressed)
        {
            // Sicherstellen, dass die isDashingKeyJustPressed Variable zurückgesetzt wird.
            // Dies ist erforderlich, da die _Input Methode bei inaktiver State nicht aufgerufen wird.
            isDashingKeyJustPressed = false;

            // Wechselt in den DashState
            player.stateMachine.SwitchCurrentState<DashState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        isDashingKeyJustPressed = Input.IsActionJustPressed(GameConstants.ACTION_DASH);
    }

    // Die Notification-Methode wird aufgerufen, wenn eine Notification an diesen Node geschickt wird.
    public override void _Notification(int what)
    {
        // Es muss sichergestellt werden, dass die base Notification-Methode aufgerufen wird.
        base._Notification(what);

        if (what == StateMachine.START_ANIMATION_NOTIFICATION)
        {
            player.animationPlayer.Play(GameConstants.ANIM_MOVE);
        }
        else if (what == StateMachine.ACTIVATE_STATE_NOTIFICATION)
        {
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == StateMachine.DEACTIVATE_STATE_NOTIFICATION)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }
}
