using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

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
    int mainCurrent = -1;
    public int[] sideCurrent = new int[6] { -1, -1, -1, -1, -1, -1 };


    private void Start()
    {
        sceneMovementChanger.SetActive(false);
    }

    public IEnumerator MainBGMLoad()
    {
        //Main
        mainBGMList.AddRange(Resources.LoadAll<AudioClip>("Music/Main/"));


        //Mix
        mainBGMList = mainBGMList.OrderBy(a => Guid.NewGuid()).ToList();

        yield return new WaitForEndOfFrame();
    }
    public IEnumerator SideBGMLoad()
    {
        for (int i = 0; i < 6; i++)
        {
            sideBGMList[i] = new List<AudioClip>();
            sideBGMList[i].AddRange(Resources.LoadAll<AudioClip>("Music/Side/" + i + "/"));
        }
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator MainBGMON()
    {
        StartCoroutine(BGMOFF(sideBGMLoader, 1));
        yield return new WaitForSeconds(1);
        mainCurrent++;
        if (mainCurrent >= mainBGMList.Count) mainCurrent = 0;
        mainBGMLoader.clip = mainBGMList[mainCurrent];
        StartCoroutine(BGMON(mainBGMLoader, 1));
    }

    public IEnumerator SideBGMON(int num)
    {
        if (num == -1) yield break;
        StartCoroutine(BGMOFF(mainBGMLoader, 1));
        yield return new WaitForSeconds(1);
        sideCurrent[num]++;
        if (sideCurrent[num] >= sideBGMList[num].Count) sideCurrent[num] = 0;
        sideBGMLoader.clip = sideBGMList[num][sideCurrent[num]];
        StartCoroutine(BGMON(sideBGMLoader, 1));
    }

    public IEnumerator BGMON(AudioSource audioSource, float time)
    {
        float tempVolume = 0;
        audioSource.volume = tempVolume;

        audioSource.Play();
        while(tempVolume < 0.5f)
        {
            tempVolume += Time.deltaTime * 0.5f / time;
            audioSource.volume = tempVolume;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator BGMOFF(AudioSource audioSource, float time)
    {
        float tempVolume = audioSource.volume;
        while (tempVolume > 0)
        {
            tempVolume -= Time.deltaTime * 0.5f / time;
            audioSource.volume = tempVolume;
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
    }

    public IEnumerator DayOver(float seconds)
    {
        StartCoroutine(LightOff(seconds));
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("DayOver");
    }

    public IEnumerator QuarantineIn(float seconds)
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("House");
        StartCoroutine(LightOn(seconds));
        yield return new WaitForSeconds(seconds);
    }

    public IEnumerator QuarantineEnd(float seconds)
    {
        StartCoroutine(LightOff(seconds));
        yield return new WaitForSeconds(seconds);
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
