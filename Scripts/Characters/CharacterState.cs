namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;

abstract public partial class CharacterState : Node
{
    // Notification zum Start der Animation
    public const int ACTIVATE_STATE__NOTIFICATION = 50000;
    // Notification deaktivieren des States (z.B. Stop der PhysicsProcess Methode)
    public const int DEACTIVATE_STATE__NOTIFICATION = 50001;

    protected Character character;

    public override void _Ready()
    {
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
    }

    protected virtual void DeactivateState()
    {
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    protected void StartMoveAndSlide(float speed = 5f, bool applyGravity = true)
    {
        // Der Vector2 wird in einen Vector3 umgewandelt und der Velocity zugewiesen.
        character.Velocity = new(character.direction.X, 0, character.direction.Y);

        // Die Geschwindigkeit wird skaliert auf Factor 5.
        character.Velocity *= speed;

        // Adding gravity
        if (applyGravity && !character.IsOnFloor())
        {
            character.Velocity += GameConstants.GRAVITY;
        }

        // Die Bewegung wird angewendet.
        character.MoveAndSlide();
    }
    
    protected virtual void EnterState() { }
}
