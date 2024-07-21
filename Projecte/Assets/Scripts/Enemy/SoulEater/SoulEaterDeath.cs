using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterDeath : SoulEaterState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.rb.velocity = Vector3.zero;
        controller.rb.angularVelocity = Vector3.zero;
        animator.SetInteger("Phase", animator.GetInteger("Phase") + 1);

        controller.rb.constraints = RigidbodyConstraints.FreezeAll;
        controller.tag = "Untagged";

        controller.gameManager.Invoke("WinGame", 4);

        AudioSource deathAudio = controller.transform.Find("Root_Pelvis").GetComponent<AudioSource>();
        deathAudio.Play();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.GetComponent<Rigidbody>().velocity = Vector3.zero;
        controller.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
