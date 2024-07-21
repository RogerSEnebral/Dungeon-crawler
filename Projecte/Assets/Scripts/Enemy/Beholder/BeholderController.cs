using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderController : EnemyController
{
    public int linearSpeed = 5;
    public float angularSpeed = 10;
    public int minDistance;
    public GameObject shotModel;
    public GameObject player
    {
        get;
        private set;
    }
    public int shootingRate = 5;
    public int shotSpeed = 10;
    public Vector3 shotOffset;
    private BeholderState state;
    
    override public void Start()
    {
        player = GameObject.Find("Player");
        BeholderState newState = new BeholderActiveState(this);
        ChangeState(newState);
    }

    void FixedUpdate()
    {
        state.FixedUpdate(linearSpeed, angularSpeed, minDistance);
    }

    public void ChangeState(BeholderState newState)
    {
        state = newState;
        state.Start();
    }

    public override void TakeDamage(float damage)
    {
        state.TakeDamage(damage);
    }

    public void Shoot()
    {
        state.Shoot(shotSpeed, shotOffset, shotModel);
    }

    public void Exit()
    {
        state.Exit();
    }
}
