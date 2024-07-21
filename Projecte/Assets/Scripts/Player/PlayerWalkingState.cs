using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWalkingState : PlayerActiveState
{
    private Rigidbody rigidbody;
    private Animator animator;
    private AudioSource footstepsAudio;

    public PlayerWalkingState(PlayerController controller) : base(controller)
    {
       rigidbody = controller.GetComponent<Rigidbody>();
       animator = controller.GetComponent<Animator>();
       footstepsAudio = controller.transform.Find("root/pelvis").GetComponent<AudioSource>();
    }

    public override void Start()
    {
        footstepsAudio.Play();
    }

    public override void FixedUpdate(int linearSpeed, float angularSpeed)
    {
        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalDirection, 0, verticalDirection);
        direction.Normalize();

        if (direction.magnitude != 0)
        {
            Mover.Move(rigidbody, direction, linearSpeed);
            Mover.Rotate(rigidbody, direction, angularSpeed);
            animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        }
        else
        {
            PlayerState newState = new PlayerIdleState(playerController);
            ChangeState(newState);
        }
    }

    public override void ChangeState(PlayerState newState)
    {
        Stop();
        base.ChangeState(newState);
    }

    private void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        animator.SetFloat("Speed", 0);
        footstepsAudio.Stop();
    }
}
