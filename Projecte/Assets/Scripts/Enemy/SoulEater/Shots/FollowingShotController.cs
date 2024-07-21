using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingShotController : EnemyController
{
    public float speed = 5f;
    public float time = 5f;
    private Rigidbody rb;
    private GameObject player;

    // Start is called before the first frame update
    override public void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        Mover.Move(rb, direction, speed);
    }
}
