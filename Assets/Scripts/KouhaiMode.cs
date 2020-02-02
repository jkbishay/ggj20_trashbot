using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KouhaiMode : MonoBehaviour
{
    public GameObject[] Limbs;
    public TextMeshPro Info;

    private float timer;
    private int limbIndex;
    private bool victoryCheck;

    // Start is called before the first frame update
    void Start()
    {
        limbIndex = 0;
        timer = 0;
        victoryCheck = false;
        foreach (GameObject piece in Limbs)
        {
            piece.GetComponent<MeshRenderer>().enabled = false;
        }

        StartCoroutine(TutorialText());
    }

    // Update is called once per frame
    void Update()
    {
        if (limbIndex < Limbs.Length && timer >= limbIndex * 15)
        {
            Limbs[limbIndex].GetComponent<MeshRenderer>().enabled = true;
            limbIndex++;
        }
        timer += Time.deltaTime;

        victoryCheck = true;
        foreach(GameObject piece in Limbs)
        {
            if (piece.activeInHierarchy)
            {
                victoryCheck = false;
            }
        }

        if (victoryCheck)
        {
            SceneManager.LoadScene("Win");
        }
    }

    IEnumerator TutorialText()
    {
        yield return new WaitForSeconds(3f);
        Info.text = "You are a Walking Autonomous Industrial Fetching Unit";
        yield return new WaitForSeconds(4f);
        Info.text = "or W.A.I.F.U. for short";
        yield return new WaitForSeconds(2.5f);
        Info.text = "Collect trash to stay alive";
        yield return new WaitForSeconds(2f);
        Info.text = "Search for limbs of a trashed figruine to rebuild it";
        yield return new WaitForSeconds(4f);
        Info.text = "Avoid WAIFU Extreme Extermination Bots";
        yield return new WaitForSeconds(3f);
        Info.text = "W.E.E.B. units are aggressive and will kill you";
        yield return new WaitForSeconds(3f);
        Info.gameObject.SetActive(false);
    }
}
