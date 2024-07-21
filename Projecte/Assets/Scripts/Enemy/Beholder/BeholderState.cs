using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeholderState
{
    protected BeholderController beholderController
    {
        get;
        private set;
    }

    public BeholderState(BeholderController controller)
    {
        beholderController = controller;
    }

    public virtual void Start()
    {}

    public virtual void FixedUpdate(int linearSpeed, float angularSpeed, int minDistance)
    {}

    public virtual void TakeDamage(float damage)
    {
        beholderController.health -= damage;
        BeholderState newState = new BeholderTakingDamageState(beholderController);
        beholderController.ChangeState(newState);
    }

    public virtual void Shoot(int shotSpeed, Vector3 shotOffset, GameObject shotModel)
    {}

    public virtual void Exit()
    {}
}
