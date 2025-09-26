namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class ChaseState : EnemyKnightState
{
    protected override void EnterState()
    {
        // Sicherstellen, dass die Move Animation abgespielt wird.
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);        
    }

    public override void _PhysicsProcess(double delta)
    {
        // Abbruch, wenn der Spieler nicht mehr verfügbar oderim Sichtbereich ist.
        if (character.targetCharacter == null || !character.CanSeeObject(character.targetCharacter))
        {
            // Zurück zum Patrol State, wenn der Spieler nicht mehr verfügbar ist.
            character.stateMachine.SwitchCurrentState<ReturnToPathStartState>();
            return;
        }

        // Ziel Position auf Position des Spielers setzen.
        character.navigationAgent3DNode.TargetPosition = character.targetCharacter.GlobalPosition;

        // Zur Ziel Position bewegen.
        GoToNavigationTarget(6f);
    }
}