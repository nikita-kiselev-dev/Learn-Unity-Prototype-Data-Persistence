using System;
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
    
    public void SavePlayerName(string playerNameFromMenuManager)
    {
        playerName = playerNameFromMenuManager;
    }
    
    [System.Serializable]
    class SaveData
    {
        public int bestPlayerScore;
        public string bestPlayerName;
        public string lastPlayerName;
    }

    public void SavePlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data = new SaveData();
        data.bestPlayerScore = bestPlayerScore;
        data.bestPlayerName = bestPlayerName;
        data.lastPlayerName = playerName;
        
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(path, json);
    }



    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerScore = data.bestPlayerScore;
            bestPlayerName = data.bestPlayerName;
            playerName = data.lastPlayerName;
        }
    }

}
