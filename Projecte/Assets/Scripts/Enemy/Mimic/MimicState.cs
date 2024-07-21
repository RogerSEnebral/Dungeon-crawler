using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MimicState
{
    protected MimicController mimicController
    {
        get;
        private set;
    }

    public MimicState(MimicController controller)
    {
        mimicController = controller;
    }

    public virtual void Start()
    {}

    public virtual void FixedUpdate()
    {}

    public virtual void TakeDamage(float damage)
    {
        mimicController.health -= damage;
        MimicState newState = new MimicTakingDamageState(mimicController);
        mimicController.ChangeState(newState);
    }

    public virtual void Open()
    {}

    public virtual void Rotate()
    {}

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && mimicController.tag == "Enemy")
        {
            MimicState newState = new MimicLaughState(mimicController);
            mimicController.ChangeState(newState);
        } 
    }

    public virtual void Exit()
    {}
}
