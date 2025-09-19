namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;
using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class IdleState : Node
{
    public override void _Ready()
    {
                
    }
    
    // Die Notification-Methode wird aufgerufen, wenn eine Notification an diesen Node geschickt wird.
    public override void _Notification(int what)
    {
        // Es muss sichergestellt werden, dass die base Notification-Methode aufgerufen wird.
        base._Notification(what);

        if (what == StateMachine.START_ANIMATION_NOTIFICATION)
        {
            // Über die GetOwner-Methode wird der Player-Node (Root-Node) abgerufen,
            // da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
            Player player = GetOwner<Player>();
            player.animationPlayer.Play(GameConstants.ANIM_IDLE);
        }
    }
}
