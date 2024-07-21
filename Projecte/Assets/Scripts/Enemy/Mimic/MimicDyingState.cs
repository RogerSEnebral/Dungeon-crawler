using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicDyingState : MimicState
{
    Rigidbody mimicRB;
    public MimicDyingState(MimicController controller) : base(controller)
    {
        mimicRB = mimicController.GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        Animator animator = mimicController.GetComponent<Animator>();
        animator.SetTrigger("Death");

        Transform rootTransform = mimicController.transform.Find("Root");
        AudioSource deathAudio = rootTransform.GetComponent<AudioSource>();
        deathAudio.Play();

        mimicController.gameObject.tag = "Mimic";
        mimicRB.constraints |= RigidbodyConstraints.FreezePosition;
    }

    public override void TakeDamage(float damage)
    { }

    private void DropInsides()
    {
        if (!mimicController.droped)
        {
            if (mimicController.dropInside != null)
            {
                GameObject drop = GameObject.Instantiate(mimicController.dropInside);
                Vector3 dropPosition = mimicController.transform.position;
                dropPosition.y += 0.5f;
                drop.transform.position = dropPosition;
            }
            mimicController.droped = true;
        }
    }

    public override void Exit()
    {
        DropInsides();
        mimicController.gameObject.tag = "Mimic";
        MimicState newState = new MimicChestState(mimicController);
        mimicController.ChangeState(newState);
        mimicController.gameObject.SetActive(false);

        Transform parentTransform = mimicController.transform.parent;
        if (parentTransform.name.Contains("Room"))
        {
            foreach (Transform child in parentTransform)
            {
                if (child.tag == "Door")
                    child.GetComponent<DoorInfo>().EnemyDead();
            }
        }
    }
}
