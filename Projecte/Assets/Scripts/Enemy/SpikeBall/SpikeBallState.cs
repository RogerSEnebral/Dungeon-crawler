using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpikeBallState
{
    protected SpikeBallController spikeBallController
    {
        get;
        private set;
    }

    public SpikeBallState(SpikeBallController controller)
    {
        spikeBallController = controller;
    }

    public virtual void Start()
    {}

    public virtual void FixedUpdate()
    {}

    public virtual void TakeDamage(float damage)
    {
        spikeBallController.health -= damage;
        SpikeBallState newState = new SpikeBallTakingDamageState(spikeBallController);
        spikeBallController.ChangeState(newState);
    }

    public virtual void Exit()
    {}
}
