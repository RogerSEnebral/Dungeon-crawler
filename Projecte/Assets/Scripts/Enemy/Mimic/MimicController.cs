using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicController : EnemyController
{
    public float linearSpeed = 10f;
    public float angularSpeed = 10;
    public GameObject player;
    public float attack1Cooldown = 1f;
    public float rotationStopTimeBite = 0.6f;
    public Material originalMat;
    public Color originalColour;
    public GameObject dropInside;
    public bool droped;
    private MimicState state;

    // Start is called before the first frame update
    override public void Start()
    {
        player = GameObject.Find("Player");
        originalMat = transform.Find("Mesh").GetComponent<Renderer>().material;
        originalColour = originalMat.color;

        MimicState newState = new MimicChestState(this);
        ChangeState(newState);
    }

    void FixedUpdate()
    {
        state.FixedUpdate();
    }

    public void ChangeState(MimicState newState)
    {
        state = newState;
        state.Start();
    }

    public override void TakeDamage(float damage)
    {
        state.TakeDamage(damage);
    }

    public void Open()
    {
        state.Open();
    }

    public void OnCollisionEnter(Collision collision)
    {
        state.OnCollisionEnter(collision);
    }

    public void Rotate()
    {
        state.Rotate();
    }

    public void Exit()
    {
        state.Exit();
    }
}
