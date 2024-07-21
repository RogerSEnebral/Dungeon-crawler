using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{

    public float animationTime = 0.5f;
    private bool pushing = false;
    private Collider boxCollider;
    private Vector3 initialRoomPosition;
    private Vector3 startingMovementPosition;
    private Vector3 actualPosition;
    private Vector3 wantedPosition;
    private float elapsedAnimationTime;
    private AudioSource draggingSound;

    void Awake()
    {
        initialRoomPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingMovementPosition = initialRoomPosition;
        actualPosition = initialRoomPosition;
        wantedPosition = initialRoomPosition;
        boxCollider = GetComponent<Collider>();
        draggingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!actualPosition.Equals(wantedPosition))
        {
            elapsedAnimationTime += Time.deltaTime;
            float percentageComplete = elapsedAnimationTime / animationTime;

            actualPosition.x = Mathf.Lerp(startingMovementPosition.x, wantedPosition.x, percentageComplete);
            actualPosition.z = Mathf.Lerp(startingMovementPosition.z, wantedPosition.z, percentageComplete);
            if (percentageComplete >= 1) 
            {
                elapsedAnimationTime = 0;
                startingMovementPosition = wantedPosition;
                actualPosition = wantedPosition;
                pushing = false;
            }

            transform.position = new Vector3(actualPosition.x, transform.position.y, actualPosition.z);
        }
    }

    public void StartPushing(Vector3 direction)
    {
        pushing = true;
        Vector3 scaleVector = new Vector3(1.5f, 0, 1.5f);
        wantedPosition = transform.position + Vector3.Scale(scaleVector, direction);
        draggingSound.Play();
    }


    public bool CanPushObject(Vector3 direction)
    {
        return !Physics.BoxCast(boxCollider.bounds.center, 
                boxCollider.bounds.extents/2f, direction, transform.rotation, 1.5f);
    }

    public bool GetPushing()
    {
        return pushing;
    }

    public void RestartPosition()
    {
        Start();
        transform.position = initialRoomPosition;
    }
}
