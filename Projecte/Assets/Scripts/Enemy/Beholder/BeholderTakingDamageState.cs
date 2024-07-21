using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderTakingDamageState : BeholderState
{
    private Material material;
    private Color originalColour;

    public BeholderTakingDamageState(BeholderController controller) : base(controller)
    {
        Transform meshTransform = controller.transform.Find("Mesh");
        material = meshTransform.GetComponent<Renderer>().material;
        originalColour = material.color;
    }

    public override void Start()
    {
        material.color = originalColour + Color.red;
        if (beholderController.health <= 0)
        {
            BeholderState newState = new BeholderDyingState(beholderController);
            beholderController.ChangeState(newState);
        }
        else
        {
            Animator animator = beholderController.GetComponent<Animator>();
            animator.SetTrigger("Damage");

            AudioSource hitAudio = beholderController.GetComponent<AudioSource>();
            hitAudio.Play();
        }
    }

    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        material.color = originalColour;
        BeholderState newState = new BeholderActiveState(beholderController);
        beholderController.ChangeState(newState);
    }
}
