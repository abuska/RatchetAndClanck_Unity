using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainerDisplay : MonoBehaviour
{
 

    // Update is called once per frame
    //display the amount images the amount health the player has
    //by creating an array of child objects
    public int playerCurrentHealth = 3;
    void Update()
    {
        playerCurrentHealth = GameObject.Find("Player").GetComponent<PlayerStats>().playerCurrentHealth;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < playerCurrentHealth)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }   
        

         
        
    }
}
