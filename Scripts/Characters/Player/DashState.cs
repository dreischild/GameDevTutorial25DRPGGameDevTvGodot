namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class DashState : Node
{
    private Player player;
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 10f;

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
        if (dashTimerNode.IsStopped() || player.direction == Vector2.Zero)
        {
            // Wechselt in den IdleState
            player.stateMachine.SwitchCurrentState<IdleState>();
            return;
        }

        // Der Vector2 wird in einen Vector3 umgewandelt und der Velocity zugewiesen.
        player.Velocity = new(player.direction.X, 0, player.direction.Y);

        // Die Geschwindigkeit wird skaliert auf Factor 5.
        player.Velocity *= speed;

        // Die Bewegung wird angewendet.
        player.MoveAndSlide();
    }
    
    // Die Notification-Methode wird aufgerufen, wenn eine Notification an diesen Node geschickt wird.
    public override void _Notification(int what)
    {
        // Es muss sichergestellt werden, dass die base Notification-Methode aufgerufen wird.
        base._Notification(what);

        if (what == StateMachine.START_ANIMATION_NOTIFICATION)
        {
            dashTimerNode.Start();
            player.animationPlayer.Play(GameConstants.ANIM_DASH);
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
