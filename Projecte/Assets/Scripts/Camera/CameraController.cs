using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static GameManager gameManager;

    public float cameraMovementSpeed = 20;
    private bool changeRoom = false;
    private bool[] sign;
    private Vector3 destination;
 
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRoom)
        {
            Vector3 direction = destination - transform.position;
            if ((direction.x > 0f) != sign[0] || (direction.z > 0f) != sign[1]) 
            {
                changeRoom = false;
                gameManager.ResumeTime();
            }
            else
            {
                direction.Normalize();
                transform.Translate(direction * cameraMovementSpeed * Time.unscaledDeltaTime, Space.World);
            }
        }
    }

    public void ChangeRoom(Vector3 destination)
    {
        changeRoom = true;
        this.destination = destination;
        sign = new bool[] {destination.x - transform.position.x > 0f, destination.z - transform.position.z > 0f};
    }
}
