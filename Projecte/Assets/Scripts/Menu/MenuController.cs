using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject mainMenu, instructionsMenu, creditsMenu;
    private AudioSource acceptAudio;
    private AudioSource selectAudio;
    private AudioSource backAudio;

    // Start is called before the first frame update
    void Start()
    {
        acceptAudio = GetAudio("accept");
        selectAudio = GetAudio("select");
        backAudio = GetAudio("back");

        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GoBack()
    {
        backAudio.Play();
        OpenMainMenu();
    }

    public void OpenInstructions()
    {
        acceptAudio.Play();
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        acceptAudio.Play();
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void StartGame()
    {
        acceptAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
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
