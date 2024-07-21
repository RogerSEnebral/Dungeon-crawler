using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : EnemyController
{
    public float linearSpeed = 0.8f;
    public float angularSpeed = 3;
    public GameObject player;
    public int comboCooldown = 4;
    public float attack1Cooldown = 2f;
    public float attack2Cooldown = 3.5f;
    private SpikeBallState state;

    // Start is called before the first frame update
    override public void Start()
    {
        player = GameObject.Find("Player");
        SpikeBallState newState = new SpikeBallActiveState(this);
        ChangeState(newState);
    }

    void FixedUpdate()
    {
        state.FixedUpdate();
    }

    public void ChangeState(SpikeBallState newState)
    {
        state = newState;
        state.Start();
    }

    public override void TakeDamage(float damage)
    {
        state.TakeDamage(damage);
    }

    public void Exit()
    {
        state.Exit();
    }
}
