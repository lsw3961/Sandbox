using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float ANIMATION_DAMP_TIME = .01f;
    private const float CROSS_FADE_DURATION = .01f;

    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.animator.CrossFadeInFixedTime(moveBlendTreeHash, CROSS_FADE_DURATION);

        stateMachine.inputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void Tick()
    {
        if (!stateMachine.controller.isGrounded) 
        {
            //stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        stateMachine.animator.SetFloat(moveSpeedHash, stateMachine.inputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, ANIMATION_DAMP_TIME, Time.deltaTime);


    }

    public override void Exit()
    {
        stateMachine.inputReader.OnJumpPerformed -= SwitchToJumpState;
    }

    private void SwitchToJumpState() 
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

}
