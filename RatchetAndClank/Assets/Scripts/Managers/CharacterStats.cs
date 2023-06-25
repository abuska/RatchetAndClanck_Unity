using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // - Health -
    [SerializeField]
    public int healthLevel = 10;
    protected int maxHealth;
    protected int currentHealth;
    protected bool isDead = false;

    public GameObject LastSeenEnemy;

    // - Armor -
    [SerializeField]
    public int armorLevel = 10;
    protected int maxArmor;
    protected int currentArmor;

    

    // Set the last seen enemy
    public void SetLastSeenEnemy(GameObject enemy)
    {
        LastSeenEnemy = enemy;
    }

    // Get the last seen enemy
    public GameObject GetLastSeenEnemy()
    {
        return LastSeenEnemy;
    }

    /**********************************************************************************************/
    // - Health -
    
    // Get the max health of the character
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    // Set current health of the character
    public void SetCurrentHealth(int newCurrent)
    {
        currentHealth = newCurrent;
    }

    // Get current health of the character
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    // Get the isDead the character
      public bool GetIsDead()
    {
        return isDead;
    }
    // Set the isDead the character
      public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    /**********************************************************************************************/
    // - Armor -

    // Get the max armor of the character
    public int GetMaxArmor()
    {
        return maxArmor;
    }

    // Set the current armor of the character
    public void SetCurrentArmor(int newCurrent)
    {
        currentArmor = newCurrent;
    }

    // Get the current armor of the character
    public int GetCurrentArmor()
    {
        return currentArmor;
    }
}
