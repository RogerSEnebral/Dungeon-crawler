using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1ScreamingState : SoulEaterState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.GetComponent<CapsuleCollider>().enabled = true;

        Transform headTransform = controller.transform.Find("Root_Pelvis/Spine/Chest/Neck/Head");
        AudioSource roar = headTransform.GetComponent<AudioSource>();
        roar.Play();
        
        animator.SetInteger("Phase", animator.GetInteger("Phase") + 1);
    }

    public override void TakeDamage(float damage)
    {}
}
