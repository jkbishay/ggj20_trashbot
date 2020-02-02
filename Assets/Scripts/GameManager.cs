using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject WEEB;
    public Transform[] WEEBSpawnPoints;

    private float WEEBInterval;
    private float timer;
    private int WEEBCount;
    private int respawnCount;

    [SerializeField] private GameMode mode = GameMode.KOUHAI;

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
                WEEBCount = 0;
                break;
        }

        timer = 0;
        respawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerControl>().isAlive)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("Lose");
        }

        if (timer >= WEEBCount * WEEBInterval)
        {
            // spawn new bot
            SpawnWEEB();
        }

        if (mode == GameMode.SENSEI && timer >= 20 * respawnCount + 1)
        {
            this.GetComponent<LayoutSpawner>().RepopulateArea(100, 200);
            respawnCount++;
        }

        if (mode == GameMode.KOUHAI && timer >= 15 * respawnCount + 1)
        {
            this.GetComponent<LayoutSpawner>().RepopulateArea(50, 100);
            respawnCount++;
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
