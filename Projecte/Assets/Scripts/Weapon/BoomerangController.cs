using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    public int speed = 10;
    public int maxDistance = 10;
    private bool _available = false;
    private BoomerangState state;

    // Start is called before the first frame update
    void Start()
    {
        BoomerangState newState = new BoomerangStoredState(this);
        ChangeState(newState);
    }

    void FixedUpdate()
    {
        state.FixedUpdate(maxDistance, speed);
    }

    public void Throw(Vector3 direction)
    {
        state.Throw(direction, speed);
    }

    public void OnTriggerEnter(Collider collider)
    {
        state.OnTriggerEnter(collider);
    }

    public bool available
    {
        get => _available && state.isAvailable();
        set => _available = value;
    }

    public void ChangeState(BoomerangState newState)
    {
        state = newState;
        state.Start();
    }

    public bool IsPicked()
    {
        return _available;
    }
}
