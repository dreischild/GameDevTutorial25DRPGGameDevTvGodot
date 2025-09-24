namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class ReturnToPathStartState : EnemyKnightState
{
    protected override void EnterState()
    {
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Ermittelt den nächsten Punkt auf dem Pfad vom Character aus gesehen.
        // character.GlobalPosition muss in den lokalen Raum des Path3DNode umgerechnet werden,
        // da die GetClosestPoint Methode einen lokalen Punkt erwartet.
        Vector3 localPointPosition = character.Path3DNode.Curve.GetClosestPoint(character.Path3DNode.ToLocal(character.GlobalPosition));
        Vector3 globalPointPosition = localPointPosition + character.Path3DNode.GlobalPosition;

        float distanceToPoint = character.GlobalPosition.DistanceTo(globalPointPosition);
        Vector3 directionToPoint = character.GlobalPosition.DirectionTo(globalPointPosition);

        if (distanceToPoint < 0.2f)
        {
            // Wechselt in den IdleState
            character.stateMachine.SwitchCurrentState<IdleState>();
            return;
        }

        character.direction = new Vector2(directionToPoint.X, directionToPoint.Z);
        StartMoveAndSlide(5f);
    }
}