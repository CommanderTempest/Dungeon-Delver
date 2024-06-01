using System;
using System.ComponentModel;
using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{
	[Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
	[Export] public Sprite3D Sprite {get; private set;}
	[Export] public AnimationPlayer AnimPlayerNode {get; private set;}
	[Export] public StateMachine StateMachineNode {get; private set;}
	[Export] public Area3D HitboxNode {get; private set;}
	[Export] public Area3D HurtboxNode {get; private set;}
	[Export] public CollisionShape3D HitboxShapeNode {get; private set;}
	

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode {get; private set;}
    [Export] public NavigationAgent3D AgentNode {get; private set;}
	[Export] public Area3D ChaseAreaNode {get; private set;}
	[Export] public Area3D AttackAreaNode {get; private set;}

	[Export] public Timer ShaderTimer {get; private set;}
	
		[Export] private ShaderMaterial shader;
    public Vector2 direction = new();

    public override void _Ready()
    {
			shader = (ShaderMaterial) Sprite.MaterialOverlay;
        HurtboxNode.AreaEntered += HandleHurtboxEntered;
				Sprite.TextureChanged += HandleTextureChanged;
				ShaderTimer.Timeout += () => shader.SetShaderParameter("active", false);
    }

    


    public void Flip()
	{
		bool isNotMovingHorizontally = Velocity.X == 0;

		if (isNotMovingHorizontally) {return;}
		
		bool isMovingLeft = Velocity.X < 0;

		Sprite.FlipH = isMovingLeft;
	}

	private void HandleHurtboxEntered(Area3D area)
    {
			if (area is not IHitbox hitbox)
			{
				return;
			}

			StatResource health = GetStatResource(Stat.Health);

			float damage = hitbox.GetDamage();

			health.StatValue -= damage;

			shader.SetShaderParameter(
				"active", true
			);
			ShaderTimer.Start();
    }

		private void HandleTextureChanged()
    {
        shader.SetShaderParameter(
					"tex", Sprite.Texture
				);
    }
	public StatResource GetStatResource(Stat stat)
	{
		return stats.Where((element) => element.StatType == stat).FirstOrDefault();
	}

	public void ToggleHitbox(bool flag) 
	{
		HitboxShapeNode.Disabled = flag;
	}

}