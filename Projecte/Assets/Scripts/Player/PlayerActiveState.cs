using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveState : PlayerState
{
    private BoomerangController boomerangController;

    public PlayerActiveState(PlayerController controller) : base(controller)
    {
        Transform boomerang = controller.transform.Find("boomerang");
        boomerangController = boomerang.GetComponent<BoomerangController>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetAxis("Fire4") != 0)
        {
            ChangeState(new PlayerAttackingSwordState(playerController));
        }
        else if (Input.GetAxis("Fire5") != 0 && boomerangController.available)
        {
            ChangeState(new PlayerAttackingBoomerangState(playerController));
        }
        else if (Input.GetAxis("Fire6") != 0)
        {
            if(!CheckCollisionChest())
                ChangeState(new PlayerPushingState(playerController));
        }
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Chest" || other.gameObject.tag == "Mimic")
        {
            playerController.collidedChest = other.gameObject;
        }
    }

    public override void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Chest" || other.gameObject.tag == "Mimic")
        {
            playerController.collidedChest = null;
        }
    }

    private bool CheckCollisionChest()
    {
        if (playerController.collidedChest != null)
        {
            if (playerController.collidedChest.tag == "Chest")
            {
                ChestController chestController = playerController.collidedChest.GetComponent<ChestController>();
                if (!chestController.opened)
                {
                    PlayerState newState = new PlayerOpeningState(playerController);
                    ChangeState(newState);
                    return true;
                }
            }
            else if (playerController.collidedChest.tag == "Mimic")
            {
                playerController.collidedChest.GetComponent<MimicController>().Open();
                return true;
            }
        }
        return false;
    }
}

