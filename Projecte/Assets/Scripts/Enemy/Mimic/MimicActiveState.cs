using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicActiveState : MimicState
{
    private float timeToAttack;
    private Animator animator;
    private Rigidbody rigidbody;
    private Transform transform;
    private GameObject player;
    private bool biting = false;
    private float rotationStopTime;
    private AudioSource biteAudio;

    public MimicActiveState(MimicController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();
        transform = controller.transform;
        player = controller.player;

        Transform teethTransform = mimicController.transform.Find("Root/Body/LowerTeethCTRL");
        biteAudio = teethTransform.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        timeToAttack = 0.5f;
    }

    public override void FixedUpdate()
    {
        Attack();
        StillAttacking();

        if (biting) 
        {
            Mover.Move(rigidbody, transform.forward, mimicController.linearSpeed*2f);
        }
        else 
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Mover.Move(rigidbody, direction, mimicController.linearSpeed);
            Mover.Rotate(rigidbody, direction, mimicController.angularSpeed);
        }
    }

    private void StillAttacking()
    {
        if (biting)
        {
            rotationStopTime -= Time.deltaTime;
            biting = rotationStopTime > 0;
        }
    }

    private void Attack()
    {
        timeToAttack -= Time.deltaTime;
        float distancePlayerEnemy = Vector3.Distance(player.transform.position, transform.position);
        if (distancePlayerEnemy < 5f)
        {
            if (timeToAttack < 0)
            {
                RandomAttack();
            }
        }
    }

    private void RandomAttack()
    {
        if (Random.value >= 0.25f)
        {
            animator.SetTrigger("Bite");
            biteAudio.PlayDelayed(0.1f);
            timeToAttack = mimicController.attack1Cooldown;    
            rotationStopTime = mimicController.rotationStopTimeBite;
            biting = true;
        }
        else
        {
            mimicController.ChangeState(new MimicHeadbuttState(mimicController));
        }
    }

    public override void TakeDamage(float damage)
    {
        rigidbody.velocity *= 0.25f;
        base.TakeDamage(damage);
    }
}
