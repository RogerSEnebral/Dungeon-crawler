using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestContent {
    KEY, BOSS_KEY, BOOMERANG
}
public class ChestController : MonoBehaviour
{
    public ChestContent content;
    public bool opened {
        get;
        private set;
    } = false;
    private Animator animator;
    private AudioSource openChestAudio;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        openChestAudio = GetComponent<AudioSource>();
    }

    public virtual void Open()
    {
        animator.SetTrigger("Open");
        openChestAudio.PlayDelayed(0.5f);
        opened = true;
    }
}
