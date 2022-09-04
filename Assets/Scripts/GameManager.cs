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
    //[HideInInspector]
    public QuarantineData quarantineData = new QuarantineData();

    public GameObject sceneMovementChanger;

    private void Start()
    {
        sceneMovementChanger.SetActive(false);
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

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameQuit()
    {
        SaveDataScript.SaveIntoJson(quarantineData);
        Application.Quit();
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

    public void AutoSave(QuarantineData saveQuarantineData)
    {
        SaveDataScript.SaveIntoJson(saveQuarantineData);
    }
}
