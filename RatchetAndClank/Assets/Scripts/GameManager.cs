using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

public class GameManager : MonoBehaviour
{

    public class SaveSlot{
        public string createDate;
        public string lastSaveDate;
        public string activeScreenName;
    }
    [System.Serializable]
    public class SaveData
    {
        public SaveSlot slot1;
        public SaveSlot slot2;
        public SaveSlot slot3;
    }
    private SaveData saveData;
    private static string filePath;

    [SerializeField]
    private static UIManager UIManager;


    private float mainMenuTimer = Mathf.Infinity;
    private float mainMenuTime = 0.5f;
    private float _deltaTime = 0.0f;

    private bool isSavedDataFound = false;
    private bool isGameOver = false;
    private bool isGameStarted = false;
    private bool isPause = false;
    private bool isLevelComplete = false;

    // Start is called before the first frame update
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/Save.txt";
        saveData = new SaveData();

        ReadDataFromFile();
        
        GameObject UIManagerObject = GameObject.Find("Canvas");
        UIManager = UIManagerObject.GetComponent<UIManager>();
        UIManager.SetContinueButton(isSavedDataFound);

        _deltaTime = Time.deltaTime;

        DontDestroyOnLoad(gameObject);
    }

    // - File Write and Read - //
    /**********************************************************************************************/
    
    private void WriteDataToFile()
    {
        string jsonData = JsonConvert.SerializeObject(saveData);

        // Fájlba írás
        File.WriteAllText(filePath, jsonData);

        Debug.Log("SaveFile: " + filePath);
    }

    private void ReadDataFromFile()
    {
      if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            if(saveData.slot1.activeScreenName != null || saveData.slot2.activeScreenName != null || saveData.slot3.activeScreenName != null){
                isSavedDataFound = true;
            }
        }
        else
        {
            Debug.Log("File not found: " + filePath);
            saveData.slot1 = new SaveSlot();
            saveData.slot2 = new SaveSlot();
            saveData.slot3 = new SaveSlot();
            WriteDataToFile();
        }
     
    }

    // Update is called once per frame
    void Update()
    {
     
        if (isGameOver == true){

        }
        if (
                Input.GetKey(KeyCode.Escape) &&
                mainMenuTimer >= mainMenuTime
            )
            {
            Debug.Log("ESC");
                if (!isPause)
                {
                    Debug.Log("Pause");
                    PauseGame();
                }else
                {
                    Debug.Log("Continue");
                    ContinueGame();
                }
            }
        mainMenuTimer += _deltaTime;

        
    }

    // - Menu related methods - //
    /**********************************************************************************************/
    public void QuitGame()
    {
        Application.Quit();
    }

    //Start a new game
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UIManager.DisableMainMenu();
        Time.timeScale = 1;
        mainMenuTimer = 0;
        isPause = false;
        isGameStarted = true;
    }

    //Save the game
    
    public void SaveGame(int slotNumber){
        //Save the game
        try
        {
            string activeScreenName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            SaveSlot slot;

            Debug.Log("SaveData: " + JsonUtility.ToJson(saveData));
            switch (slotNumber)
            {
                case 1:
                    slot = saveData.slot1;
                    Debug.Log("Slot1: " + JsonUtility.ToJson(slot));
                    break;
                case 2:
                    slot = saveData.slot2;
                    Debug.Log("Slot2: " + JsonUtility.ToJson(slot));
                    break;
                case 3:
                    slot = saveData.slot3;
                    Debug.Log("Slot3: " + JsonUtility.ToJson(slot));
                    break;
                default:
                    slot = new SaveSlot();
                    break;
            }

            if(slot.createDate == null){
                slot.createDate = System.DateTime.Now.ToString();
            }
            slot.lastSaveDate = System.DateTime.Now.ToString();
            slot.activeScreenName = activeScreenName;
    
            WriteDataToFile();
        }
       catch (KeyNotFoundException)
        {
            Debug.Log("SaveGameError " + slotNumber);
        }
        UIManager.DisableSaveMenu();
        ContinueGame();
        
    }

    //Load the game
    public void LoadGame(int slotNumber){
        try
        {
            SaveSlot slot = new SaveSlot();
            switch (slotNumber)
            {
                case 1:
                    slot = saveData.slot1;
                    break;
                case 2:
                    slot = saveData.slot2;
                    break;
                case 3:
                    slot = saveData.slot3;
                    break;
                default:
                    break;
            }
            SceneManager.LoadScene(slot.activeScreenName);
            UIManager.DisableLoadMenu();
            UIManager.DisableMainMenu();
            UIManager.DisableGameMenu();
            
        }
         catch (KeyNotFoundException)
        {
            Debug.Log("KeyNotFound: ActiveScreen");
        }
    }

    // Load the next scene
    // if give screenName, load that scene if not load the next scene
    public void LoadNextScene(string sceneName = null)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
 
    // - Game related methods - //
    // Pause the game
    public void PauseGame()
    {
        UIManager.EnableGameMenu();

        Time.timeScale = 0;
        mainMenuTimer = 0;
        isPause = true;
    }

    // Continue the game
    public void ContinueGame()
    {
        UIManager.DisableGameMenu();

        Time.timeScale = 1;
        mainMenuTimer = 0;
        isPause = false;
    }

    // - Getters - //
    /**********************************************************************************************/
    public bool GetIsGamePause()
    {
        return isPause;
    }
    public SaveData GetSaveData()
    {
        return saveData;
    }
    public bool GetIsLevelComplete()
    {
        return isLevelComplete;
    }
    public bool GetIsGameOver()
    {
        return isGameOver;
    }
    public bool GetIsGameStarted()
    {
        return isGameStarted;
    }

    // - Setters - //
    /**********************************************************************************************/
    public void setIsLevelComplete(bool _isLevelComplete)
    {
        isLevelComplete = _isLevelComplete;
    }
}
