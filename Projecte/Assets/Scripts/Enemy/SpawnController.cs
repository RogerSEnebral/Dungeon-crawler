using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialHealth;
    private int initialDamage;
    private Material originalMat;
    private Color originalColour;
    private EnemyController controller;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        controller = GetComponent<EnemyController>();
        initialHealth = controller.health;
        initialDamage = controller.damage;
        
        originalMat = transform.Find("Mesh").GetComponent<Renderer>().material;
        originalColour = originalMat.color;

        gameObject.SetActive(false);
    }

    public void ResetEnemy()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        controller.health = initialHealth;
        controller.damage = initialDamage;
        transform.Find("Mesh").GetComponent<Renderer>().material.color = originalColour;

        controller.Start();
        gameObject.SetActive(true);
    }
}
