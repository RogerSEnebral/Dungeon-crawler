using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrowingState : BoomerangState
{
    private Vector3 throwOrigin;
    private Rigidbody rigidbody;
    private AudioSource throwingAudio;

    public BoomerangThrowingState(BoomerangController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        throwingAudio = controller.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        boomerangController.gameObject.SetActive(true);
        
        Transform boomerangTransform = boomerangController.transform;
        boomerangTransform.localPosition.Scale(Vector3.up);
        throwOrigin = boomerangTransform.position;
        throwingAudio.Play();
    }

    public override void FixedUpdate(int maxDistance, int speed)
    {
        Vector3 position = boomerangController.transform.position;
        Vector3 route = position - throwOrigin;
        int distance = (int) route.magnitude;
        if (distance > maxDistance)
        {
            BoomerangState newState = new BoomerangBackState(boomerangController);
            boomerangController.ChangeState(newState);
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != boomerangController.transform.parent.gameObject && collider.tag != "Sensor")
        {
            BoomerangState newState = new BoomerangBackState(boomerangController);
            boomerangController.ChangeState(newState);
        }
    }
}
