namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters.Player;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;
using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private Sprite3D sprite3dNode;
    [Export] public AnimationPlayer animationPlayer { get; private set; }
    [Export] public StateMachine stateMachine { get; private set; }

    public Vector2 direction = Vector2.Zero;
    private StringName currentAnimation = GameConstants.ANIM_IDLE;

    public override void _Ready()
    {
    }   

    public override void _PhysicsProcess(double delta)
    {
        
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
        direction = Input.GetVector(
            GameConstants.ACTION_MOVE_LEFT,
            GameConstants.ACTION_MOVE_RIGHT,
            GameConstants.ACTION_MOVE_FORWARD,
            GameConstants.ACTION_MOVE_BACKWARD
            );

        // Sprite drehen
        if (Input.IsActionPressed(GameConstants.ACTION_MOVE_LEFT) || Input.IsActionPressed(GameConstants.ACTION_MOVE_RIGHT))
        {
            // True, wenn sich der Spieler nach links bewegt.
            sprite3dNode.FlipH = Input.IsActionPressed(GameConstants.ACTION_MOVE_LEFT);
        }
    }
}
