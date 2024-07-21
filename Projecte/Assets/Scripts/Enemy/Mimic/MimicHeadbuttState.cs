using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicHeadbuttState : MimicState
{
    private Rigidbody mimicRb;
    private float timeToStartAttack = 0.2f;
    private bool attacking, rotation = false;
    private float headbuttRotationSpeed = 30;
    private float headbuttRotationDecrement = 0.5f;
    private Animator animator;
    private AudioSource spinningAudio;
    private AudioSource dizzyAudio;

    public MimicHeadbuttState(MimicController controller) : base(controller)
    {
        mimicRb = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();

        Transform bodyTransform = controller.transform.Find("Root/Body");
        spinningAudio = bodyTransform.Find("Head").GetComponent<AudioSource>();
        dizzyAudio = bodyTransform.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        mimicRb.constraints |= RigidbodyConstraints.FreezePositionX;
        mimicRb.constraints |= RigidbodyConstraints.FreezePositionZ;
    }

    public override void FixedUpdate()
    {
        if (!attacking)
        {
            timeToStartAttack -= Time.deltaTime;
            if (timeToStartAttack < 0)
            {
                attacking = true;
                animator.SetTrigger("Headbutt");
            }
        }
        else if (rotation)
        {
            if (headbuttRotationSpeed > 0) 
            {
                Vector3 rotationVector = new Vector3(0, headbuttRotationSpeed, 0);
                mimicController.transform.Rotate(rotationVector, Space.World);
                dizzyAudio.Play();
            }
            headbuttRotationSpeed -= headbuttRotationDecrement;
            headbuttRotationDecrement -= 0.004f;
        }
    }

    public override void TakeDamage(float damage)
    {
        Exit();
        base.TakeDamage(damage);
    }

    public override void Rotate()
    {
        rotation = true;
        spinningAudio.Play();
    }

    public override void Exit()
    {
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

        MimicState newState = new MimicActiveState(mimicController);
        mimicController.ChangeState(newState);
    }
}
