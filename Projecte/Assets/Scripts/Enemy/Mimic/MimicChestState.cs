using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicChestState : MimicState
{
    private Rigidbody mimicRb;
    public MimicChestState(MimicController controller) : base(controller)
    {
        mimicRb = mimicController.GetComponent<Rigidbody>();
    }

    public override void Open()
    {
        mimicController.tag = "Enemy";
        mimicController.ChangeState(new MimicLaughState(mimicController));
    }

    public override void TakeDamage(float damage)
    {
        mimicController.tag = "Enemy";
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        mimicRb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

        base.TakeDamage(damage);
    }

}
