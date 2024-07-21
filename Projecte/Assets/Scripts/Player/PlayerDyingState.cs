using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDyingState : PlayerState
{

    public PlayerDyingState(PlayerController controller) : base(controller)
    {}

    public override void Start()
    {
        Animator animator = playerController.GetComponent<Animator>();
        animator.SetTrigger("Death");

        AudioSource deathAudio = playerController.GetComponent<AudioSource>();
        deathAudio.Play();
    }

    public override void TakeDamage(int damage)
    {}

    public override void Exit()
    {
        playerController.gameManager.LoseGame();
    }
}
