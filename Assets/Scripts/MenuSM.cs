using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSM : MonoBehaviour
{
    public Button gameStart, gameContinue, quit;
    public Text gameContinueText;
    GameManager gm;
    QuarantineData quarantineData;

    //BGM
    public AudioClip menuBGM;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.mainBGMLoader.clip = menuBGM;
        StartCoroutine( gm.LightOn(3));
        quarantineData = SaveDataScript.LoadFromJson();
        if (quarantineData == null)
        {
            gameContinue.enabled = false;
            gameContinueText.color = Color.gray;
        }

            
    }

    public void GameStart()
    {
        gm.mainBGMLoader.Stop();
        StartCoroutine(gm.QuarantineIn(3));
    }

    public void GameContinue()
    {
        gm.mainBGMLoader.Stop();
        gm.quarantineData = quarantineData;
        StartCoroutine(gm.QuarantineIn(3));
    }
}
