using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoulEaterState : StateMachineBehaviour
{
    private Animator animator;
    protected SoulEaterController controller
    {
        get;
        private set;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;

        controller = animator.gameObject.GetComponent<SoulEaterController>();
        controller.ChangeState(this);
    }

    virtual public void TakeDamage(float damage)
    {
        int phase = animator.GetInteger("Phase"); 

        if (phase == 3 || (phase == 1 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Shooting")))
        {
            controller.actualDamageUntilStagger -= damage;
            if (controller.actualDamageUntilStagger < 0)
            {
                animator.SetFloat("Hit", damage);
                controller.actualDamageUntilStagger = controller.damageUntilStagger;
            }
            else
            {
                LoseHealth(damage);
            }

            AudioSource hitAudio = controller.GetComponent<AudioSource>();
            hitAudio.Play();
        }
        else if (phase == 1)
            animator.SetFloat("Hit", damage);
    }

    virtual public void Shoot()
    {}

    virtual public void Bite()
    {}

    virtual public void TailAttack()
    {}

    virtual public void StopCharge()
    {}

    virtual public void LoseHealth(float damage)
    {
        Transform meshTransform = controller.transform.Find("Mesh");
        Renderer renderer = meshTransform.GetComponent<Renderer>();
        controller.material = renderer.material;
        controller.originalColour = controller.material.color;
        controller.material.color += Color.red;

        controller.health -= damage;
        animator.SetFloat("Health", controller.health);

        controller.Invoke("ReturnToNormalColor", 0.3f);
    }
}
