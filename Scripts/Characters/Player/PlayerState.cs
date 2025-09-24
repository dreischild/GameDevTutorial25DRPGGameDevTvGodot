namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

abstract public partial class PlayerState : CharacterState
{

    abstract protected string animationName { get; }

    public override void _Ready()
    {
        base._Ready();

        // Über die GetOwner-Methode wird der Player-Node (Root-Node) abgerufen,
        // da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
        character = GetOwner<Player>();
    }

    protected override void ActivateState()
    {
        base.ActivateState();

        character.animationPlayer.Play(animationName);
    }
}
