using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightDefendState : DogKnightState
{
    private Animator animator;
    private Rigidbody rigidbody;
    private Transform transform;
    private GameObject player;
    private GameObject shield;
    private Material shieldMaterial;
    private Color shieldOriginalColour;
    private float actualTimeDefending;
    private bool shieldUp = false;
    private bool shieldBash = false;
    private AudioSource slashAudio;
    private AudioSource defendAudio;

    public DogKnightDefendState(DogKnightController controller) : base(controller)
    {
        rigidbody = controller.GetComponent<Rigidbody>();
        animator = controller.GetComponent<Animator>();
        transform = controller.transform;
        player = controller.player;

        Transform pelvisTransform = controller.transform.Find("root/pelvis");
        shield = pelvisTransform.Find("Shield").gameObject;
        shieldMaterial = shield.transform.Find("ShieldPolyart").GetComponent<Renderer>().material;
        shieldOriginalColour = shieldMaterial.color;

        slashAudio = pelvisTransform.Find("Weapon").GetComponent<AudioSource>();
        defendAudio = shield.GetComponent<AudioSource>();
    }
   
    public override void Start()
    {
        Animator animator = dogKnightController.GetComponent<Animator>();
        animator.SetTrigger("Defend");
        actualTimeDefending = dogKnightController.timeDefending;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        Mover.Rotate(rigidbody, direction, dogKnightController.angularSpeed);

        if (!shieldBash)
        {
            checkIfStopDefending();
        }
    }


    public override void TakeDamage(float damage)
    {
        if (!shieldUp)
        {
            base.TakeDamage(damage);
        }
        else if (!shieldBash)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Mover.Move(rigidbody, direction, dogKnightController.linearSpeed*2);

            shieldBash = true;
            animator.SetTrigger("ShieldBash");
            defendAudio.Play();
            slashAudio.PlayDelayed(0.1f);
        } 
    }

    private void checkIfStopDefending()
    {
        actualTimeDefending -= Time.deltaTime;
        if (actualTimeDefending <= 0)
        {
            animator.SetTrigger("StopDefense");
            Exit();
        }
    }

    public override void StartDefending()
    {
        shieldUp = true;
        rigidbody.velocity = Vector3.zero;
        shieldMaterial.color += Color.green; 
    }

    public override void Exit()
    {
        shieldMaterial.color = shieldOriginalColour;
        Animator animator = dogKnightController.GetComponent<Animator>();

        DogKnightState newState = new DogKnightActiveState(dogKnightController);
        dogKnightController.ChangeState(newState);
    }
}
