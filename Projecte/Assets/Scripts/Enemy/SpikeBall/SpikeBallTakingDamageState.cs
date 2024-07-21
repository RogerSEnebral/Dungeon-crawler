using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTakingDamageState : SpikeBallState
{
    private Material material;
    private Color originalColour;

    public SpikeBallTakingDamageState(SpikeBallController controller) : base(controller)
    {
        material = controller.transform.Find("Mesh").GetComponent<Renderer>().material;
        originalColour = material.color;
    }

    public override void Start()
    {
        material.color = originalColour + Color.red;
        if (spikeBallController.health <= 0)
        {
            SpikeBallState newState = new SpikeBallDyingState(spikeBallController);
            spikeBallController.ChangeState(newState);
        }
        else
        {
            Animator animator = spikeBallController.GetComponent<Animator>();
            animator.SetTrigger("Damage");

            AudioSource hitAudio = spikeBallController.GetComponent<AudioSource>();
            hitAudio.Play();
        }
    }
 
    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        material.color = originalColour;
        SpikeBallState newState = new SpikeBallActiveState(spikeBallController);
        spikeBallController.ChangeState(newState);
    }
}
