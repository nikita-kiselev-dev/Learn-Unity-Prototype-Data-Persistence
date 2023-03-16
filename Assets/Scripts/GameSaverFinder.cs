using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSaverFinder : MonoBehaviour
{
    private GameObject GameSaver;
    // Start is called before the first frame update
    void Start()
    {
        GameSaver = GameObject.FindWithTag("Game Saver");
        switch (gameObject.tag)
        {
            case ("Player Input"):
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
