using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameSaver : MonoBehaviour
{    
    public static GameSaver Instance;
    
    public string playerName;
    public string bestPlayerName;
    public int bestPlayerScore;

    //[SerializeField] GameObject playerNameInput;
    

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveBestPlayerData(string playerName, int playerScore)
    {
        bestPlayerName = playerName;
        bestPlayerScore = playerScore;
        Debug.Log($"Best Player: {bestPlayerName} / Best Score: {bestPlayerScore}");
    }
    
    [System.Serializable]
    class SaveData
    {
    }

    public void SavePlayerScore()
    {
        SaveData data = new SaveData();
        
    }

    public void SavePlayerName(string playerNameFromMenuManager)
    {
        playerName = playerNameFromMenuManager;
    }

    public void DataLoad()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
        }
    }

}
