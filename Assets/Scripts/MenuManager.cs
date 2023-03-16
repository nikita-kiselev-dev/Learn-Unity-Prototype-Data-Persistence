using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string playerName;
    [SerializeField] GameObject playerNameInput;
    
    [SerializeField] GameObject bestPlayerText;
    [SerializeField] GameObject pleaseEnterNameText;

    private void Start()
    {
        GameSaver.Instance.LoadPlayerData();
        
        playerNameInput.GetComponent<TMP_InputField>().text = GameSaver.Instance.playerName;

        if (GameSaver.Instance.bestPlayerScore > 0)
        {
            bestPlayerText.GetComponent<TextMeshProUGUI>().text = $"Best score : {GameSaver.Instance.bestPlayerName} : {GameSaver.Instance.bestPlayerScore}";
            bestPlayerText.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (playerNameInput.GetComponent<TMP_InputField>().text != "")
        {
            SceneManager.LoadScene((int)SceneName.Main);
        }
        else
        {
            pleaseEnterNameText.SetActive(true);
        }
    }
    
    public void Exit()
    {
        GameSaver.Instance.SavePlayerData();
        
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void EnterPlayerName()
    {
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        GameSaver.Instance.SavePlayerName(playerName);
    }
}
