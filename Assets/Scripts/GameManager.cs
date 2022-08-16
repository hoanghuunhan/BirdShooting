using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Bird[] birdPrefabs;
    public float spawnTime;
    public int timeLimit;

    int m_curTimeLimit;
    int m_birdKill;
    bool m_isGameOver;
    public Player player;

    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public int BirdKill { get => m_birdKill; set => m_birdKill = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_curTimeLimit = timeLimit;

    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKill);

    }

    public void PlayGame()
    {
        player.isShotting = true;
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);

    }

    IEnumerator TimeCountDown()
    {
        while(m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;
            if (m_curTimeLimit <= 0)
            {
                player.isShotting = false;
                m_isGameOver = true;
                if (m_birdKill > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST SCORE: " + m_birdKill);

                } else
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("GOOD", "BEST SCORE: " + Prefs.bestScore);

                }
                Prefs.bestScore = m_birdKill;
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

                
            }
            GameGUIManager.Ins.UpdateTimer(IntoTime(m_curTimeLimit));
        }
    }

    IEnumerator GameSpawn()
    {
        while(!m_isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        float randCheck = Random.Range(0f, 1f);
        if(randCheck >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(1.5f, 4f), 0);
        } else
        {
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length >0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length);

            if (birdPrefabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }

    string IntoTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        return minute.ToString("00") + ":" + seconds.ToString("00");
    }
}
