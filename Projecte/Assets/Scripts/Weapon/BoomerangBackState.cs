using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBackState : BoomerangState
{
    private Rigidbody rigidbody;

    public BoomerangBackState(BoomerangController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
    }

    public override void FixedUpdate(int maxDistance, int speed)
    {
        Transform boomerangTransform = boomerangController.transform;
        Vector3 direction = boomerangTransform.parent.position - boomerangTransform.position;
        direction.y = 0;
        int distance = (int) direction.magnitude;
        if (distance == 0)
        {
            BoomerangState newState = new BoomerangStoredState(boomerangController);
            boomerangController.ChangeState(newState);
        }
        else
        {
            direction.Normalize();
            Mover.Move(rigidbody, direction, speed);
        }
    }
}
