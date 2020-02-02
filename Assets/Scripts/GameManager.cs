using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject WEEB;
    public Transform[] WEEBSpawnPoints;

    private float WEEBInterval;
    private float timer;
    private float timeLimit;
    private int WEEBCount;

    [SerializeField] private GameMode mode;

    private enum GameMode
    {
        KOUHAI,
        SENPAI,
        SENSEI
    }

    // Start is called before the first frame update
    void Start()
    {
        switch(mode)
        {
            case GameMode.KOUHAI:
                WEEBCount = 0;
                WEEBInterval = 10;
                break;
            case GameMode.SENPAI:
                WEEBCount = 0;
                WEEBInterval = 1;
                 break;
            case GameMode.SENSEI:
                WEEBInterval = 10;
                WEEBCount = 3;
                break;
        }

        timer = 0;

        for (int i = 0; i < WEEBCount; i++)
        {
            SpawnWEEB();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerControl>().isAlive)
        {
            timer += Time.deltaTime;
        }

        if (timer >= WEEBCount * WEEBInterval)
        {
            // spawn new bot
            SpawnWEEB();
        }
    }

    void SpawnWEEB()
    {
        int spawnPoint = Random.Range(0, WEEBSpawnPoints.Length);

        while (Vector3.Distance(Player.transform.position, WEEBSpawnPoints[spawnPoint].position) < 10)
        {
            spawnPoint = Random.Range(0, WEEBSpawnPoints.Length);
        }
            
        GameObject NewWEEB = Instantiate(WEEB, WEEBSpawnPoints[spawnPoint].position, Quaternion.identity);
        NewWEEB.GetComponent<WEEBControl>().Waifu = Player;
        WEEBCount++;
    }
}
