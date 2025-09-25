namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using Godot;

abstract public partial class EnemyKnightState : CharacterState
{
    protected Vector3 destinationPoint;
 
    public override void _Ready()
    {
        base._Ready();

        // Über die GetOwner-Methode wird der Player-Node (Root-Node) abgerufen,
        // da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
        character = GetOwner<EnemyKnight>();
    }
}