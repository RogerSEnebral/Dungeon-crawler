using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Material red;
    public Material green;
    public GameObject[] linkedObjects;
    public MeshRenderer LED;
    private int objectsOnTop;
    private AudioSource switchAudio;

    void Start()
    {
        LED = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
        switchAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectsOnTop == 0)
        {
            switchAudio.Play();
        }
        ++objectsOnTop;
        LED.material = green;
        foreach (GameObject linkedObject in linkedObjects)
        {
            if (linkedObject.tag == "Door")
            {
                linkedObject.GetComponent<DoorInfo>().PressedButton();
            }
            else if (linkedObject.tag == "Elevator" && objectsOnTop == 1)
            {
                linkedObject.GetComponent<ElevatorController>().SwitchState();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectLeftButton(other.gameObject);
    }

    private void ObjectLeftButton(GameObject leftObject)
    {
        --objectsOnTop;
        if (objectsOnTop == 0)
        {
            LED.material = red;
            foreach(GameObject linkedObject in linkedObjects)
            {
                if(linkedObject.tag == "Door")
                {
                    linkedObject.GetComponent<DoorInfo>().UnpressedButton();
                }
                else if (linkedObject.tag == "Elevator")
                {
                    linkedObject.GetComponent<ElevatorController>().SwitchState();
                }
            }
            switchAudio.Play();
        }
    }
}
