using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterGoLandingPosition : SoulEaterState
{
    private Vector3 destination;
    private Vector3 direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        destination = controller.player.transform.position;

        direction = destination - controller.transform.position;
        direction.y = 0; 
        direction.Normalize();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.Distance(destination, new Vector3(controller.transform.position.x, 
                                        controller.player.transform.position.y, controller.transform.position.z)) < 0.5f)
        {
            animator.SetTrigger("Land");
            controller.rb.velocity = Vector3.zero;
            controller.rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Mover.Move(controller.rb, direction, controller.flyingSpeed);
            Mover.Rotate(controller.rb, direction, controller.angularSpeed);
        }
    }
}
