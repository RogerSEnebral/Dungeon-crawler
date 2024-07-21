using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPuzzle : MonoBehaviour
{
    public GameObject key;
    public GameObject circleSignal;
    private bool won;
    private bool correctOrder = true;
    private List<GameObject> enemyOrder;
    private GameObject keyInstance;
    private float yKeyOffset;
    private AudioSource[] sounds;

    public void Start()
    {
        keyInstance = Instantiate(key);
        keyInstance.SetActive(false);
        Transform pPlane3 = keyInstance.transform.Find("key_silver/pPlane3");
        BoxCollider keyCollider = pPlane3.GetComponent<BoxCollider>();
        yKeyOffset = keyCollider.size.y*pPlane3.parent.localScale.y/2.0f;

        sounds = GetComponents<AudioSource>();
    }

    public void Awake()
    {
        enemyOrder = new List<GameObject>();
    }

    public void RestartEnemies()
    {
        gameObject.SetActive(true);
        enemyOrder = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Enemy")
            {
                transform.GetChild(i).GetComponent<SpawnController>().ResetEnemy();
                enemyOrder.Add(transform.GetChild(i).gameObject);
            }
        }

        if (!won)
        {
            correctOrder = true;
            circleSignal.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyOrder.Count > 0 && correctOrder && !won)
        {
            CircleFollow();

            for (int i = 0; i < enemyOrder.Count; i++)
            {
                if (!enemyOrder[i].activeInHierarchy)
                {
                    if (i == 0)
                    {
                        enemyOrder.RemoveAt(i);
                    }
                    else
                    {
                        sounds[1].Play();
                        correctOrder = false;
                        circleSignal.SetActive(false);
                        enemyOrder.RemoveAt(i);
                    }
                }
            }

            if (enemyOrder.Count == 0)
            {
                if (correctOrder)
                {
                    WinPuzzle();
                }
            }
        }
    }

    private void WinPuzzle()
    {
        won = true;
        sounds[0].Play();

        keyInstance.SetActive(true);
        Vector3 circlePosition = circleSignal.transform.position;
        Vector3 keyPosition = new Vector3(circlePosition.x, yKeyOffset, circlePosition.z);
        keyInstance.transform.position = keyPosition;
        
        circleSignal.SetActive(false);
    }

    private void CircleFollow()
    {
        Vector3 circlePosition = new Vector3(enemyOrder[0].transform.position.x, circleSignal.transform.position.y,
                                             enemyOrder[0].transform.position.z);
        circleSignal.transform.position = circlePosition;
    }
}