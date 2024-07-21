using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderDyingState : BeholderState
{
    Rigidbody beholderRB;
    public BeholderDyingState(BeholderController controller) : base(controller)
    {
        beholderRB = beholderController.GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        Animator animator = beholderController.GetComponent<Animator>();
        animator.SetTrigger("Death");

        Transform rootTransform = beholderController.transform.Find("Root");
        AudioSource deathAudio = rootTransform.GetComponent<AudioSource>();
        deathAudio.Play();

        beholderController.gameObject.tag = "Untagged";
        beholderRB.constraints |= RigidbodyConstraints.FreezePosition;
    }

    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        beholderController.gameObject.tag = "Enemy";
        beholderRB.constraints &= ~RigidbodyConstraints.FreezePositionX;
        beholderRB.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        BeholderState newState = new BeholderActiveState(beholderController);
        beholderController.ChangeState(newState);
        beholderController.gameObject.SetActive(false);

        Transform parentTransform = beholderController.transform.parent;
        if(parentTransform.name.Contains("Room"))
        {
            foreach(Transform child in parentTransform)
            {
                if(child.tag == "Door")
                    child.GetComponent<DoorInfo>().EnemyDead();
            }
        }
    }
}
