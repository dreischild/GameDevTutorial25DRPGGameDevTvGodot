namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

public partial class MoveState : Node
{
    public override void _Ready()
    {
        /*
         * Über die GetOwner-Methode wird der Player-Node abgerufen,
         * da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
         */
        Player player = GetOwner<Player>();

        GD.Print("MoveState _Ready");

        // Initialisierung der Animation.
        player.animationPlayer.Play(GameConstants.ANIM_MOVE);
    }
}
