using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerActiveState
{

    public PlayerIdleState(PlayerController controller) : base(controller)
    {}

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            PlayerState newState = new PlayerWalkingState(playerController);
            ChangeState(newState);
        }
    }
}
