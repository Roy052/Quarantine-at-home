using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSM : MonoBehaviour
{
    GameManager gm;

    public Text timerText, musicText, expText, activityText;
    public Slider expSlider;

    //Fix ����
    int exp = 0, level = 0;
    int activityNum = -1;
    int[] activateDuration = new int[6];
    int[] activityCooldown = new int[6];
    bool[] activityEnabled = new bool[6];
    int[] upgradeProgress = new int[6];
    public QuarantineData quarantineData;

    public Button[] activateButtons, upgradeButtons;
    public Text[] upgradeTexts;

    int hours = 21, minutes = 59, seconds = 0;
    int quarantineday = 1;
    float secondCheck = 0;
    bool dayEnd = false;
    AudioSource audioSource;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(gm.quarantineData.quarantineday);
        quarantineData = gm.quarantineData;

        //Data Pull
        PullData();

        timerText.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
        expText.text = exp + "/" + Data.expGaps[level];
        expSlider.value = exp / Data.expGaps[level];

        gm.LightOn(3);

        //Init
        for (int i = 0; i < 6; i++)
        {
            activityEnabled[i] = true;
            activateDuration[i] = 0;
            activityCooldown[i] = 0;

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
                if(activateDuration[activityNum] >= (hours * 3600 + minutes * 60 + seconds))
                {
                    activityNum = -1;
                }

                //Cooldown End
                for(int i = 0; i < 6; i++)
                {
                    if(activityEnabled[i] == false)
                    {
                        if (activityCooldown[i] >= (hours * 3600 + minutes * 60 + seconds))
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

        if(dayEnd == false)
        {
            if (quarantineday == 7 && hours >= 24)
            {
                dayEnd = true;
                gm.AutoSave(new QuarantineData());
                gm.QuarantineEnd();
            }
            else
            {
                if (hours >= 22)
                {
                    //Save
                    PushData();
                    quarantineData.quarantineday++;
                    gm.quarantineData = quarantineData;
                    gm.AutoSave(quarantineData);
                    dayEnd = true;

                    StartCoroutine(gm.DayOver(3));
                }
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
        activateDuration[activityNum] = hours * 3600 + minutes * 60 + seconds;
        activityCooldown[activityNum] = hours * 3600 + minutes * 60 + seconds;

        for (int i = 0; i < 3; i++)
        {
            //Duration Check
            activateDuration[activityNum] += (int) (Data.activityDuration[activityNum] * 3600);

            //Cooldown Check
            activityCooldown[activityNum] += (int)(Data.activityDuration[activityNum] * 3600 
                + Data.activityCooldown[activityNum] * 3600);
        }
        
    }

    public void PullData()
    {
        //Progress
        quarantineday = quarantineData.quarantineday;
        exp = quarantineData.exp;
        level = quarantineData.level;
        upgradeProgress = quarantineData.upgradeProgress;

        //Time
        hours = quarantineData.hours;
        minutes = quarantineData.minutes;
        seconds = quarantineData.seconds;

        //Activity
        activateDuration = quarantineData.activateDuration;
        activityCooldown = quarantineData.activityCooldown;
        activityEnabled = quarantineData.activityEnabled;

        //ActivityNum
        activityNum = -1;
        for(int i = 0; i < 6; i++)
        {
            if(activateDuration[i] != 0 && activateDuration[i] < (hours * 3600 + minutes * 60 + seconds))
            {
                activityNum = i;
                break;
            }
        }
    }

    public void PushData()
    {
        //Progress
        quarantineData.quarantineday = quarantineday;
        quarantineData.exp = exp;
        quarantineData.level = level;
        quarantineData.upgradeProgress = upgradeProgress;

        
        if(hours >= 22)
        {
            //Time
            quarantineData.hours = 21;
            quarantineData.minutes = 59;
            quarantineData.seconds = 0;

            //Activity
            quarantineData.activateDuration = new int[6] { 0, 0, 0, 0, 0, 0 };
            quarantineData.activityCooldown = new int[6] { 0, 0, 0, 0, 0, 0 };
            quarantineData.activityEnabled = new bool[6] { true, true, true, true, true, true };
        }
        else
        {
            //Time
            quarantineData.hours = hours;
            quarantineData.minutes = minutes;
            quarantineData.seconds = seconds;

            //Activity
            quarantineData.activateDuration = activateDuration;
            quarantineData.activityCooldown = activityCooldown;
            quarantineData.activityEnabled = activityEnabled;
        }
    }

    public void ToMenu()
    {
        PushData();
        gm.quarantineData = quarantineData;
        gm.ToMenu();
    }
}
