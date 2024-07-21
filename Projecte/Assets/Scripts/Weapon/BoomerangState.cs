using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoomerangState
{
    protected BoomerangController boomerangController
    {
        get;
        private set;
    }

    public BoomerangState(BoomerangController controller)
    {
        boomerangController = controller;
    }

    public virtual void Start()
    {}

    public virtual void FixedUpdate(int maxDistance, int speed)
    {}

    public virtual void Throw(Vector3 direction, int speed)
    {}

    public virtual void OnTriggerEnter(Collider collider)
    {}

    public virtual bool isAvailable()
    {
        return false;
    }
}
