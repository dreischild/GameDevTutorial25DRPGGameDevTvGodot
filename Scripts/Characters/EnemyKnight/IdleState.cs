namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class IdleState : EnemyKnightState
{
    protected override void EnterState()
    {
       character.animationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}