using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyAnimatorManager enemyAnimatorManager;

    [SerializeField]
    public int attackDamage = 10;

    [SerializeField]
    public float attackCooldown = 3f;

    [SerializeField]
    public float movementSpeed = 3f;

    [SerializeField]
    public float sightRange = 10f;

    [SerializeField]
    public float patrolRange = 5f;

    [SerializeField]
    public float attackRange = 1.5f;

    [SerializeField]
    public string EnemyType = "Melee";
    public float fireballSpeed = 10f;

    private void Awake()
    {
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            //todo enemy armor
            currentHealth = currentHealth - damage;
            Debug.Log("Enemy Health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Enemy Died");
                currentHealth = 0;
                enemyAnimatorManager.PlayDeath();
                isDead = true;
            }
            else
            {
                Debug.Log("Enemy Damaged");
                enemyAnimatorManager.PlayDamage();
            }
        }
    }
  
}
