using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject WEEB;
    public Transform[] WEEBSpawnPoints;

    private float timer;
    private int WEEBCount;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        WEEBCount = 0;
        SpawnWEEB();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerControl>().isAlive)
        {
            timer += Time.deltaTime;
        }

        if (timer >= WEEBCount * 10)
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
