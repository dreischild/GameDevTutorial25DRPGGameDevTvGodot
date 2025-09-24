namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class IdleState : PlayerState
{
    protected override void EnterState()
    {
       character.animationPlayer.Play(GameConstants.ANIM_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.direction != Vector2.Zero)
        {
            // Wechselt in den MoveState
            character.stateMachine.SwitchCurrentState<MoveState>();
            return;
        }

        // Aufgrund der Gravity muss der Character auch im IdleState bewegt werden.
        // Ansonsten würde der Character in der Luft schweben bleiben.
        StartMoveAndSlide(0f);
    }
}
