namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class PatrolOnPathState : EnemyKnightState
{
    private int pathPointCount = 0;
    private int currentPathIndex = 0;

    [Export] Timer waitAtPathPointTimer;

    protected override void EnterState()
    {
        // Anzahl der Punkte auf dem Pfad ermitteln.
        pathPointCount = character.Path3DNode.Curve.GetPointCount();

        // Beim Eintritt dieses States haben wir den am nächsten liegenden Punkt auf dem Pfad errechnet.
        // Dieser ist also unser aktueller Standort.
        // character.Path3DNode.Curve.GetClosestPoint gibt uns nur den Vector Punkt zurück. Nicht den Index des Punktes.
        // Daher müssen wir die Punkte durchgehen, um den Index zu ermitteln.
        for (int i = 0; i < pathPointCount; i++)
        {
            Vector3 pathPoint = character.Path3DNode.Curve.GetPointPosition(i);
            Vector3 globalPathPoint = character.Path3DNode.ToGlobal(pathPoint);

            float distanceToPathPoint = character.GlobalPosition.DistanceTo(globalPathPoint);
            if (distanceToPathPoint < 2f)
            {
                // Wir sind nahe genug an diesem Punkt, um ihn als aktuellen Punkt zu betrachten.
                currentPathIndex = i;
                break;
            }
        }

        // Immer, wenn das Ziel erreicht ist, wird ein Timer für das Verfolgen des nächsten Punktes gestartet.
        character.navigationAgent3DNode.NavigationFinished += PauseAndSetWaitAtPathPointTimer;

        // Wenn der Timer abläuft, wird der nächste Pfadpunkt als Ziel gesetzt.
        waitAtPathPointTimer.Timeout += SetNextPathPointAsDestination;

        // Initial den nächsten Pfadpunkt als Ziel setzen.
        SetNextPathPointAsDestination();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 nextNavigationPosition = character.navigationAgent3DNode.GetNextPathPosition();
        Vector3 directionToNextNavigationPosition = character.GlobalPosition.DirectionTo(nextNavigationPosition);

        character.direction = new Vector2(directionToNextNavigationPosition.X, directionToNextNavigationPosition.Z);
        StartMoveAndSlide(3f);
    }

    private void PauseAndSetWaitAtPathPointTimer()
    {
        // Animation auf Idle setzen.
        character.animationPlayer.Play(GameConstants.ANIM_IDLE);

        // Randomisiere die Wartezeit zwischen 0.5 und 2 Sekunden.
        waitAtPathPointTimer.WaitTime = GD.RandRange(0.5f, 2f);

        // Startet den Timer.
        waitAtPathPointTimer.Start();
    }
    
    private void SetNextPathPointAsDestination()
    {
        // Animation auf Move setzen.
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);

        // Erhöht den aktuellen Pfad Index um 1.
        // Wenn der Index das Ende des Pfades überschreitet, wird er auf 0 zurückgesetzt.
        currentPathIndex = Mathf.Wrap(currentPathIndex + 1, 0, pathPointCount);

        // Alternativ zu Mathf.Wrap:
        //. currentPathIndex++;
        //. if (currentPathIndex >= pathPointCount)
        //. {
        //      currentPathIndex = 0;
        //. }

        // Nächsten Pfadpunkt ermitteln.
        Vector3 localPointPosition = character.Path3DNode.Curve.GetPointPosition(currentPathIndex);
        destinationPoint = character.Path3DNode.ToGlobal(localPointPosition);

        // Setzt das Ziel des NavigationAgent3D auf den ermittelten Pfadpunkt.
        character.navigationAgent3DNode.TargetPosition = destinationPoint;
    }
}