using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1RunningState : SoulEaterState
{
    private float time;
    private Vector3 direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        time = controller.stateTimeBase + Random.Range(0.5f, 1.5f)*controller.stateTimeOffset;

        SoulEaterDirectionManager directionManager = controller.GetComponent<SoulEaterDirectionManager>();
        direction = directionManager.NewDirection();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            controller.rb.velocity = Vector3.zero;
        }
        else
        {
            Mover.Move(controller.rb, direction, controller.linearSpeed);
            Mover.Rotate(controller.rb, direction, controller.angularSpeed);
        }
        animator.SetFloat("Speed", controller.rb.velocity.magnitude);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.rb.velocity = Vector3.zero;
        animator.SetFloat("Speed", 0);
        if (Random.value <= 0.75)
        {
            if (Random.value <= 0.3)
            {
                animator.SetTrigger("Shoot3");
            }
            else
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
}
