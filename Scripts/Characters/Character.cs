namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;
using System.Linq;

public partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] protected Sprite3D sprite3dNode;
    [Export] public AnimationPlayer animationPlayer { get; private set; }
    [Export] public StateMachine stateMachine { get; private set; }

    [ExportGroup("Enemy Nodes")]
    [Export] public Path3D Path3DNode;
    [Export] public NavigationAgent3D navigationAgent3DNode;
    [Export] public Area3D chaseArea3DNode;

    public Vector2 direction = Vector2.Zero;
    public Character targetCharacter;
    private StringName currentAnimation = GameConstants.ANIM_IDLE;

    public bool CanSeeObject(Node3D target)
    {
        if (target == null)
        {
            return false;
        }

        // Ray-Konfiguration
        PhysicsRayQueryParameters3D queryParameters3D = new PhysicsRayQueryParameters3D
        {
            From = GlobalPosition,
            To = target.GlobalPosition,
            CollideWithAreas = true
        };

        // Raycast durchführen
        var spaceState = GetWorld3D().DirectSpaceState;
        var result = spaceState.IntersectRay(queryParameters3D);

        // Von Variante zu Node3D casten
        var collider = result["collider"].As<Node3D>();

        return collider == target;
    }
}