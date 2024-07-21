using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public int initialRoom = 2;
    public GameObject cam;
    public GameObject player;
    public GameObject backgroundMusic;
    public AudioSource music;
    private int actualRoomPos;
    public GameObject actualRoom;
    private int roomSizex = 20;
    private int roomSizey = 12;
    private float distanceFromDoor = 1.5f;
    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = cam.GetComponent<CameraController>();
        music = backgroundMusic.GetComponent<AudioSource>();

        actualRoomPos = initialRoom;
        ResetEnemiesRoom();
        CountEnemiesInRoom();
    }

    public void ChangeRoom(GameObject room1, GameObject room2, int posRoom1, int posRoom2)
    {
        DespawnEnemiesLastRoom();

        int direction;
        if(posRoom1 == actualRoomPos)
        {
            direction = posRoom2 - actualRoomPos;
            actualRoom = room2;
            actualRoomPos = posRoom2; 
        }
        else 
        {
            direction = posRoom1 - actualRoomPos;
            actualRoom = room1;
            actualRoomPos = posRoom1;
        }

        ResetEnemiesRoom();
        CountEnemiesInRoom();
        CheckDarkRoom();
        
        //room down left corner
        int roomStartx = roomSizex*(actualRoomPos%5);
        int roomStarty = roomSizey*(actualRoomPos/5);

        if (direction == 1) 
            player.transform.position = new Vector3(roomStartx + distanceFromDoor, 0, roomStarty + roomSizey/2);
        else if (direction == -1) 
            player.transform.position = new Vector3(roomStartx + roomSizex - distanceFromDoor, 0, roomStarty + roomSizey/2);
        else if (direction < -1) 
            player.transform.position = new Vector3(roomStartx + roomSizex/2, 0, roomStarty + roomSizey - distanceFromDoor);
        else 
            player.transform.position = new Vector3(roomStartx + roomSizex/2, 0, roomStarty + distanceFromDoor);

        StopTimeScale();

        Vector3 destination = new Vector3(roomSizex/2 + roomStartx, cam.transform.position.y, roomStarty - 1);
        cameraController.ChangeRoom(destination);

        if (actualRoom.tag == "BossRoom")
        {
            CloseBossRoom(room1);
            ChangeMusic();
        }
            
    }

    private void DespawnEnemiesLastRoom()
    {
        for (int i = 0; i < actualRoom.transform.childCount; i++) 
        {
            GameObject child = actualRoom.transform.GetChild(i).gameObject;
            if (child.tag == "Enemy" || child.tag == "Mimic")
            {
                child.SetActive(false);
            }
            else if(child.tag == "OrderPuzzle")
            {
                child.SetActive(false);
            }
        }
    }

    private void ResetEnemiesRoom()
    {
        for (int i = 0; i < actualRoom.transform.childCount; i++) 
        {
            GameObject child = actualRoom.transform.GetChild(i).gameObject;
            if (child.tag == "Enemy" || child.tag == "Mimic")
            {
                child.GetComponent<SpawnController>().ResetEnemy();
            }
            else if (child.tag == "Pushable")
            {
                child.GetComponent<PushableObject>().RestartPosition();
                child.SetActive(true);
            }
            else if(child.tag == "OrderPuzzle")
            {
                child.GetComponent<OrderPuzzle>().RestartEnemies();
            }
        }
    }

    private void CountEnemiesInRoom()
    {
        for (int i = 0; i < actualRoom.transform.childCount; i++) 
        {
            GameObject child = actualRoom.transform.GetChild(i).gameObject;
            if (child.tag == "Door")
                child.GetComponent<DoorInfo>().CountEnemies();
        }
    }

    private void CloseBossRoom(GameObject room1)
    {
        Transform door = room1.transform.Find("DoorKeyBoss");
        door.GetComponent<DoorInfo>().unlocked = false;
        door.Find("rotationPoint").gameObject.SetActive(false);
    }

    private void CheckDarkRoom()
    {
        player.GetComponent<PlayerController>().SwitchLight(actualRoom.tag == "DarkRoom");
    }

    public void StopTimeScale()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        SceneManager.LoadScene(3);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeMusic()
    {
        AudioSource tempMusic = actualRoom.GetComponent<AudioSource>();
        if (music.isPlaying)
        {
            music.Stop();
            tempMusic.Play();
        }
        music = tempMusic;
    }
}
