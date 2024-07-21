using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterBasicAttack : SoulEaterState
{
    private bool biting;
    private Vector3 direction;
    private AudioSource biteAudio;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.rb.angularVelocity = Vector3.zero;
        controller.rb.velocity = Vector3.zero;
        biting = false;

        Transform neckTransform = controller.transform.Find("Root_Pelvis/Spine/Chest/Neck");
        biteAudio = neckTransform.GetComponent<AudioSource>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!biting)
        {
            getPlayerDirection();
            Mover.Rotate(controller.rb, direction, controller.angularSpeed);
        }
        else
        {
            Mover.Move(controller.rb, direction, controller.linearSpeed*3f);
            controller.rb.angularVelocity = Vector3.zero;
        }
        
    }

    override public void Bite()
    {
        biting = true;
        getPlayerDirection();
        controller.rb.angularVelocity = Vector3.zero;
        biteAudio.Play();
    }

    public override void StopCharge()
    {
        controller.rb.velocity = Vector3.zero;
    }

    private void getPlayerDirection()
    {
        direction = controller.player.transform.position - controller.transform.position;
        direction.y = 0;
        direction.Normalize();
    }
}