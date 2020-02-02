using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 7 || (timer >= 2 && Input.anyKeyDown))
        {
            SceneManager.LoadScene(0);
        }
    }
}
