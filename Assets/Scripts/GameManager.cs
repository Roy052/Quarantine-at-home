using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Save
    public int quarantineday;
    public int exp, level;
    public int[] upgradeProgress = new int[6];

    public GameObject sceneMovementChanger;

    private void Start()
    {
        //Init
        quarantineday = 1;
        exp = 0; level = 0;
        for (int i = 0; i < 6; i++)
            upgradeProgress[i] = 0;
    }

    public IEnumerator DayOver(float seconds)
    {
        StartCoroutine(LightOff(seconds));
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("DayOver");
    }

    public IEnumerator QuarantineIn(float seconds)
    {
        SceneManager.LoadScene("House");
        StartCoroutine(LightOn(seconds));
        yield return new WaitForSeconds(seconds);
    }

    public void QuarantineEnd()
    {
        SceneManager.LoadScene("End");
    }
    
    public void GameEnd()
    {

    }
    public IEnumerator LightOn(float seconds)
    {
        sceneMovementChanger.GetComponent<Image>().color = Color.white;
        sceneMovementChanger.SetActive(true);
        StartCoroutine(FadeManager.FadeOut(sceneMovementChanger.GetComponent<Image>(), seconds));
        yield return new WaitForSeconds(seconds+0.5f);
        sceneMovementChanger.SetActive(false);
    }

    public IEnumerator LightOff(float seconds)
    {
        sceneMovementChanger.GetComponent<Image>().color = Color.black;
        sceneMovementChanger.SetActive(true);
        StartCoroutine(FadeManager.FadeIn(sceneMovementChanger.GetComponent<Image>(), seconds));
        yield return new WaitForSeconds(seconds+0.5f);
        sceneMovementChanger.SetActive(false);
    }
}
