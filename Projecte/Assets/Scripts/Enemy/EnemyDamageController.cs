using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject collidedObject = collider.gameObject; 
        if (collidedObject.tag == "Weapon")
        {
            WeaponProperties weaponProperties = collidedObject.GetComponent<WeaponProperties>();
            enemyController.TakeDamage(weaponProperties.damage);
        }
    }
}
