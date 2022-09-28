using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        button.onClick.AddListener(gm.GameQuit);
    }

}
