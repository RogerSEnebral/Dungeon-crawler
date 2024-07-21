using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1DefendingState : SoulEaterState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Transform wingTransform = controller.transform.Find("Root_Pelvis/Spine/Wing01_Left");
        AudioSource defendAudio = wingTransform.GetComponent<AudioSource>();
        defendAudio.Play();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat("Hit", 0);
        animator.SetTrigger("Shoot3");
    }

    override public void TakeDamage(float damage)
    {}
}
