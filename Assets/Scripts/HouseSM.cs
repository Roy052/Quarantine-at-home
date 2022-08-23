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
    int[,] activateDuration = new int[6, 3];
    int[,] activityCooldown = new int[6, 3];
    bool[] activityEnabled = new bool[6];
    int[] upgradeProgress = new int[6];
    public Button[] activateButtons, upgradeButtons;
    public Text[] upgradeTexts;

    int hours = 21, minutes = 59, seconds = 0;
    int quarantineday = 1;
    float secondCheck = 0;
    AudioSource audioSource;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        quarantineday = gm.quarantineday;
        exp = gm.exp;
        level = gm.level;
        for (int i = 0; i < 6; i++)
            upgradeProgress[i] = gm.upgradeProgress[i];

        timerText.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
        expText.text = exp + "/" + Data.expGaps[level];
        expSlider.value = exp / Data.expGaps[level];

        //Init
        for (int i = 0; i < 6; i++)
        {
            activityEnabled[i] = true;
            for(int j = 0; j < 3; j++)
            {
                activateDuration[i, j] = 0;
                activityCooldown[i, j] = 0;
            }

            Debug.Log(i + ", " + upgradeProgress[i] + " : ");
            upgradeTexts[i].text = Data.upgradeValue[i, upgradeProgress[i]] + " to Upgrade";
        }
           
        
    }
    private void Update()
    {
        
        secondCheck += Time.deltaTime;
        
        //Seconds Flow
        if((activityNum != -1 && secondCheck >= 1.0 / Data.activityTimeValue[activityNum]) || secondCheck >= 1)
        {
            if (activityNum != -1)
            {
                TimeFlows(1);

                //Acitity End
                if(hours > activateDuration[activityNum,0] || 
                    (hours == activateDuration[activityNum, 0] && minutes >= activateDuration[activityNum,1]))
                {
                    activityNum = -1;
                }

                //Cooldown End
                for(int i = 0; i < 6; i++)
                {
                    if(activityEnabled[i] == false)
                    {
                        if (hours > activityCooldown[i, 0] ||
                    (hours == activityCooldown[i, 0] && minutes >= activityCooldown[i, 1]))
                        {
                            activityEnabled[i] = true;
                        }
                    }
                }
                
            }
            else
            {
                TimeFlows(1);
            }

            secondCheck = 0;
        }

        if(quarantineday == 6 && hours >= 24)
        {
            gm.QuarantineEnd();
        }
        else
        {
            if (hours >= 22)
            {
                //Save
                gm.exp = exp;
                quarantineday++;
                gm.quarantineday = quarantineday;
                gm.level = level;
                for (int i = 0; i < 6; i++)
                    gm.upgradeProgress[i] = upgradeProgress[i];

                gm.DayOver();
            }

        }
            

        //Button Enable
        for(int i = 0; i < 6; i++)
        {
            if(activityNum != -1)
            {
                activateButtons[i].enabled = false;
            }
            else
            {
                if (activityEnabled[i])
                {
                    activateButtons[i].enabled = true;
                    activateButtons[i].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    activateButtons[i].enabled = false;
                    activateButtons[i].GetComponent<Image>().color = Color.gray;
                }
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
        AddDurationCooldown(num);

    }

    public void UpgradeON(int num)
    {
        GainExp(-Data.upgradeValue[num, upgradeProgress[num]]);
        upgradeProgress[num]++;
        upgradeTexts[num].text = Data.upgradeValue[num, upgradeProgress[num]] + " to Upgrade";
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

    public void AddDurationCooldown(int activityNum)
    {
        activateDuration[activityNum, 0] = hours;
        activateDuration[activityNum, 1] = minutes;
        activateDuration[activityNum, 2] = seconds;

        activityCooldown[activityNum, 0] = hours;
        activityCooldown[activityNum, 1] = minutes;
        activityCooldown[activityNum, 2] = seconds;

        for (int i = 0; i < 3; i++)
        {
            //Duration Check
            activateDuration[activityNum, 1] += (int) (Data.activityDuration[activityNum] * 60);
            activateDuration[activityNum, 0] += activateDuration[activityNum, 1] / 60;
            activateDuration[activityNum, 1] %= 60;

            //Cooldown Check
            activityCooldown[activityNum, 1] += (int)(Data.activityDuration[activityNum] * 60 
                + Data.activityCooldown[activityNum] * 60);
            activityCooldown[activityNum, 0] += activityCooldown[activityNum, 1] / 60;
            activityCooldown[activityNum, 1] %= 60;
        }
        
    }
}
