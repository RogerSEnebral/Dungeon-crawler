using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallDyingState : SpikeBallState
{
    Rigidbody spikeBallRB;
    public SpikeBallDyingState(SpikeBallController controller) : base(controller)
    {
        spikeBallRB = spikeBallController.GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        Animator animator = spikeBallController.GetComponent<Animator>();
        animator.SetTrigger("Death");

        Transform rootTransform = spikeBallController.transform.Find("Body");
        AudioSource deathAudio = rootTransform.GetComponent<AudioSource>();
        deathAudio.Play();

        spikeBallController.gameObject.tag = "Untagged";
        spikeBallRB.constraints |= RigidbodyConstraints.FreezePosition;
    }

    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        spikeBallController.gameObject.tag = "Enemy";
        spikeBallRB.constraints &= ~RigidbodyConstraints.FreezePositionX;
        spikeBallRB.constraints &= ~RigidbodyConstraints.FreezePositionZ;

        SpikeBallState newState = new SpikeBallActiveState(spikeBallController);
        spikeBallController.ChangeState(newState);

        spikeBallController.GetComponent<RandomDrop>().Drop();
        spikeBallController.gameObject.SetActive(false);

        Transform parentTransform = spikeBallController.transform.parent;
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
