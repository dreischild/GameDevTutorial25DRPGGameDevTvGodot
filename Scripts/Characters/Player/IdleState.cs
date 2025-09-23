namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class IdleState : AbstractState
{
    protected override string animationName => GameConstants.ANIM_IDLE;

    public override void _PhysicsProcess(double delta)
    {
        if (player.direction != Vector2.Zero)
        {
            // Wechselt in den MoveState
            player.stateMachine.SwitchCurrentState<MoveState>();
        }
    }
}
