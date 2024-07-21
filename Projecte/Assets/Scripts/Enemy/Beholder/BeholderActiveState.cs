using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderActiveState : BeholderState
{
    private float timeToShoot;
    private Animator animator;
    private Rigidbody rigidbody;
    private Transform transform;
    private GameObject player;
    private CapsuleCollider playerCollider;
    private AudioSource shootingAudio;

    public BeholderActiveState(BeholderController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();
        transform = controller.transform;
        player = controller.player;
        playerCollider = player.GetComponent<CapsuleCollider>();
        shootingAudio = controller.transform.Find("Root/Body/EyeCTRL").GetComponent<AudioSource>();
    }

    public override void Start()
    {
        timeToShoot = 10.0f / beholderController.shootingRate;
    }

    public override void FixedUpdate(int linearSpeed, float angularSpeed, int minDistance)
    {
        StartShooting();
        Move(linearSpeed, angularSpeed, minDistance);
    }

    public override void Shoot(int shotSpeed, Vector3 shotOffset, GameObject shotModel)
    {
        Vector3 shotPosition = transform.position + transform.TransformVector(shotOffset);
        GameObject shot = GameObject.Instantiate(shotModel, shotPosition, transform.rotation);
        Rigidbody rigidbody = shot.GetComponent<Rigidbody>();
        SphereCollider shotCollider = shot.GetComponent<SphereCollider>();
        
        Vector3 direction = player.transform.position - shot.transform.position;
        direction.y += playerCollider.height - shotCollider.radius;
        direction.Normalize();
        Mover.Move(rigidbody, direction, shotSpeed);
    }

    private void StartShooting()
    {
        timeToShoot -= Time.deltaTime;
        if (timeToShoot < 0)
        {
            Start();
            animator.SetTrigger("Attack");
            shootingAudio.Play();
        }
    }

    public override void TakeDamage(float damage)
    {
        rigidbody.velocity = Vector3.zero;
        base.TakeDamage(damage);
    }

    private void Move(int linearSpeed, float angularSpeed, int minDistance)
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        if (direction.magnitude < minDistance)
        {
            direction.Normalize();
            Mover.Move(rigidbody, -direction, linearSpeed);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

        Mover.Rotate(rigidbody, direction, angularSpeed);
    }
}
