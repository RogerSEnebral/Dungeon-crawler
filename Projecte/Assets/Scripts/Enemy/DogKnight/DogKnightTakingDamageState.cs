using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightTakingDamageState : DogKnightState
{
    private Material material;
    private Color originalColour;

    public DogKnightTakingDamageState(DogKnightController controller) : base(controller)
    {
        material = controller.transform.Find("Mesh").GetComponent<Renderer>().material;
        originalColour = material.color;
    }

    public override void Start()
    {
        material.color = originalColour + Color.red;
        if (dogKnightController.health <= 0)
        {
            DogKnightState newState = new DogKnightDyingState(dogKnightController);
            dogKnightController.ChangeState(newState);
        }
        else
        {
            Animator animator = dogKnightController.GetComponent<Animator>();
            animator.SetTrigger("Damage");
            
            AudioSource hitAudio = dogKnightController.GetComponent<AudioSource>();
            hitAudio.Play();
        }
    }
 
    public override void TakeDamage(float damage)
    {}

    public override void Exit()
    {
        material.color = originalColour;
        DogKnightState newState = new DogKnightActiveState(dogKnightController);
        dogKnightController.ChangeState(newState);
    }
}
