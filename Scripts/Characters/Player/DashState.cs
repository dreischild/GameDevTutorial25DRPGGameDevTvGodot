namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class DashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    // https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_exports.html
    [Export(PropertyHint.Range, "1, 20, 0.1")] private float speed = 10f;

    protected override string animationName => GameConstants.ANIM_DASH;

    protected override void EnterState()
    {
        dashTimerNode.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (dashTimerNode.IsStopped() || character.direction == Vector2.Zero)
        {
            // Wechselt in den IdleState
            character.stateMachine.SwitchCurrentState<IdleState>();
            return;
        }

        StartMoveAndSlide(speed);
    }
}
