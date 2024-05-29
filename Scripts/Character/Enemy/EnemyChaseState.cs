using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer chaseTimerNode;

    private CharacterBody3D target;

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        chaseTimerNode.Timeout += HandleChaseTimerTimeout;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        chaseTimerNode.Timeout -= HandleChaseTimerTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleChaseTimerTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }

    private void HandleAttackAreaEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
