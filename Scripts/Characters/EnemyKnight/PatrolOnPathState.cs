namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class PatrolOnPathState : EnemyKnightState
{
    private int pathPointCount = 0;
    private int currentPathIndex = 0;

    private bool playerInChaseArea = false;

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

        // Event für das Betreten des Chase-Bereichs abonnieren.
        character.chaseArea3DNode.BodyEntered += OnBodyEnteredChaseArea;

        // Event für das Verlassen des Chase-Bereichs abonnieren.
        character.chaseArea3DNode.BodyExited += OnBodyExitedChaseArea;

        // Initial den nächsten Pfadpunkt als Ziel setzen.
        SetNextPathPointAsDestination();
    }

    protected override void LeaveState()
    {
        // Events abmelden, um Memory Leaks zu vermeiden.
        character.navigationAgent3DNode.NavigationFinished -= PauseAndSetWaitAtPathPointTimer;
        waitAtPathPointTimer.Timeout -= SetNextPathPointAsDestination;
        character.chaseArea3DNode.BodyEntered -= OnBodyEnteredChaseArea;
        character.chaseArea3DNode.BodyExited -= OnBodyExitedChaseArea;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerInChaseArea && IsPlayerInSight())
        {
            // Wechsle in den Chase State
            character.stateMachine.SwitchCurrentState<ChaseState>();
            return;
        }

        GoToNavigationTarget(4f);
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

    private void OnBodyEnteredChaseArea(Node3D body)
    {
        // Nur relevant, wenn es sich um den Spieler handelt
        if (!(body is Player.Player))
        {
            return;
        }

        // Flag setzen, dass der Spieler im Chase-Bereich ist
        playerInChaseArea = true;
    }

    private void OnBodyExitedChaseArea(Node3D body)
    {
        // Nur relevant, wenn es sich um den Spieler handelt
        if (!(body is Player.Player))
        {
            return;
        }

        // Flag setzen, dass der Spieler nicht mehr im Chase-Bereich ist
        playerInChaseArea = false;
    }

    private bool IsPlayerInSight()
    {
        if (character.chaseArea3DNode.GetOverlappingBodies().Count == 0)
        {
            return false;
        }

        // Prüfe, ob ein überlappender Körper der Spieler ist
        foreach (var body in character.chaseArea3DNode.GetOverlappingBodies())
        {
            if (!(body is Player.Player player))
            {
                continue;
            }

            character.targetCharacter = player;
            // Prüfe, ob der Spieler gesehen werden kann
            if (character.CanSeeObject(player))
            {
                return true;
            }
        }

        return false;
    }
}