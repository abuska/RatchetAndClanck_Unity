using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private static GameObject MainMenuUI;
    [SerializeField]
    private static GameObject GameMenuUI;
    [SerializeField]
    private static GameObject LoadMenuUI;
    [SerializeField]
    private static GameObject SaveMenuUI;


    private void Awake()
    {
        GameMenuUI = GameObject.Find("GameMenu");
        MainMenuUI = GameObject.Find("MainMenu");
        LoadMenuUI = GameObject.Find("LoadMenu");
        SaveMenuUI = GameObject.Find("SaveMenu");
        DisableGameMenu();
        DisableLoadMenu();
        DisableSaveMenu();
        
        DontDestroyOnLoad(gameObject);
    }
    
    // - Enable UI elements - //
    public void EnableMainMenu()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        MainMenuUI = GameObject.Find("MainMenu");
        DisableGameMenu();
        DisableLoadMenu();
        DisableSaveMenu();
    }

    public void EnableGameMenu()
    {
        // TODO: fix game manager shows up on the third esc press instead of the first one
        //Debug.Log("EnableGameMenu");
        transform.GetChild(0).gameObject.SetActive(true);
        GameMenuUI = GameObject.Find("GameMenu");
        DisableMainMenu();
        DisableSaveMenu();
        DisableLoadMenu();
    }

    public void EnableLoadMenu()
    {
        GameManager.SaveData saveData = GameObject.Find("GameManager").GetComponent<GameManager>().GetSaveData();

        transform.GetChild(3).gameObject.SetActive(true);
        LoadMenuUI = GameObject.Find("LoadMenu");
        setSlotText("Slot1", saveData.slot1);
        setSlotText("Slot2", saveData.slot2);
        setSlotText("Slot3", saveData.slot3);
        
        DisableSaveMenu();
    }

    //Search for the slot name and set the text by the save data
    private void setSlotText(string slotName, GameManager.SaveSlot slot)
    {
        GameObject SlotObject = GameObject.Find(slotName);
        GameObject SlotTextObject = SlotObject.transform.Find("Text").gameObject;
        TMP_Text Slot = SlotTextObject.GetComponent<TextMeshProUGUI>();

        Debug.Log("Slot: " + Slot);
        
        if (slot != null && slot.activeScreenName != null)
        {
            Slot.text = slot.activeScreenName+": "+slot.lastSaveDate;
        }
        else
        {
            Slot.text = "Empty";
        }
    }
    
    public void EnableSaveMenu()
    {
        GameManager.SaveData saveData = GameObject.Find("GameManager").GetComponent<GameManager>().GetSaveData();

        transform.GetChild(2).gameObject.SetActive(true);
        SaveMenuUI = GameObject.Find("SaveMenu");
        setSlotText("Slot1", saveData.slot1);
        setSlotText("Slot2", saveData.slot2);
        setSlotText("Slot3", saveData.slot3);
        DisableLoadMenu();
    }

    // - Disable UI elements - //
    public void DisableMainMenu()
    {
        try
        {
            MainMenuUI.SetActive(false);
        }
        catch (System.Exception e)
        {
           // Debug.Log("MainMenuUI is null");
        }
    }

    public void DisableGameMenu()
    {
        try {
            GameMenuUI.SetActive(false);
        }
        catch (System.Exception e)
        {
        //Debug.Log("GameMenuUI is null");
        }
      
    }

    public void DisableLoadMenu()
    {
        try
        {
            LoadMenuUI.SetActive(false);
        }
        catch (System.Exception e)
        {
           // Debug.Log("LoadMenuUI is null");
        }
    }

    public void DisableSaveMenu()
    {
        try {
            SaveMenuUI.SetActive(false);
        }
        catch (System.Exception e)
        {
           // Debug.Log("SaveMenuUI is null");
        }
    }


    // - Button Methods - //
    public void SetContinueButton(bool isSavedDataFound)
    {
        Debug.Log("SetContinueButtonMethod");
        GameObject childTransform = GameObject.Find("Continue");
        Debug.Log("SetContinueButton: " + childTransform);

        childTransform.SetActive(isSavedDataFound);
    }
    
    // TODO remove this method if it is not used
    // - Update - //
    public void Update()
    {
        Debug.Log("Update");
        Debug.Log("GameMenu: " + GameMenuUI);
       
    }



}
