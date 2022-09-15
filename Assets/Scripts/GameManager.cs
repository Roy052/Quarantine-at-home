using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

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

    public AudioSource mainBGMLoader, sideBGMLoader;
    public List<AudioClip> mainBGMList = new List<AudioClip>();
    public List<AudioClip>[] sideBGMList = new List<AudioClip>[6];

    private void Start()
    {
        sceneMovementChanger.SetActive(false);

        //BGMLoad

        //Main
        mainBGMList.AddRange( Resources.LoadAll<AudioClip>("Music/Main/"));

        string debugText = "";
        for (int i = 0; i < 1; i++)
        {
            sideBGMList[i] = new List<AudioClip>();
            sideBGMList[i].AddRange(Resources.LoadAll<AudioClip>("Music/Side/" + i + "/"));
            debugText += i + " : " + sideBGMList[i].Count + ", ";
        }
        Debug.Log(debugText);
            
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
