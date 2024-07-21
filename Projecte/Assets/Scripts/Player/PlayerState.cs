using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController playerController
    {
        get;
        private set;
    }
    private bool invencibleReady = true;

    public PlayerState(PlayerController controller)
    {
        playerController = controller;
    }

    public virtual void Start()
    {}

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetAxisRaw("Invulnerable") != 0)
        {
            if (invencibleReady)
            {
                playerController.invencible = !playerController.invencible;
                invencibleReady = false;
            }
        }
        else
        {
            invencibleReady = true;
        }
    }

    public virtual void FixedUpdate(int linearSpeed, float angularSpeed)
    {}

    public virtual void OnCollisionEnter(Collision collision)
    {}

    public virtual void OnCollisionExit(Collision collision)
    {}

    public virtual void TakeDamage(int damage)
    {
        if (!playerController.invencible)
        {
            playerController.health -= damage;
        }
        PlayerState newState = new PlayerTakingDamageState(playerController);
        ChangeState(newState);
    }

    public virtual void Attack(int inProgress)
    {}

    public virtual void Push()
    {}

    public virtual void Exit()
    {}

    public virtual void ChangeState(PlayerState newState)
    {
        playerController.ChangeState(newState);
    }
}
