namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class MoveState : AbstractState
{
    protected override string animationName => GameConstants.ANIM_MOVE;
    private bool isDashingKeyJustPressed = false;

    public override void _PhysicsProcess(double delta)
    {
        if (player.direction == Vector2.Zero)
        {
            // Wechselt in den IdleState
            player.stateMachine.SwitchCurrentState<IdleState>();

            return;
        }
        else if (isDashingKeyJustPressed)
        {
            // Sicherstellen, dass die isDashingKeyJustPressed Variable zurückgesetzt wird.
            // Dies ist erforderlich, da die _Input Methode bei inaktiver State nicht aufgerufen wird.
            isDashingKeyJustPressed = false;

            // Wechselt in den DashState
            player.stateMachine.SwitchCurrentState<DashState>();

            return;
        }

        // Der Vector2 wird in einen Vector3 umgewandelt und der Velocity zugewiesen.
        player.Velocity = new(player.direction.X, 0, player.direction.Y);

        // Die Geschwindigkeit wird skaliert auf Factor 5.
        player.Velocity *= 5;

        // Die Bewegung wird angewendet.
        player.MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        isDashingKeyJustPressed = Input.IsActionJustPressed(GameConstants.ACTION_DASH);
    }
}
