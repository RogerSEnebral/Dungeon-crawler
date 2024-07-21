using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject returnButton;
    private AudioSource backAudio;

    // Start is called before the first frame update
    void Start()
    {
        backAudio = GetComponent<AudioSource>();
    }

    public void ReturnToMainMenu()
    {
        backAudio.Play();
        SceneManager.LoadScene(0);
    }
}
