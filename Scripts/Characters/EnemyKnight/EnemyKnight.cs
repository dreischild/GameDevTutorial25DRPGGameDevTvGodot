namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.EnemyKnight;

using Godot;

public partial class EnemyKnight : Character
{
    public override void _PhysicsProcess(double delta)
    {
        if (direction.X < 0)
        {
            sprite3dNode.FlipH = true;
        }
        else if (direction.X > 0)
        {
            sprite3dNode.FlipH = false;
        }
    }
}
