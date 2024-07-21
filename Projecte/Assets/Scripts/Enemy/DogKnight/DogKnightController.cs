using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightController : EnemyController
{
    public float linearSpeed = 1.2f;
    public float angularSpeed = 5;
    public GameObject player;
    public int comboCooldown = 4;
    public float swordAttackCooldown = 0.5f;
    public float timeDefending = 2;
    private DogKnightState state;
    
    override public void Start()
    {
        player = GameObject.Find("Player");
        DogKnightState newState = new DogKnightActiveState(this);
        ChangeState(newState);
    }

    void FixedUpdate()
    {
        state.FixedUpdate();
    }

    public void ChangeState(DogKnightState newState)
    {
        state = newState;
        state.Start();
    }

    public override void TakeDamage(float damage)
    {
        state.TakeDamage(damage);
    }

    public void StartDefending()
    {
        state.StartDefending();
    }

    public void Exit()
    {
        state.Exit();
    }
}
