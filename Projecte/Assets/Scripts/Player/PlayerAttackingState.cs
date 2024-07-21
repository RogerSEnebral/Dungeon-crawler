using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerAttackingState : PlayerState
{
    private Animator animator;

    public PlayerAttackingState(PlayerController controller) : base(controller)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void Start()
    {
        animator.SetTrigger("Attack");
    }

    public override void Exit()
    {
        PlayerState newState = new PlayerIdleState(playerController);
        ChangeState(newState);
    }
}
