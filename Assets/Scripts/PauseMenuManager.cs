using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    GameObject gameManagerObject;
    [SerializeField] Slider volumeSlider;

    public QuarantineData quarantineData;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void PauseON(QuarantineData quarantineData)
    {
        this.quarantineData = quarantineData;
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOFF()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }


    public void ToMenu()
    {
        gameManagerObject.GetComponent<GameManager>().AutoSave(quarantineData);
        gameManagerObject.GetComponent<GameManager>().ToMenu();
        PauseOFF();
    }

    public void GameQuit()
    {
        gameManagerObject.GetComponent<GameManager>().AutoSave(quarantineData);
        gameManagerObject.GetComponent<GameManager>().GameQuit();
    }
}
