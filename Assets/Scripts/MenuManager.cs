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
    public void StartGame()
    {
        SceneManager.LoadScene((int)SceneName.Main);
    }
    
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
