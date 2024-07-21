using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomController : MonoBehaviour
{
    private static GameManager gameManager;
    private PlayerPickUpController playerPickUpController;

    void Start()
    {
        gameManager = GameManager.instance;
        playerPickUpController = GetComponent<PlayerPickUpController>();
    }

    // We use on collision stay so that in the case that the player stays on the door while it is unlocking, he automatically
    // goes through when finished
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            DoorInfo doorInfo = collision.gameObject.GetComponent<DoorInfo>();
            if (doorInfo.buttonsLeft <= 0 && doorInfo.enemiesKilled && playerPickUpController.SpendKeys(doorInfo.keyNum, doorInfo.keyType))
            {
                doorInfo.Unlock();
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            DoorInfo doorInfo = collision.gameObject.GetComponent<DoorInfo>();

            if (doorInfo.unlocked)
            {
                gameManager.ChangeRoom(doorInfo.room1, doorInfo.room2, doorInfo.posRoom1, doorInfo.posRoom2);
            }
        }
    }
}
