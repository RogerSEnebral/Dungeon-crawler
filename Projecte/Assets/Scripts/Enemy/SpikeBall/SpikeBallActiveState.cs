using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallActiveState : SpikeBallState
{
    private float timeToAttack;
    private Animator animator;
    private Rigidbody rigidbody;
    private Transform transform;
    private GameObject player;
    private int combo = 0;
    private bool attacking = false;
    private float rotationStopTime;
    private AudioSource attackAudio;
    private AudioSource attackAudio2;

    public SpikeBallActiveState(SpikeBallController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();
        transform = controller.transform;
        player = controller.player;

        Transform spineTransform = controller.transform.Find("Body/Spine01");
        attackAudio = spineTransform.Find("Shell").GetComponent<AudioSource>();
        attackAudio2 = spineTransform.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        timeToAttack = 0;
    }

    public override void FixedUpdate()
    {
        Attack();
        StillAttacking();

        if(attacking) 
            Mover.Move(rigidbody, transform.forward, spikeBallController.linearSpeed);
        else 
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Mover.Move(rigidbody, direction, spikeBallController.linearSpeed);
            Mover.Rotate(rigidbody, direction, spikeBallController.angularSpeed);
        }
    }

    private void StillAttacking()
    {
        if (attacking) 
        {
            rotationStopTime -= Time.deltaTime;
            attacking = rotationStopTime > 0;
        }
    }

    private void Attack()
    {
        timeToAttack -= Time.deltaTime;
        float distancePlayerEnemy = Vector3.Distance(player.transform.position, transform.position);
        if (combo == 0 && distancePlayerEnemy < 2f)
        {
            if (timeToAttack < 0)
            {
                RandomAttack();
            }
        }
        //combo
        else if (combo != 0 && distancePlayerEnemy < 3f)
        {
            if (combo < 3)
            {
                if (timeToAttack < 0)
                {
                    RandomAttack();
                }
            }
            else 
            {
                combo = 0;
                timeToAttack = spikeBallController.comboCooldown;
            }  
        } 
    }

    private void RandomAttack()
    {
        if (Random.value >= 0.33f)
        {
            ++combo;
            animator.SetTrigger("Attack1");
            timeToAttack = spikeBallController.attack1Cooldown;    
            rotationStopTime = 1f;
            attackAudio.PlayDelayed(0.1f);
        }
        else
        {
            combo = 0;
            animator.SetTrigger("Attack2");
            timeToAttack = spikeBallController.attack2Cooldown;
            rotationStopTime = 1.3f;
            attackAudio2.Play();
        }
        attacking = true;
    }

    public override void TakeDamage(float damage)
    {
        rigidbody.velocity *= 0.25f;
        base.TakeDamage(damage);
    }
}
