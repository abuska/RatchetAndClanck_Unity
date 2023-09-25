using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPImage : MonoBehaviour
{
    GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        //set XPIMAGE width to 0
        transform.localScale = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //set  width of XPIMAGE according to the maximum experience and the current player experience
        float playerXP = (float)player.GetComponent<PlayerStats>().GetPlayerExperience();
        int playerLevel = player.GetComponent<PlayerStats>().playerLevel;
        float levelXPLimit = (float)player.GetComponent<PlayerStats>().playerLevelExperience[playerLevel];
        float percentage = (playerXP/ levelXPLimit);
        transform.localScale = new Vector2(3f * percentage, 0.1f);
    }
}
