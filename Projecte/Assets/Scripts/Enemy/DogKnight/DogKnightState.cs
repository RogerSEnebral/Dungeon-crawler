using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogKnightState
{
    protected DogKnightController dogKnightController
    {
        get;
        private set;
    }

    public DogKnightState(DogKnightController controller)
    {
        dogKnightController = controller;
    }

    public virtual void Start()
    {}

    public virtual void FixedUpdate()
    {}

    public virtual void TakeDamage(float damage)
    {
        dogKnightController.health -= damage;
        DogKnightState newState = new DogKnightTakingDamageState(dogKnightController);
        dogKnightController.ChangeState(newState);
    }

    public virtual void Exit()
    {}

    public virtual void StartDefending()
    {}
}
