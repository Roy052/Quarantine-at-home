using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSM : MonoBehaviour
{
    GameManager gm;

    public Text timer, music;
    int hours = 10, minutes = 0, seconds = 0;
    int quarantineday = 0;
    float secondCheck = 0;


    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        quarantineday = gm.quarantineday;
        timer.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
    }
    private void Update()
    {
        
        secondCheck += Time.deltaTime;
        if(secondCheck >= 1)
        {
            TimeFlows(1);
            secondCheck = 0;
        }


        if(hours >= 22)
        {
            gm.DayEnd();
        }
    }
    public void Clicked()
    {
        TimeFlows(1);
    }

    public void TimeFlows(int ammount)
    {
        seconds += ammount;
        if (seconds >= 60)
        {
            minutes += seconds / 60;
            seconds %= 60;
        }
        if (minutes == 60)
        {
            hours += minutes / 60;
            minutes %= 60;
        }
        timer.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
    }
}
