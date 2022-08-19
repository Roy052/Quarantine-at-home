using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSM : MonoBehaviour
{
    GameManager gm;

    public Text timerText, musicText, expText, activityText;
    public Slider expSlider;
    int exp = 0, level = 0;
    int activityNum = -1;
    int[] activateDuration = new int[3] { 0, 0, 0 };
    int[] activityCooldown = new int[3] { 0, 0, 0 };
    int[] upgradeProgress = new int[3] { 0, 0, 0 };
    public Button[] activateButtons, upgradeButtons;

    int hours = 10, minutes = 0, seconds = 0;
    int quarantineday = 0;
    float secondCheck = 0;
    AudioSource audioSource;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        quarantineday = gm.quarantineday;
        exp = gm.exp;
        level = gm.level;

        timerText.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
        expText.text = exp + "/" + Data.expGaps[level];
        expSlider.value = exp / Data.expGaps[level];
    }
    private void Update()
    {
        
        secondCheck += Time.deltaTime;
        
        if(secondCheck >= 1)
        {
            if (activityNum != -1)
            {
                TimeFlows(Data.activityTimeValue[activityNum]);
                activateDuration[activityNum]--;
                if (activateDuration[activityNum] <= 0) activityNum = -1;
            }
            else
            {
                TimeFlows(1);
            }

            for (int i = 0; i < 3; i++)
                if (activityCooldown[i] > 0) activityCooldown[i]--;

            secondCheck = 0;
        }

        if(hours >= 22)
        {
            //Save
            gm.exp = exp;
            quarantineday++;
            gm.quarantineday = quarantineday;
            gm.level = level;

            gm.DayEnd();
        }

        for(int i = 0; i < 3; i++)
        {
            if(activityNum != -1)
            {
                activateButtons[i].enabled = false;
            }
            else
            {
                if (activityCooldown[i] != 0) activateButtons[i].enabled = false;
                else activateButtons[i].enabled = true;
            }

            if (exp >= Data.upgradeValue[i, upgradeProgress[i]])
                upgradeButtons[i].enabled = true;
            else
                upgradeButtons[i].enabled = false;
        }
    }
    public void Clicked()
    {
        if(activityNum == -1)
        {
            TimeFlows(1);
        }
        else
        {
            TimeFlows(Data.activityClickValue[activityNum]);
        }
        
        GainExp(1);
    }

    public void ActivityON(int num)
    {
        activityNum = num;
        activityText.text = Data.activityName[num];
        activateDuration[num] = (int)(Data.activityDuration[num] * 3600);
        activityCooldown[num] = (int) (Data.activityDuration[num] * 3600 + Data.activityCooldown[num] * 3600);

    }

    public void UpgradeON(int num)
    {
        GainExp(-Data.upgradeValue[num, upgradeProgress[num]]);
        upgradeProgress[num]++;
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
        timerText.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
    }
    
    public void GainExp(int ammount)
    {
        exp += ammount;
        expSlider.value = exp / (float) Data.expGaps[level];
        if(exp >= Data.expGaps[level])
        {
            level++;
            exp = 0;
        }
        expText.text = exp + "/" + Data.expGaps[level];
    }
}
