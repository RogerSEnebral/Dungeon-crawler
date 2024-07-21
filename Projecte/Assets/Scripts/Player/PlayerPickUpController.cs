using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpController : MonoBehaviour
{
    public int purpleKeys;
    public int yellowKeys;
    public int gems;

    private HUDUpdate hUDUpdate;
    private BoomerangController boomerangController;
    private PlayerController playerController;

    private AudioSource keyGetAudio;
    private AudioSource gemAudio;
    private AudioSource heartAudio;
    private bool keyReady;

    // Start is called before the first frame update
    void Start()
    {
        yellowKeys = 0;
        gems = 0;
        keyReady = true;

        GameObject hUD = transform.Find("HUD").gameObject;
        hUDUpdate = hUD.GetComponent<HUDUpdate>();

        boomerangController = transform.Find("boomerang").GetComponent<BoomerangController>();

        playerController = GetComponent<PlayerController>();

        keyGetAudio = FindAudio("keyGet");
        gemAudio = FindAudio("gem");
        heartAudio = FindAudio("heart");
    }

    void Update()
    {
        if (keyReady)
        {
            if (Input.GetAxisRaw("Key") != 0)
            {
                NewYellowKey();
                keyReady = false;
            }
            else if (Input.GetAxisRaw("Boss Key") != 0)
            {
                NewPurpleKey();
                keyReady = false;
            }
            else if (Input.GetAxisRaw("Boomerang") != 0)
            {
                GetBoomerang();
                keyReady = false;
            }
        }
        keyReady = keyReady ||
            Input.GetAxisRaw("Key") == 0 && Input.GetAxisRaw("Boss Key") == 0 && Input.GetAxisRaw("Boomerang") == 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        bool found = true;
        GameObject collidedObject = collider.gameObject;
        switch (collidedObject.tag)
        {
            case "YellowKey":
                NewYellowKey();
                keyGetAudio.Play();
                break;

            case "PurpleKey":
                NewPurpleKey();
                keyGetAudio.Play();
                break;

            case "Gem":
                if (gems < 99)
                {
                    gems++;
                }
                hUDUpdate.UpdateGemsText(gems);
                gemAudio.Play();
                break;

            case "Heart":
                if (playerController.health < 6)
                {
                    playerController.health = Mathf.Min(6, playerController.health + 2);
                }
                heartAudio.Play();
                break;

            default:
                found = false;
                break;
        }
        collidedObject.SetActive(!found);
    }

    //indica si puede abrir la puerta y gasta las llaves correspondientes para ello
    public bool SpendKeys(int spentKeys, string keyType)
    {
        if (keyType.Equals("YellowKey"))
        {
            if (yellowKeys >= spentKeys)
            {
                yellowKeys -= spentKeys;
            }
            else
            {
                return false;
            }
        }
        else if (keyType.Equals("PurpleKey"))
        {
            if (purpleKeys >= spentKeys)
            {
                purpleKeys -= spentKeys;
            }
            else
            {
                return false;
            }
        }

        hUDUpdate.DeleteKeys(spentKeys);

        return true;
    }

    public void NewYellowKey()
    {
        yellowKeys++;
        hUDUpdate.AddKey(false);
    }

    public void NewPurpleKey()
    {
        purpleKeys++;
        hUDUpdate.AddKey(true);
    }

    public void GetBoomerang()
    {
        if (!boomerangController.IsPicked())
        {
            boomerangController.available = true;
            hUDUpdate.AddBoomerang();
        }
    }

    private AudioSource FindAudio(string path)
    {
        return transform.Find(path).GetComponent<AudioSource>();
    }
}
