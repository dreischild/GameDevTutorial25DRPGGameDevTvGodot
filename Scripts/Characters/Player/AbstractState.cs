namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

abstract public partial class AbstractState : Node
{
    // Notification zum Start der Animation
    public const int ACTIVATE_STATE__NOTIFICATION = 50000;
    // Notification deaktivieren des States (z.B. Stop der PhysicsProcess Methode)
    public const int DEACTIVATE_STATE__NOTIFICATION = 50001;

    protected Player player;

    abstract protected string animationName { get; }

    public override void _Ready()
    {
        // Über die GetOwner-Methode wird der Player-Node (Root-Node) abgerufen,
        // da dieser das Eltern-Node ist. Über <Player> wird der Typ gecastet.
        player = GetOwner<Player>();

        // State standardmäßig deaktivieren.
        DeactivateState();
    }

    // Die Notification-Methode wird aufgerufen, wenn eine Notification an diesen Node geschickt wird.
    public override void _Notification(int what)
    {
        // Es muss sichergestellt werden, dass die base Notification-Methode aufgerufen wird.
        base._Notification(what);

        if (what == ACTIVATE_STATE__NOTIFICATION)
        {
            ActivateState();
            EnterState();
        }
        else if (what == DEACTIVATE_STATE__NOTIFICATION)
        {
            DeactivateState();
        }
    }

    protected virtual void ActivateState()
    {
        SetPhysicsProcess(true);
        SetProcessInput(true);

        player.animationPlayer.Play(animationName);
    }

    protected virtual void DeactivateState()
    {
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    protected void StartMoveAndSlide(float speed = 5f, bool applyGravity = true)
    {
        // Der Vector2 wird in einen Vector3 umgewandelt und der Velocity zugewiesen.
        player.Velocity = new(player.direction.X, 0, player.direction.Y);

        // Die Geschwindigkeit wird skaliert auf Factor 5.
        player.Velocity *= speed;

        // Adding gravity
        if (applyGravity && !player.IsOnFloor())
        {
            player.Velocity += GameConstants.GRAVITY;
        }

        // Die Bewegung wird angewendet.
        player.MoveAndSlide();
    }
    
    protected virtual void EnterState() { }
}
