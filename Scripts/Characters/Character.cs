namespace GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;

using GameDevTutorial25DRPGGameDevTvGodot.Scripts.Characters;
using GameDevTutorial25DRPGGameDevTvGodot.Scripts.General;
using Godot;
using System;

public partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] protected Sprite3D sprite3dNode;
    [Export] public AnimationPlayer animationPlayer { get; private set; }
    [Export] public StateMachine stateMachine { get; private set; }

    public Vector2 direction = Vector2.Zero;
    private StringName currentAnimation = GameConstants.ANIM_IDLE;
}