using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterPhase1ShootingState : SoulEaterState
{
    private Vector3 direction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.rb.constraints |= RigidbodyConstraints.FreezePosition;

        direction = controller.player.transform.position - controller.transform.position;
        direction.Normalize();

        AudioSource shootingAudio = controller.jaw.GetComponent<AudioSource>();
        shootingAudio.Play();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Mover.Rotate(controller.rb, direction, controller.angularSpeed);
    }

    override public void Shoot()
    {
        GameObject toInstantiate;
        if (Random.value <= 0.4f)
        {
            toInstantiate = controller.followingShot;
        }
        else
        {
            toInstantiate = controller.spreadShot;
        }

        GameObject shot = Instantiate(toInstantiate, controller.jaw.transform.position, toInstantiate.transform.rotation);
        shot.SetActive(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }
}
