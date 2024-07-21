using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackingBoomerangState : PlayerAttackingState
{
    private GameObject sword;
    private GameObject playerBoomerang;
    private BoomerangController boomerangController;

    public PlayerAttackingBoomerangState(PlayerController controller) : base(controller)
    {
        sword = controller.transform.Find($"{PlayerController.WEAPON_PATH}/OHS03").gameObject;
        playerBoomerang = controller.transform.Find($"{PlayerController.WEAPON_PATH}/boomerang").gameObject;
        Transform boomerang = controller.transform.Find("boomerang");
        boomerangController = boomerang.GetComponent<BoomerangController>();
    }

    public override void Start()
    {
        playerBoomerang.SetActive(true);
        sword.SetActive(false);
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        sword.SetActive(true);
        base.TakeDamage(damage);
    }

    public override void Attack(int inProgress)
    {
        if (inProgress == 1)
        {
            playerBoomerang.SetActive(false);
            Transform playerTransform = playerController.transform;
            boomerangController.Throw(playerTransform.forward);
        }
    }

    public override void Exit()
    {
        sword.SetActive(true);
        base.Exit();
    }
}
