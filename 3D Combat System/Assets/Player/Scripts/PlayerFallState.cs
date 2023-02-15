using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CROSS_FADE_DURATION = 0.1f;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = 0f;

        stateMachine.animator.CrossFadeInFixedTime(FallHash, CROSS_FADE_DURATION);
    }
    public override void Tick()
    {
        ApplyGravity();
        Move();

        if (stateMachine.controller.isGrounded) 
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
    }
    public override void Exit() { }
    

}
