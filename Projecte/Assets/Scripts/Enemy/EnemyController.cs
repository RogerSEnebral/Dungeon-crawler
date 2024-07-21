using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage = 1;
    public float health = 3;

    virtual public void Start()
    {}

    virtual public void TakeDamage(float damage)
    {}
}
