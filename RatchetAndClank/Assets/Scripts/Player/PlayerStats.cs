using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private GameObject player;

    private int playerScore = 0;

    public int playerBolts = 0;

    private int playerLevel = 1;

    private int playerExperience = 0;

    // - Initial - //
    /**********************************************************************************************/
    // Start is called before the first frame update
    // set the max health of the player and the current health of the player
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    // - Health - //
    /**********************************************************************************************/
    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return 3; //maxHealth;
    }

    // Check the player is can pickup the health item
    public bool CanPickupHealthItem(int PointsRestored)
    {
        return currentHealth + PointsRestored <= maxHealth;
    }

    // Increase the health of the player
    public void IncreaseHealth(int value)
    {
        currentHealth = currentHealth + value;
    }

    // Take damage and logic for armor
    // if armor is 100% then first damage decrease armor by 20%
    // if armor is 0% then damage goes to health
    public void TakeDamage(int damage)
    {
        if (currentArmor <= 0)
        {
            // take damage without armor
            currentHealth -= damage;
        }
        else
        {
            // take damage with armor
            float damageReduction =
                (float) currentArmor / 100.0f * (float) damage;
            float floatDamage = (float) damage - damageReduction;

            currentHealth -= (int) Mathf.Ceil(floatDamage) / 2;

            //armor damage calculates here
            if (currentArmor != 100)
            {
                currentArmor -= (int) Mathf.Ceil(floatDamage);
                if (currentArmor <= 0)
                {
                    currentArmor = 0;
                }
            }
            else
            {
                //damage calculation, on 100 armor = 0 damage
                //on first damage decrease armor by 20%
                currentArmor -= 20;
            }
        }

        // animatorHandler.PlayTargetAnimation("Damage01",true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // animatorHandler.PlayTargetAnimation("Death01",true);
            // handle death
            // get the game manager and get the player and play the die sound
            // and call the game over function
            isDead = true;
            //player.GetComponent<PlayerMovementScript>().PlayDieSound();
        }
        else
        {
            // get the player and play the damage sound
            /* player
                .GetComponent<PlayerMovementScript>()
                .PlayDamageSound();*/
        }
    }

    // Disable the player scripts
    public void disablePlayerScript()
    {
        player.GetComponent<GunInventory>().enabled = false;
        /*  player
            .GetComponent<GunInventory>()
            .currentGun
            .GetComponent<GunScript>()
            .enabled = false;*/
        // player.GetComponent<PlayerMovementScript>().enabled = false;
    }

    //Enable the player scripts
    public void enablePlayerScript()
    {
        player.GetComponent<GunInventory>().enabled = true;
        /*player
            .GetComponent<GunInventory>()
            .currentGun
            .GetComponent<GunScript>()
            .enabled = true;*/
        // player.GetComponent<PlayerMovementScript>().enabled = true;
    }

    //- Bolts -//
    /**********************************************************************************************/
    // Get the bolts of the player
    public int GetPlayerBolts()
    {
        return playerBolts;
    }

    public void IncreaseBolts(int _playerBolts)
    {
        playerBolts += _playerBolts;
    }

    public void DecreaseBolts(int _playerBolts)
    {
        if ((playerBolts -= _playerBolts) >= 0)
        {
            playerBolts -= _playerBolts;
        }
        else
        {
            playerBolts = 0;
        }
    }

    // - Armor - //
    /**********************************************************************************************/
    // Check the player is can pickup the armor item
    public bool CanPickupArmorItem(int PointsRestored, string ArmorType)
    {
        if (currentArmor < 100)
        {
            return true;
        }
        return false;
    }

    // Increase the armor of the player
    public void IncreaseArmor(int PointsRestored, string ArmorType)
    {
        currentArmor += PointsRestored;
    }

    // - Score - //
    /**********************************************************************************************/
    // Get the score of the player
    public int GetPlayerScore()
    {
        return playerScore;
    }

    // Set the score of the player
    public void SetPlayerScore(int _playerScore)
    {
        playerScore = _playerScore;
    }

    // Increase the level of the player
    public void IncreasePlayerLevel(int _playerLevel)
    {
        playerLevel += _playerLevel;
    }

    // Get the level of the player
    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    // Increase the experience of the player
    public void IncreasePlayerExperience(int _playerExperience)
    {
        playerExperience += _playerExperience;
    }

    // Get the experience of the player
    public int GetPlayerExperience()
    {
        return playerExperience;
    }
}
