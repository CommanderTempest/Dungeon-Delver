using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[ExportGroup("Required Nodes")]
	[Export] public Sprite3D Sprite {get; private set;}
	[Export] public AnimationPlayer AnimPlayerNode {get; private set;}
	[Export] public StateMachine StateMachineNode {get; private set;}


	public Vector2 direction = new();

	public override void _Input(InputEvent @event)
	{
		direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT, 
			GameConstants.INPUT_MOVE_RIGHT, 
			GameConstants.INPUT_MOVE_FORWARD, 
			GameConstants.INPUT_MOVE_BACK
		);
	}

	public void Flip()
	{
		bool isNotMovingHorizontally = Velocity.X == 0;

		if (isNotMovingHorizontally) {return;}
		
		bool isMovingLeft = Velocity.X < 0;

		Sprite.FlipH = isMovingLeft;
	}
}
