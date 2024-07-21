using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangStoredState : BoomerangState
{
    private Rigidbody rigidbody;
    private AudioSource throwingAudio;

    public BoomerangStoredState(BoomerangController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        throwingAudio = controller.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        boomerangController.gameObject.SetActive(false);
        throwingAudio.Stop();
    }

    public override void Throw(Vector3 direction, int speed)
    {
        Mover.Move(rigidbody, direction, speed);

        BoomerangState newState = new BoomerangThrowingState(boomerangController);
        boomerangController.ChangeState(newState);
    }

    public override bool isAvailable()
    {
        return true;
    }
}
