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
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        quarantineData = SaveDataScript.LoadFromJson();
        if (quarantineData == null)
        {
            gameContinue.enabled = false;
            gameContinueText.color = Color.gray;
        }

            
    }

    public void GameStart()
    {
        StartCoroutine(gm.QuarantineIn(3));
    }

    public void GameContinue()
    {
        Debug.Log(quarantineData.exp);
        gm.quarantineData = quarantineData;
        StartCoroutine(gm.QuarantineIn(3));
    }
}
