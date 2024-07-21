using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterTailAttack : SoulEaterState
{
    private float tailAttackRotationSpeed = 4;
    private bool attacking;
    private Collider tailCollider;
    private AudioSource swingAudio;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.rb = controller.GetComponent<Rigidbody>();
        controller.rb.angularVelocity = Vector3.zero;
        controller.rb.velocity = Vector3.zero;

        tailCollider = controller.tail.GetComponent<Collider>();
        tailCollider.enabled = true;
        attacking = false;

        swingAudio = controller.tail.GetComponent<AudioSource>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attacking)
        {
            Vector3 rotationVector = new Vector3(0, tailAttackRotationSpeed, 0);
            controller.rb.MoveRotation(Quaternion.Euler(rotationVector));
            controller.rb.velocity = Vector3.zero;
        }

    }

    override public void TailAttack()
    {
        attacking = true;
        swingAudio.Play();
    }

    public override void StopCharge()
    {
        attacking = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        tailCollider.enabled = false;
    }
}
