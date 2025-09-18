using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private Sprite3D sprite3dNode;
    [Export] private AnimationPlayer animationPlayer;

    private Vector2 direction = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    { 
        // Der Vector2 wird in einen Vector3 umgewandelt und der Velocity zugewiesen.
        Velocity = new(direction.X, 0, direction.Y);

        // Die Geschwindigkeit wird skaliert auf Factor 5.
        Velocity *= 5;

        // Die Bewegung wird angewendet.
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        /**
         * An dieser Stelle wird die Input-Abfrage durchgeführt.
         * Input.GetVector() gibt einen Vektor2 zurück, der die Richtung
         * basierend auf den angegebenen Eingaben darstellt.
         * 
         * Hierbei werden die Vectoren entsprechend addiert.
         *
         * Findet keine Eingabe statt, so ist der Vektor (0, 0).
         */
        direction = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
    }
}
