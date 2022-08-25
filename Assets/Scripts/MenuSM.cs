using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSM : MonoBehaviour
{
    public Button gameStart, gameContinue, quit;
    GameManager gm;
    QuarantineData quarantineData;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        quarantineData = SaveDataScript.LoadFromJson();
        if (quarantineData.quarantineday != -1)
            gameContinue.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        StartCoroutine(gm.QuarantineIn(3));
    }

    public void GameContinue()
    {
        gm.quarantineData = quarantineData;
        StartCoroutine(gm.QuarantineIn(3));
    }
}
