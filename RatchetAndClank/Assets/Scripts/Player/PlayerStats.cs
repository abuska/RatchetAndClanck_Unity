using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : CharacterStats
{
    private GameObject player;

    private int playerScore = 0;

    public int playerBolts = 0;

    public int playerMaxHealth = 3;

    public int playerDefaultMaxHealth = 3;

    public int playerCurrentHealth = 1;

    public int playerLevel = 1;

    private int[]
        playerLevelExperience = { 0, 30, 90, 180, 300, 450, 630, 840, 1080, 1350 };

    private int playerExperience = 0;

    private float _damageCoolDown = 1f;

    private float _damageCoolDownTimer = Mathf.Infinity;

    public GameObject _uiBoltsTextComponent;
    public GameObject _uiHealthTextComponent;

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
        playerCurrentHealth = playerMaxHealth;
        CalculateMaxHealth();
        _uiBoltsTextComponent.GetComponent<TextMeshProUGUI>().text= playerBolts.ToString();
        _uiHealthTextComponent.GetComponent<TextMeshProUGUI>().text = playerCurrentHealth.ToString();
    }

    public void Update()
    {
        _uiBoltsTextComponent.GetComponent<TextMeshProUGUI>().text = playerBolts.ToString();
        _uiHealthTextComponent.GetComponent<TextMeshProUGUI>().text = playerCurrentHealth.ToString();
    }

    // - Health - //
    /**********************************************************************************************/
    // Check the player is can pickup the health item
    public bool CanPickupHealthItem(int PointsRestored)
    {
        return playerCurrentHealth + PointsRestored <= maxHealth;
    }

    // Increase the health of the player
    public void IncreaseHealth(int value)
    {
        playerCurrentHealth = playerCurrentHealth + value;
    }

    public void IncreaseMaxHealth(int value)
    {
        playerMaxHealth = playerMaxHealth + value;
    }

    public void CalculateMaxHealth()
    {
        playerMaxHealth = 3 + playerLevel;
    }

    public void DecreaseHealth(int value)
    {
        if (_damageCoolDownTimer >= _damageCoolDown)
        {
            _damageCoolDownTimer = 0f;
            playerCurrentHealth = playerCurrentHealth - value;
        }
        _damageCoolDownTimer += Time.deltaTime * 100;
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
        CalculateMaxHealth();
        playerCurrentHealth = playerMaxHealth;
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
        if (
            playerLevel < (playerLevelExperience.Length - 1) &&
            (playerExperience + _playerExperience) >=
            playerLevelExperience[playerLevel]
        )
        {
            IncreasePlayerLevel(1);
        }
    }

    // Get the experience of the player
    public int GetPlayerExperience()
    {
        return playerExperience;
    }
}
