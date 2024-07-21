using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightActiveState : DogKnightState
{
    private float timeToAttack;
    private Animator animator;
    private Rigidbody rigidbody;
    private Transform transform;
    private GameObject player;
    private int combo;
    private bool swordAttack;
    private float rotationStopTime;
    private AudioSource slashAudio;

    public DogKnightActiveState(DogKnightController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();
        transform = controller.transform;
        player = controller.player;
        slashAudio = controller.transform.Find("root/pelvis/Weapon").GetComponent<AudioSource>();
    }

    public override void Start()
    {
        timeToAttack = 0;
        combo = 0;
        swordAttack = false;
        rotationStopTime = 0;
    }

    public override void FixedUpdate()
    {
        Attack();
        StillAttacking();

        if (!swordAttack) 
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Mover.Move(rigidbody, direction, dogKnightController.linearSpeed);
            Mover.Rotate(rigidbody, direction, dogKnightController.angularSpeed);
        }
    }

    private void StillAttacking()
    {
        if (swordAttack) 
        {
            rotationStopTime -= Time.deltaTime;
            swordAttack = rotationStopTime > 0;
        }
    }

    private void Attack()
    {
        timeToAttack -= Time.deltaTime;
        float distancePlayerEnemy = Vector3.Distance(player.transform.position, transform.position);
        if (combo == 0)
        {
            if (timeToAttack < 0 && distancePlayerEnemy < 2.5f)
            {
                RandomAttack();
            }
        }
        //combo
        else
        {
            if (combo < 2)
            {
                if (timeToAttack < 0)
                {
                    RandomAttack();
                }
            }
            else 
            {
                combo = 0;
                timeToAttack = dogKnightController.comboCooldown;
            }  
        } 
    }

    private void RandomAttack()
    {
        if (Random.value >= 0.4f)
        {
            animator.SetTrigger("SwordAttack");
            ++combo;
            timeToAttack = dogKnightController.swordAttackCooldown;    
            rotationStopTime = 0.3f;
            rigidbody.velocity = Vector3.zero;
            swordAttack = true;
            slashAudio.PlayDelayed(0.2f);
        }
        else
        {
            DogKnightState newState = new DogKnightDefendState(dogKnightController);
            dogKnightController.ChangeState(newState);
        }
    }

    public override void TakeDamage(float damage)
    {
        rigidbody.velocity *= 0.25f;
        base.TakeDamage(damage);
    }
}
