using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDUpdate : MonoBehaviour
{
    private const string HEARTS_GEMS_AND_KEYS_PATH = "UpperHUD/Hearts, gems & keys";
    private const string GEMS_AND_KEYS_PATH = HEARTS_GEMS_AND_KEYS_PATH + "/Gems & keys";
    public GameObject keyHUD;
    public GameObject bossKeyHUD;
    public GameObject fullHeartHUD;
    public GameObject halfHeartHUD;
    public GameObject emptyHeartHUD;
    public GameObject boomerangHUD;
    private TextMeshProUGUI gemNumInputText;
    private Transform transformGemsAndKeys;
    private Transform transformHearts;
    private Transform transformBoomerang;
    private GameObject[] heartsHUD;

    // Start is called before the first frame update
    void Start()
    {
        transformGemsAndKeys = transform.Find(GEMS_AND_KEYS_PATH);
        GameObject gemNum = transformGemsAndKeys.Find("Gem Quantity").gameObject;
        gemNumInputText = gemNum.GetComponent<TextMeshProUGUI>();
        transformHearts = transform.Find($"{HEARTS_GEMS_AND_KEYS_PATH}/Hearts");
        transformBoomerang = transform.Find("UpperHUD/Objects/Boomerang");
        heartsHUD = new GameObject[]
        {
            emptyHeartHUD, halfHeartHUD, fullHeartHUD
        };
    }

    public void UpdateHearts(int health)
    {
        foreach (Transform heart in transformHearts)
        {
            Destroy(heart.gameObject);
        }
        for (int i = 0; i < 3; ++i)
        {
            int hUDIndex = Mathf.Clamp(health - 2*i, 0, 2);
            Instantiate(heartsHUD[hUDIndex], transformHearts);
        }
    }

    public void UpdateGemsText(int newGemNum)
    {
        gemNumInputText.SetText(newGemNum.ToString());
    }

    public void AddKey(bool boss)
    {
        if (!boss)
        {
            Instantiate(keyHUD, transformGemsAndKeys);
        }
        else
        {
            Instantiate(bossKeyHUD, transformGemsAndKeys);
        }
    }

    public void DeleteKeys(int numKeys)
    {
        for (int i = 1; i <= numKeys; i++)
        {
            int lastKeyNumber = transformGemsAndKeys.childCount - i;
            Transform lastKeyTransform = transformGemsAndKeys.GetChild(lastKeyNumber);
            Destroy(lastKeyTransform.gameObject);
        }
    }

    public void AddBoomerang()
    {
        Instantiate(boomerangHUD, transformBoomerang.parent);
        Destroy(transformBoomerang.gameObject);
    }
}
