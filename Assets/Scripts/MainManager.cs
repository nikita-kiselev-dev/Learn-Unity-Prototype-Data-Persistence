using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    private int bestPoints;
    
    private bool m_GameOver = false;

    [SerializeField] private Text bestScoreText;

    public static MainManager Instance;

    
    // Start is called before the first frame update
    void Start()
    {
        bestPoints = GameSaver.Instance.bestPlayerScore;
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (bestPoints > 0)
        {
            ShowBestScore();
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene((int)SceneName.Main);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        ShowBestScore();
        GameSaver.Instance.SavePlayerData();
    }

    public void BackToMenu()
    {
        GameSaver.Instance.SavePlayerData();
        SceneManager.LoadScene((int)SceneName.Menu);
    }

    public string DetermBestPlayer(int currentPlayerScore, int bestPlayerScore)
    {
        if (currentPlayerScore > bestPlayerScore)
        {
            GameSaver.Instance.bestPlayerName = GameSaver.Instance.playerName;
        }
        return GameSaver.Instance.bestPlayerName;
    }

    public int DetermBestScore(int currentPlayerScore, int bestPlayerScore)
    {
        if (currentPlayerScore > bestPlayerScore)
        {
            GameSaver.Instance.bestPlayerScore = m_Points;
        }
        return GameSaver.Instance.bestPlayerScore;
    }

    public void ShowBestScore()
    {
        bestScoreText.text = $"Best Score : {DetermBestPlayer(m_Points, bestPoints)} : {DetermBestScore(m_Points, bestPoints)}";
    }
}