using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenpaiMode : MonoBehaviour
{
    public TextMeshPro Clock;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 20;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Clock.text = timer.ToString("00");

        if (timer <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
