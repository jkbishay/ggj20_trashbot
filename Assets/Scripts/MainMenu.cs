using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Camera MainCam;
    public GameObject KouhaiButton;
    public GameObject SenpaiButton;
    public GameObject SenseiButton;
    public GameObject QuitButton;
    public Color originalColor;
    public GameObject KouhaiText;
    public GameObject SenpaiText;
    public GameObject SenseiText;
    public GameObject QuitText;


    // Use this for initialization
    void Start()
    {
        originalColor = KouhaiButton.GetComponent<TextMeshPro>().color;
        KouhaiText.SetActive(false);
        SenpaiText.SetActive(false);
        SenseiText.SetActive(false);
        QuitText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        RaycastHit hit;
        // If click input
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == KouhaiButton)
                {
                    SceneManager.LoadScene(1);
                }
                else if (hit.transform.gameObject == SenpaiButton)
                {
                    SceneManager.LoadScene(2);
                }
                else if (hit.transform.gameObject == SenseiButton)
                {
                    SceneManager.LoadScene(3);
                }
                else if (hit.transform.gameObject == QuitButton)
                {
                    Application.Quit();
                }
            }
        }

        // mouse hover
        Ray ray2 = MainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hit))
        {
            if (hit.transform.gameObject == KouhaiButton)
            {
                KouhaiButton.GetComponent<TextMeshPro>().color = Color.red;
                KouhaiText.SetActive(true);

            }
            else if (hit.transform.gameObject == SenpaiButton)
            {
                SenpaiButton.GetComponent<TextMeshPro>().color = Color.red;
                SenpaiText.SetActive(true);
            }
            else if (hit.transform.gameObject == SenseiButton)
            {
                SenseiButton.GetComponent<TextMeshPro>().color = Color.red;
                SenseiText.SetActive(true);
            }
            else if (hit.transform.gameObject == QuitButton)
            {
                QuitButton.GetComponent<TextMeshPro>().color = Color.red;
                QuitText.SetActive(true);
            }
            else
            {
                KouhaiText.SetActive(false);
                KouhaiButton.GetComponent<TextMeshPro>().color = originalColor;
                SenpaiText.SetActive(false);
                SenpaiButton.GetComponent<TextMeshPro>().color = originalColor;
                SenseiText.SetActive(false);
                SenseiButton.GetComponent<TextMeshPro>().color = originalColor;
                QuitText.SetActive(false);
                QuitButton.GetComponent<TextMeshPro>().color = originalColor;
                
            }
            
        }
    }
}

