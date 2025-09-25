namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class ReturnToPathStartState : EnemyKnightState
{
    private Vector3 destinationPoint;

    protected override void EnterState()
    {
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);

        // Ermittelt den nächsten Punkt auf dem Pfad vom Character aus gesehen.
        // character.GlobalPosition muss in den lokalen Raum des Path3DNode umgerechnet werden,
        // da die GetClosestPoint Methode einen lokalen Punkt erwartet.
        Vector3 localPointPosition = character.Path3DNode.Curve.GetClosestPoint(character.Path3DNode.ToLocal(character.GlobalPosition));

        // Pfad Punkt in den globalen Raum umrechnen und als Zielpunkt speichern.
        destinationPoint = character.Path3DNode.ToGlobal(localPointPosition);
        // Alternative: destinationPoint = localPointPosition + character.Path3DNode.GlobalPosition;

        // Setzt das Ziel des NavigationAgent3D auf den ermittelten Pfadpunkt.
        character.navigationAgent3DNode.TargetPosition = destinationPoint;


        GD.Print("destinationPoint" + destinationPoint);
    }

    public override void _PhysicsProcess(double delta)
    {

        Vector3 nextNavigationPosition = character.navigationAgent3DNode.GetNextPathPosition();
        Vector3 directionToNextNavigationPosition = character.GlobalPosition.DirectionTo(nextNavigationPosition);

        if (character.navigationAgent3DNode.IsNavigationFinished())
        {
            // Wechselt in den IdleState
            character.stateMachine.SwitchCurrentState<IdleState>();
            return;
        }

        character.direction = new Vector2(directionToNextNavigationPosition.X, directionToNextNavigationPosition.Z);
        StartMoveAndSlide(5f);
    }
}