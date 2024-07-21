using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackingSwordState : PlayerAttackingState
{
    private GameObject trail;
    private TrailRenderer trailRenderer;
    private AudioSource slashAudio;

    public PlayerAttackingSwordState(PlayerController controller) : base(controller)
    {
        trail = controller.transform.Find($"{PlayerController.WEAPON_PATH}/Trail").gameObject;
        trailRenderer = trail.GetComponent<TrailRenderer>();
        slashAudio = trail.GetComponent<AudioSource>();
    }

    public override void TakeDamage(int damage)
    {
        trail.SetActive(false);
        base.TakeDamage(damage);
    }

    public override void Attack(int inProgress)
    {
        trail.SetActive(inProgress == 1);
        if (inProgress == 1)
        {
            slashAudio.Play();
        }

    }

    public override void Exit()
    {
        trail.SetActive(false);
        base.Exit();
    }
}
