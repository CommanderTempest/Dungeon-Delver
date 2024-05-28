using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    public override void _PhysicsProcess(double delta)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }

    
}
