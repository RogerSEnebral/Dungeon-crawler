using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicLaughState : MimicState
{
    private Rigidbody mimicRb;

    public MimicLaughState(MimicController controller) : base(controller)
    {
        mimicRb = controller.GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        Animator mimicAnimator = mimicController.GetComponent<Animator>();
        mimicAnimator.SetTrigger("Laugh");

        Transform teethTransform = mimicController.transform.Find("Root/Body/Head/UpperTeethCTRL");
        AudioSource laughAudio = teethTransform.GetComponent<AudioSource>();
        laughAudio.Play();
        
        mimicRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void TakeDamage(float damage)
    {
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        base.TakeDamage(damage);
    }

    public override void Exit()
    {
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

        mimicController.ChangeState(new MimicActiveState(mimicController));
    }
}

