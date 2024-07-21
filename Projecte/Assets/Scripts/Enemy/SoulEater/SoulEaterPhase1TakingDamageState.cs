using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1TakingDamageState : SoulEaterState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        base.LoseHealth(animator.GetFloat("Hit"));
        animator.SetFloat("Hit", 0);

        controller.rb.constraints |= RigidbodyConstraints.FreezePosition;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }

    override public void TakeDamage(float damage)
    {}
}
