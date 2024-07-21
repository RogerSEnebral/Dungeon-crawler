using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ElevatorController : MonoBehaviour
{
    private bool changing = false;
    private Vector3 startingMovementPosition;
    private Vector3 downPosition;
    private Vector3 upPosition;
    private Vector3 actualPosition;
    private Vector3 wantedPosition;
    private float elapsedAnimationTime;
    public float animationTime = 0.5f;
    private Collider boxCollider;
    public bool playerOnTop;

    public float height;
    public bool isUp;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (isUp == true)
        {
            boxCollider.enabled = false;
            upPosition = transform.position;
            downPosition = transform.position + new Vector3(0, -height, 0);
        }
        else
        {
            downPosition = transform.position;
            upPosition = transform.position + new Vector3(0, height, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startingMovementPosition = transform.position;
        actualPosition = transform.position;
        wantedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!actualPosition.Equals(wantedPosition) && !playerOnTop)
        {
            elapsedAnimationTime += Time.deltaTime;
            float percentageComplete = elapsedAnimationTime / animationTime;

            actualPosition.x = Mathf.Lerp(startingMovementPosition.x, wantedPosition.x, percentageComplete);
            actualPosition.y = Mathf.Lerp(startingMovementPosition.y, wantedPosition.y, percentageComplete);
            actualPosition.z = Mathf.Lerp(startingMovementPosition.z, wantedPosition.z, percentageComplete);
            if (percentageComplete >= 1)
            {
                elapsedAnimationTime = 0;
                startingMovementPosition = wantedPosition;
                actualPosition = wantedPosition;
                changing = false;
                boxCollider.enabled = !isUp;
            }

            transform.position = new Vector3(actualPosition.x, actualPosition.y, actualPosition.z);
        }
    }

    public void SwitchState()
    {
        changing = true;
        isUp = !isUp;

        if (isUp == false)
            wantedPosition = downPosition;

        else
            wantedPosition = upPosition;
    }

    public void GoUp()
    {
        if (isUp == false)
        {
            boxCollider.enabled = false;
            isUp = true;
            changing = true;
            wantedPosition = upPosition;
        }
    }

    public bool getChanging()
    {
        return changing;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
            playerOnTop = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
            playerOnTop = false;
    }
}