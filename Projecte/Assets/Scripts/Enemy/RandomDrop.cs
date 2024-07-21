using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public GameObject gem;
    public GameObject heart;
    public float dropRate = 0.3f;
    public float gemToHeartRate = 0.6f;
    private GameObject gemInstance;
    private BoxCollider gemCollider;
    private GameObject heartInstance;
    private BoxCollider heartCollider;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 eulerAngles = new Vector3(-90, 0, 0);

        gemInstance = Instantiate(gem);
        gemInstance.transform.eulerAngles = eulerAngles;
        gemCollider = gemInstance.GetComponent<BoxCollider>();
        gemInstance.SetActive(false);

        heartInstance = Instantiate(heart);
        heartInstance.transform.eulerAngles = eulerAngles;
        heartCollider = heartInstance.GetComponent<BoxCollider>();
        heartInstance.SetActive(false);
    }

    public void Drop()
    {
        if (Random.value <= dropRate)
        {
            GameObject toDrop = null;
            BoxCollider toDropCollider = null;
            if (Random.value <= gemToHeartRate)
            {
                toDrop = gemInstance;
                toDropCollider = gemCollider;
            }
            else
            {
                toDrop = heartInstance;
                toDropCollider = heartCollider;
            }
            toDrop.SetActive(true);
            
            float positionY = transform.position.y + toDropCollider.size.z*toDrop.transform.localScale.z/2f;
            Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);
            toDrop.transform.position = position;
        }
    }
}
