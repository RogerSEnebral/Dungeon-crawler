using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase2RunningState : SoulEaterState
{
    private float time;
    private Vector3 direction;
    private float distanceWithPlayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        time = controller.comboCooldown;
        controller.rb.angularVelocity = Vector3.zero;
        controller.rb.velocity = Vector3.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = controller.player.transform.position - controller.transform.position;
        direction.y = 0;
        direction.Normalize();

        Mover.Rotate(controller.rb, direction, controller.angularSpeed);

        distanceWithPlayer = Vector3.Distance(controller.player.transform.position, controller.transform.position);
        if (distanceWithPlayer > 2.25f)
            Mover.Move(controller.rb, direction, controller.linearSpeed);
        else if (distanceWithPlayer < 2f)
            Mover.Move(controller.rb, -direction, controller.linearSpeed);

        time -= Time.deltaTime;
        if (time <= 0f)
        {
            ChooseAttack(animator);
        }
        animator.SetFloat("Speed", controller.rb.velocity.magnitude);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {}

    private void ChooseAttack(Animator animator)
    {
        if (distanceWithPlayer < 5f)
        {
            if (Random.value <= 0.5)
                animator.SetTrigger("Combo1");
            else
                animator.SetTrigger("Combo2");
        }

        else
        {
            if (Random.value <= 0.3)
            {
                controller.rb.velocity = Vector3.zero;
                animator.SetFloat("Speed", 0);
                animator.SetTrigger("Shoot");
                time = controller.intoComboCooldown;
            }
        }

    }
}
