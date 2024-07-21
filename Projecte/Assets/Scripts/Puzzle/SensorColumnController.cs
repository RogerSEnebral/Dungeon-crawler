using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorColumnController : MonoBehaviour
{
    public Material red;
    public Material green;
    public GameObject[] linkedObjects;
    public MeshRenderer LED;
    public float switchingTime = 4.5f;
    private float actualSwitchingTime;
    private bool switched;
    private bool unlocked;
    private AudioSource switchAudio;

    void Start()
    {
        LED = gameObject.GetComponent<MeshRenderer>();
        switchAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (switched && !unlocked)
        {
            if (actualSwitchingTime > 0)
            {
                actualSwitchingTime -= Time.deltaTime;
            }
            else
            {
                switchAudio.Play();
                LED.material = red;
                switched = false;
                foreach (GameObject linkedObject in linkedObjects)
                {
                    if (linkedObject.tag == "Door")
                    {
                        linkedObject.GetComponent<DoorInfo>().UnpressedButton();
                    }
                }
            }
        }

        if (!unlocked)
        {
            foreach (GameObject linkedObject in linkedObjects)
            {
                if (linkedObject.tag == "Door")
                {
                    unlocked = linkedObject.GetComponent<DoorInfo>().buttonsLeft <= 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            if (!unlocked && !switched)
            {
                switchAudio.Play();
                LED.material = green;
                switched = true;
                actualSwitchingTime = switchingTime;
                foreach (GameObject linkedObject in linkedObjects)
                {
                    if (linkedObject.tag == "Door")
                    {
                        linkedObject.GetComponent<DoorInfo>().PressedButton();
                        if (linkedObject.GetComponent<DoorInfo>().buttonsLeft <= 0)
                        {
                            ColumnsPuzzle columnsPuzzle = transform.parent.parent.GetComponent<ColumnsPuzzle>();
                            GameObject drop = GameObject.Instantiate(columnsPuzzle.drop);
                            Vector3 dropPosition = columnsPuzzle.dropPosition;
                            drop.transform.position = dropPosition;
                            unlocked = true;
                        }
                    }
                    else if (linkedObject.tag == "Elevator")
                    {
                        linkedObject.GetComponent<ElevatorController>().GoUp();
                        unlocked = true;
                    }
                }
            }
        }
    }
}
