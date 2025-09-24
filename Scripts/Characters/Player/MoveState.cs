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

        StartMoveAndSlide(7f);
    }

    public override void _Input(InputEvent @event)
    {
        isDashingKeyJustPressed = Input.IsActionJustPressed(GameConstants.ACTION_DASH);
    }
}
