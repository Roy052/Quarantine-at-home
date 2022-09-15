using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSM : MonoBehaviour
{
    GameManager gm;

    //UI
    public Text timerText, musicText, expText, activityText, levelText;
    public Slider expSlider;
    public Button levelupButton;

    public Button[] activateButtons, upgradeButtons;
    public Text[] activateTexts ,upgradeTexts;
    public Text[] activityTimeflowTexts, activityClickTexts, activityDurationTexts, activityCooltimeTexts;
    AudioSource audioSource;

    int exp = 0, level = 0;
    public int activityNum = -1;
    int[] activateDuration = new int[6];
    public int[] activityCooldown = new int[6];
    public bool[] activityEnabled = new bool[6];
    int[] upgradeProgress = new int[6];
    public QuarantineData quarantineData;

    int hours = 18, minutes = 59, seconds = 40;
    int quarantineday = 1;
    float secondCheck = 0;
    bool dayEnd = false;
    

    public GameObject windowImage;
    bool sunset = false, twilight = false;
    Color tempColor,
        basicColor = new Color(1, 1, 1, 1),
        sunsetColor = new Color(202/(float)255, 133/ (float)255, 60/ (float)255, 1),
        twilightColor = new Color(63/(float)255, 63 / (float)255, 63 / (float)255, 1);

    public Text timer;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(gm.quarantineData.quarantineday);
        quarantineData = gm.quarantineData;

        //Data Pull
        PullData();

        //Init Text
        timerText.text = "Day - " + quarantineday + ", " 
            + hours + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "";
        expText.text = exp + "/" + Data.expGaps[level];
        expSlider.value = exp / Data.expGaps[level];
        levelText.text = level + "";

        
        for (int i = 0; i < 6; i++)
        {
            activityTimeflowTexts[i].text = Data.activityTimeValue[i, upgradeProgress[i]] + "X";
            activityClickTexts[i].text = Data.activityClickValue[i, upgradeProgress[i]] + " sec";
            activityDurationTexts[i].text = Data.activityDuration[i, upgradeProgress[i]] + "H";
            activityCooltimeTexts[i].text = Data.activityCooldown[i, upgradeProgress[i]] + "H";

            upgradeTexts[i].text = Data.upgradeValue[i, upgradeProgress[i]] + " to Upgrade";
        }
           
        
    }
    private void Update()
    {
        timer.text = (hours * 3600 + minutes * 60 + seconds) + "";
        secondCheck += Time.deltaTime;
        
        //Seconds Flow
        if((activityNum != -1 && secondCheck >= 1.0 / Data.activityTimeValue[activityNum, upgradeProgress[activityNum]]) || secondCheck >= 1)
        {
            if (activityNum != -1)
            {
                TimeFlows(1);

                //Acitity End
                if(activateDuration[activityNum] <= (hours * 3600 + minutes * 60 + seconds))
                {
                    Debug.Log("Activity Finished");
                    activityEnabled[activityNum] = false;
                    activateTexts[activityNum].text = "Activate";
                    activityNum = -1;
                }

            }
            else
            {
                TimeFlows(1);
            }


            //Cooldown End
            for (int i = 0; i < 6; i++)
            {
                if (activityEnabled[i] == false)
                {
                    if (activityCooldown[i] <= (hours * 3600 + minutes * 60 + seconds))
                    {
                        activityEnabled[i] = true;
                    }
                }
            }

            //Sunset
            if (sunset)
            {
                //0.5 hour
                tempColor = basicColor - (basicColor - sunsetColor) * ((minutes * 60 + seconds) / (float)1800) ;
                windowImage.GetComponent<SpriteRenderer>().color = tempColor;
                if (hours == 19 && minutes == 30 && seconds == 0)
                {
                    twilight = true;
                    sunset = false;
                }

            }
            else if (hours == 19 && minutes == 0 && seconds >= 0)
            {
                sunset = true;
                tempColor = windowImage.GetComponent<SpriteRenderer>().color;
            }

            // 1.5 hour
            if (twilight)
            {
                tempColor = sunsetColor - (sunsetColor - twilightColor) * ((minutes * 60 + seconds) /(float) 5400);
                windowImage.GetComponent<SpriteRenderer>().color = tempColor;
                if (hours == 21)
                    twilight = false;
            }

            //Init
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

            if (exp >= Data.upgradeValue[i, upgradeProgress[i]])
            {
                upgradeButtons[i].enabled = true;
                upgradeButtons[i].GetComponent<Image>().color = Color.white;
            }

            else
            {
                upgradeButtons[i].enabled = false;
                upgradeButtons[i].GetComponent<Image>().color = Color.gray;
            }
                
        }

        if (exp == Data.expGaps[level]) levelupButton.enabled = true;
        else levelupButton.enabled = false;
    }
    public void Clicked()
    {
        if(activityNum == -1)
        {
            TimeFlows(1);
        }
        else
        {
            TimeFlows(Data.activityClickValue[activityNum, upgradeProgress[activityNum]]);
        }
        
        GainExp(1);
    }

    public void ActivityON(int num)
    {
        Debug.Log("Activity ON" + num);
        if(activityNum != -1)
        {
            
            //Cooldown Recalculate
            activityCooldown[activityNum] -= activateDuration[activityNum] - (hours * 3600 + minutes * 60 + seconds);
            
            activateDuration[activityNum] = 0;
            
            if (activityNum == num)
            {
                activityNum = -1;
                activityText.text = "";
                activateTexts[num].text = "Activate";
                activityEnabled[num] = false;
            }
            else
            {
                //past
                activateTexts[activityNum].text = "Activate";
                activityEnabled[activityNum] = false;

                activityNum = num;
                activityText.text = Data.activityName[num];
                AddDurationCooldown(num);
                activateTexts[num].text = "Stop";
            }
        }
        else
        {
            activityNum = num;
            activityText.text = Data.activityName[num];
            AddDurationCooldown(num);
            activateTexts[num].text = "Stop";
        }
        
    }

    public void UpgradeON(int num)
    {
        GainExp(-Data.upgradeValue[num, upgradeProgress[num]]);
        upgradeProgress[num]++;
        upgradeTexts[num].text = Data.upgradeValue[num, upgradeProgress[num]] + " to Upgrade";
        activityTimeflowTexts[num].text = Data.activityTimeValue[num, upgradeProgress[num]] + "X";
        activityClickTexts[num].text = Data.activityClickValue[num, upgradeProgress[num]] + " sec";
        activityDurationTexts[num].text = Data.activityDuration[num, upgradeProgress[num]] + "H";
        activityCooltimeTexts[num].text = Data.activityCooldown[num, upgradeProgress[num]] + "H";
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
        if (exp + ammount <= Data.expGaps[level])
            exp += ammount;
        else
            exp = Data.expGaps[level];

        expSlider.value = exp / (float) Data.expGaps[level];
        expText.text = exp + "/" + Data.expGaps[level];
    }

    public void AddDurationCooldown(int activityNum)
    {
        activateDuration[activityNum] = hours * 3600 + minutes * 60 + seconds;
        //Duration Check
        activateDuration[activityNum] += (int) (Data.activityDuration[activityNum, upgradeProgress[activityNum]] * 3600);

        //Cooldown Check
        activityCooldown[activityNum] = activateDuration[activityNum]
                + (int) (Data.activityCooldown[activityNum, upgradeProgress[activityNum]] * 3600);
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

    public void LevelUP()
    {
        level++;
        exp = 0;
        expSlider.value = exp / (float)Data.expGaps[level];
        expText.text = exp + "/" + Data.expGaps[level];
        levelText.text = level + "";
    }

    public void ToMenu()
    {
        PushData();
        gm.quarantineData = quarantineData;
        gm.ToMenu();
    }
}
