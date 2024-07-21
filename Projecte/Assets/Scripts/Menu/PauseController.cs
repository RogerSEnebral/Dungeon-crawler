using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private static GameManager gameManager;
    public GameObject backgroundMusic;
    private bool pause;
    private bool pressed;
    private Image image;
    private TextMeshProUGUI text;
    private Transform menuTransform;
    private AudioSource acceptAudio;
    private AudioSource selectAudio;
    private AudioSource backAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        image = GetComponent<Image>();
        menuTransform = transform.Find("Menu");
        text = menuTransform.Find("MusicButton/Text (TMP)").GetComponent<TextMeshProUGUI>();
        acceptAudio = GetAudio("accept");
        selectAudio = GetAudio("select");
        backAudio = GetAudio("back");

        pause = false;
        pressed = false;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Pause") != 0)
        {
            if (!pressed)
            {
                OpenCloseMenu();
            }
        }
        else
        {
            pressed = false;
        }
    }

    public void OpenCloseMenu()
    {
        if (pause)
        {
            gameManager.ResumeTime();
            backAudio.Play();
        }
        else
        {
            gameManager.StopTimeScale();
            selectAudio.Play();
        }

        image.enabled = !pause;
        foreach (Transform child in menuTransform)
        {
            child.gameObject.SetActive(!pause);
        }

        pause = !pause;
        pressed = true;
    }

    public void PlayStopMusic()
    {
        if (gameManager.music.isPlaying)
        {
            gameManager.music.Stop();
            text.text = "Music On/<color=black>Off</color>";
        }
        else
        {
            gameManager.music.Play();
            text.text = "Music <color=black>On</color>/Off";
        }
    }

    public void QuitGame()
    {
        acceptAudio.Play();
        gameManager.ResumeTime();
        SceneManager.LoadScene(0);
    }

    public void Select()
    {
        selectAudio.Play();
    }

    private AudioSource GetAudio(string path)
    {
        return transform.Find(path).GetComponent<AudioSource>();
    }
}
