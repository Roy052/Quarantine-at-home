using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int quarantineday = 0;
    public int exp = 0, level = 0;
    public int[] upgradeProgress = new int[6];

    private void Start()
    {
        for (int i = 0; i < 6; i++)
            upgradeProgress[i] = 0;
    }

    public void DayEnd()
    {
        SceneManager.LoadScene("Days");
    }
    public void QuarantineEnd()
    {
        SceneManager.LoadScene("End");
    }
}
