using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightDyingState : DogKnightState
{
    private Rigidbody dogRB;
    public DogKnightDyingState(DogKnightController controller) : base(controller)
    {
        dogRB = dogKnightController.GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        Animator animator = dogKnightController.GetComponent<Animator>();
        animator.SetTrigger("Death");

        Transform rootTransform = dogKnightController.transform.Find("root");
        AudioSource deathAudio = rootTransform.GetComponent<AudioSource>();
        deathAudio.Play();

        dogKnightController.gameObject.tag = "Untagged";
        dogRB.constraints |= RigidbodyConstraints.FreezePosition;
    }

    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        dogKnightController.gameObject.tag = "Enemy";
        dogRB.constraints &= ~RigidbodyConstraints.FreezePositionX;
        dogRB.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        DogKnightState newState = new DogKnightActiveState(dogKnightController);
        dogKnightController.ChangeState(newState);
        dogKnightController.gameObject.SetActive(false);

        Transform parentTransform = dogKnightController.transform.parent;
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
