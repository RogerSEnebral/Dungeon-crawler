using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
    public GameObject content;
    public GameObject destroyParticles;
    private GameObject instanceContent;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (content != null)
        {
            boxCollider = (BoxCollider)content.GetComponentInChildren(typeof(BoxCollider));
            instanceContent = Instantiate(content);
            instanceContent.transform.eulerAngles = new Vector3(-90, 0, 0);
            instanceContent.SetActive(false);
        }
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject collidedObject = collider.gameObject;
        if (collidedObject.tag == "Weapon")
        {
            if (content != null)
            {
                instanceContent.SetActive(true);
                float positionY = transform.position.y + boxCollider.size.z * instanceContent.transform.localScale.z / 2f;
                Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);
                instanceContent.transform.position = position;
            }

            Instantiate(destroyParticles, transform.position, transform.rotation);

            transform.Translate(Vector3.up * 10000);
            DisableObject();
        }
    }

    private IEnumerator DisableObject()
    {
        yield return 0;
        gameObject.SetActive(false);
    }
}
