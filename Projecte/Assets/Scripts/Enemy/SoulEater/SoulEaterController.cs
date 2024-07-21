using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterController : EnemyController
{
    public GameManager gameManager;
    public Rigidbody rb;

    public float stateTimeBase = 0.5f;
    public float stateTimeOffset = 1f;
    public float linearSpeed = 2f;
    public float angularSpeed = 15f;
    public float flyingSpeed = 3.5f;
    public float comboCooldown = 5f;
    public float intoComboCooldown = 1f;

    public float damageUntilStagger = 10f;
    public float actualDamageUntilStagger; 

    public Material material;
    public Color originalColour;

    public GameObject jaw;
    public GameObject tail;

    public GameObject player {
        get;
        private set;
    }
    public GameObject spreadShot {
        get;
        private set;
    }
    public GameObject followingShot {
        get;
        private set;
    }
    private SoulEaterState state;
    private AudioSource flyingAudio;
    
    override public void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
        player = GameObject.Find("Player");
        
        spreadShot = transform.Find("SpreadShot").gameObject;
        followingShot = transform.Find("FollowingShot").gameObject;
        actualDamageUntilStagger = damageUntilStagger;

        Transform wingTransform = transform.Find("Root_Pelvis/Spine/Wing01_Right");
        flyingAudio = wingTransform.GetComponent<AudioSource>();
    }

    override public void TakeDamage(float damage)
    {
        state.TakeDamage(damage);
    }

    public void ChangeState(SoulEaterState newState)
    {
        state = newState;
    }

    public void Shoot()
    {
        state.Shoot();
    }

    public void Bite()
    {
        state.Bite();
    }

    public void TailAttack()
    {
        state.TailAttack();
    }

    public void StopCharge()
    {
        state.StopCharge();
    }

    public void ReturnToNormalColor()
    {
        material.color = originalColour;
    }

    public void Fly(int flying)
    {
        if (flying == 0)
        {
            flyingAudio.Stop();
        }
        else
        {
            flyingAudio.Play();
        }
    }
}
