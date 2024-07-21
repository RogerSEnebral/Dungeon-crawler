using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour
{
    public int posRoom1;
    public int posRoom2;
    public string keyType;
    public int keyNum;
    public float keyRotationSpeed = 20;
    public float doorOpeningSpeed = 80;
    public GameObject room1;
    public GameObject room2;
    public int buttonsLeft;
    public int enemiesLeft = -1;
    public bool unlocked = false;
    private bool unlocking = false;
    public bool rotateKey;
    private AudioSource unlockingAudio;
    private AudioSource solvedAudio;
    private AudioSource rotationAudio;
    private Transform rotationPoint;
    public bool enemiesKilled = false;

    void Start()
    {
        unlockingAudio = GetComponent<AudioSource>();
        rotationPoint = transform.Find("rotationPoint");
        solvedAudio = rotationPoint.Find("Door").GetComponent<AudioSource>();
        rotationAudio = rotationPoint.GetComponent<AudioSource>();

        BlackOutKeys();
    }

    void Update()
    {
        if (unlocking)
        {
            if (rotateKey)
                KeyRotation();
            else
                DoorRotation();
        }
    }

    public void Unlock()
    {
        if (!unlocked && !unlocking)
        {
            unlocking = true;
            if (rotateKey)
                unlockingAudio.Play();
            else
                rotationAudio.Play();         
        }
    }

    public void PressedButton()
    {
        --buttonsLeft;
        if (buttonsLeft == 0)
        {
            solvedAudio.Play();
            ColorKeys();
        }
    }

    public void UnpressedButton()
    {
        ++buttonsLeft;
        if (buttonsLeft == 1)
        {
            BlackOutKeys();
        }
    }

    public void EnemyDead()
    {
        --enemiesLeft;
        if (enemiesLeft == 0)
            enemiesKilled = true;
    }

    public void CountEnemies()
    {
        enemiesLeft = 0;
        Transform parentTransform = transform.parent;
        if (parentTransform.name.Contains("Room"))
        {
            foreach (Transform child in parentTransform)
            {
                if (child.tag == "Enemy")
                {
                    ++enemiesLeft;
                }
            }
        }
        if (enemiesLeft == 0)
            enemiesKilled = true;
    }

    private void KeyRotation()
    {
        foreach (Transform child in rotationPoint.transform)
        {
            if (child.gameObject.tag == "DoorKey")
            {
                GameObject childKey = child.gameObject;

                if (childKey.transform.eulerAngles.z <= 90)
                {
                    childKey.transform.Rotate(0, 0, keyRotationSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    rotateKey = false;
                    unlockingAudio.Stop();
                    rotationAudio.Play();
                }
            }
        }
    }

    private void DoorRotation()
    {
        if (rotationPoint.localRotation.eulerAngles.y <= 150)
        {
            rotationPoint.Rotate(0, doorOpeningSpeed * Time.deltaTime, 0, Space.World);
        }
        else
        {
            unlocked = true;
            unlocking = false;
        }
    }

    private void BlackOutKeys()
    {
        if (buttonsLeft > 0)
        {
            foreach (Transform child in rotationPoint.transform)
            {
                if (child.gameObject.tag == "DoorKey")
                {
                    Transform planeTransform = child.Find("key_silver/pPlane3");
                    Material material = planeTransform.GetComponent<Renderer>().material;
                    material.color = Color.gray;
                }
            }
        }
    }

    private void ColorKeys()
    {
        foreach (Transform child in rotationPoint.transform)
        {
            if (child.gameObject.tag == "DoorKey")
            {
                Transform planeTransform = child.Find("key_silver/pPlane3");
                Material material = planeTransform.GetComponent<Renderer>().material;
                if (planeTransform.tag == "YellowKey")
                {
                    material.color = Color.yellow;
                }
                else
                {
                    material.color = new Color(165, 0, 173);
                }
            }
        }
    }
}
