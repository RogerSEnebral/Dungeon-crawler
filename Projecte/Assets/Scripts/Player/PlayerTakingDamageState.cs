using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTakingDamageState : PlayerState
{
    private const string HEAD_PATH = "root/pelvis/spine_01/spine_02/spine_03/neck_01/head";
    private Material[] materials;
    private Color originalColour;
    private AudioSource hitAudio;

    public PlayerTakingDamageState(PlayerController controller) : base(controller)
    {
        string[] materialPaths = new string[]
        {
            "Body05", $"{HEAD_PATH}/Hair01", $"{HEAD_PATH}/Head01_Male"
        };
        materials = materialPaths.Select(GetMaterial).ToArray();
        originalColour = materials[0].color;
        hitAudio = controller.transform.Find("Body05").GetComponent<AudioSource>();
    }

    public override void Start()
    {
        Animator animator = playerController.GetComponent<Animator>();
        animator.SetTrigger("Damage");

        foreach (Material material in materials)
        {
            material.color = originalColour + Color.red;
        }

        hitAudio.Play();
    }

    public override void TakeDamage(int damage)
    {}

    public override void Exit()
    {
        foreach (Material material in materials)
        {
            material.color = originalColour;
        }
        if (playerController.health > 0)
        {
            PlayerState newState = new PlayerIdleState(playerController);
            ChangeState(newState);
        }
        else
        {
            PlayerState newState = new PlayerDyingState(playerController);
            ChangeState(newState);
        }
    }

    private Material GetMaterial(string path)
    {
        Transform playerPart = playerController.transform.Find(path);
        Renderer renderer = playerPart.GetComponent<Renderer>();
        return renderer.material;
    }
}
