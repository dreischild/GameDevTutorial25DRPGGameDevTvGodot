namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class DashState : AbstractState
{
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 10f;

    protected override string animationName => GameConstants.ANIM_DASH;

    protected override void ActivateState()
    {
        base.ActivateState();

        dashTimerNode.Start();
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
}
