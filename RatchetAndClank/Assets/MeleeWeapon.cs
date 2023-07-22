using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int attackDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject OtherGameObject = other.gameObject;
        if (OtherGameObject.CompareTag("Enemy"))
        {
            OtherGameObject.GetComponent<EnemyStats>().TakeDamage(attackDamage);
        }
    }
}
