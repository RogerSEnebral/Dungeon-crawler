using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerOpeningState : PlayerState
{
    private static GameManager gameManager;
    private Transform hand;
    private GameObject sword;
    private GameObject light;
    private GameObject handObject;
    private PlayerPickUpController playerPickUpController;
    
    public PlayerOpeningState(PlayerController controller) : base(controller)
    {
        gameManager = GameManager.instance;
        hand = controller.transform.Find(PlayerController.WEAPON_PATH);
        sword = hand.Find("OHS03").gameObject;
        light = hand.Find("Light").gameObject;
        playerPickUpController = playerController.GetComponent<PlayerPickUpController>();
    }

    public override void Start()
    {
        gameManager.StopTimeScale();

        sword.SetActive(false);
        light.SetActive(true);

        Animator animator = playerController.GetComponent<Animator>();
        animator.SetTrigger("Open");

        TakeInsides();
    }

    private void TakeInsides()
    {
        ChestController chestController = playerController.collidedChest.GetComponent<ChestController>();

        chestController.Open();
        switch (chestController.content)
        {
            case ChestContent.KEY:
                playerPickUpController.NewYellowKey();
                handObject = hand.Find("Key").gameObject;
                break;

            case ChestContent.BOSS_KEY:
                playerPickUpController.NewPurpleKey();
                handObject = hand.Find("KeyBoss").gameObject;
                break;

            case ChestContent.BOOMERANG:
                playerPickUpController.GetBoomerang();
                handObject = hand.Find("boomerang").gameObject;
                break;
        }
        handObject.SetActive(true);
    }

    public override void Exit()
    {
        gameManager.ResumeTime();

        light.SetActive(false);
        handObject.SetActive(false);
        sword.SetActive(true);

        PlayerState newState = new PlayerIdleState(playerController);
        ChangeState(newState);
    }
}
