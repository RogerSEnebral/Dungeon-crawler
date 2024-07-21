using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
