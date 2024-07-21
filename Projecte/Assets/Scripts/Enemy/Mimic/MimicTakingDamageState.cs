using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicTakingDamageState : MimicState
{
    private Material material;
    private Color originalColour;

    public MimicTakingDamageState(MimicController controller) : base(controller)
    {
        material = controller.transform.Find("Mesh").GetComponent<Renderer>().material;
        originalColour = material.color;
    }

    public override void Start()
    {
        material.color = originalColour + Color.red;
        if (mimicController.health <= 0)
        {
            MimicState newState = new MimicDyingState(mimicController);
            mimicController.ChangeState(newState);
        }
        else
        {
            Animator animator = mimicController.GetComponent<Animator>();
            animator.SetTrigger("Damage");

            AudioSource hitAudio = mimicController.GetComponent<AudioSource>();
            hitAudio.Play();
        }
    }
 
    public override void TakeDamage(float damage)
    {}

    public override void OnCollisionEnter(Collision collision)
    {
        material.color = originalColour;
        base.OnCollisionEnter(collision);
    }

    public override void Exit()
    {
        material.color = originalColour;
        MimicState newState = new MimicActiveState(mimicController);
        mimicController.ChangeState(newState);
    }
}
