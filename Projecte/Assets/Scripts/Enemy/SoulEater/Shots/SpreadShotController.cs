using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotController : EnemyController
{
    public float speed = 5f;
    public float time = 1f;
    private GameObject player;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    override public void Start()
    {
        player = GameObject.Find("Player");
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        Mover.Move(rigidbody, direction, speed);
    }

    void Update()
    {
        lastPosition = transform.position;
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Split();
        }
    }

    private void Split()
    {
        Destroy(gameObject);

        Transform parent = transform.parent;
        parent.position = transform.position;

        float angle = 0;
        foreach (Transform child in parent)
        {
            if (child != transform)
            {
                child.gameObject.SetActive(true);

                Vector3 newPosition = child.transform.position;
                //por alguna razon aqui no encuentra la variable player inicializada previamente si no es con este find, 
                // y tambien independientemente de donde se ponga el destroy del tiro inicial
                newPosition.y = GameObject.Find("Player").transform.position.y + child.GetComponent<SphereCollider>().radius;
                child.transform.position = newPosition;

                Vector3 direction = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                Mover.Move(child.GetComponent<Rigidbody>(), direction, speed);

                angle += 2f * Mathf.PI / 5f;

            }
        }

        AudioSource splitAudio = parent.GetComponent<AudioSource>();
        splitAudio.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        Split();
    }
}
