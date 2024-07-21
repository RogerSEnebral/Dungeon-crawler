using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1IdleState : SoulEaterState
{
    private float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        time = controller.stateTimeBase + Random.Range(0.5f, 1.5f)*controller.stateTimeOffset;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            animator.SetFloat("Speed", controller.linearSpeed);
        }
        else
        {
            Mover.Rotate(controller.rb, Vector3.back, controller.angularSpeed);
        }
    }
}
