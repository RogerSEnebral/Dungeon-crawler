using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterHovering : SoulEaterState
{
    private float hoveringTime = 6f;
    private float rechargeTime = 1f;
    private float lastShot;
    private Vector3 direction;
    private AudioSource shootingAudio;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        controller.GetComponent<CapsuleCollider>().enabled = false;

        lastShot = rechargeTime;

        SoulEaterDirectionManager directionManager = controller.GetComponent<SoulEaterDirectionManager>();
        direction = controller.player.transform.position - controller.transform.position;
        direction.Normalize();

        shootingAudio = controller.jaw.GetComponent<AudioSource>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Mover.Move(controller.rb, direction, controller.flyingSpeed);
        Mover.Rotate(controller.rb, direction, controller.angularSpeed);
        HoveringShot(animator);

        hoveringTime -= Time.deltaTime;
        if (hoveringTime <= 0f)
        {
            animator.SetTrigger("StopHovering");
        }
    }

    public void HoveringShot(Animator animator)
    {
        lastShot -= Time.deltaTime;
        if (lastShot <= 0f)
        {
            lastShot = rechargeTime;
            animator.SetTrigger("Shoot");
        }
    }

    override public void Shoot()
    {
        GameObject toInstantiate;
        toInstantiate = controller.spreadShot;
        Transform toInstantiateTransform = toInstantiate.transform;
        GameObject shot = Instantiate(toInstantiate, controller.jaw.transform.position, toInstantiateTransform.rotation);
        Transform mainShot = shot.transform.GetChild(0);
        mainShot.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        mainShot.GetComponent<SpreadShotController>().time = 100f;
        shot.SetActive(true);

        shootingAudio.Play();

        SoulEaterDirectionManager directionManager = controller.GetComponent<SoulEaterDirectionManager>();
        direction = directionManager.NewDirection();
    }
}