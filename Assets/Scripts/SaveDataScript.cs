using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    static public void SaveIntoJson(int day, int exp, int level, int[] upgrade, int hours, int minutes, int seconds, int[] activityDuration, int[] activityCooldown, bool[] activityEnabled)
    {
        //Basic Datas
        QuarantineData quarantineData = new QuarantineData();
        quarantineData.quarantineday = day;
        quarantineData.exp = exp;
        quarantineData.level = level;
        quarantineData.upgradeProgress = upgrade;
        quarantineData.hours = hours;
        quarantineData.minutes = minutes;
        quarantineData.seconds = seconds;

        //Activitys
        quarantineData.activateDuration = activityDuration;
        quarantineData.activityCooldown = activityCooldown;
        quarantineData.activityEnabled = activityEnabled;

        //Json
        string quarantine = JsonUtility.ToJson(quarantineData);
        File.WriteAllText("./Save/" + "SaveData.json", quarantine);
    }

    static public void SaveIntoJson(QuarantineData quarantineData)
    {
        QuarantineData saveData = quarantineData;
        string quarantine = JsonUtility.ToJson(quarantineData);
        File.WriteAllText("./Assets/Save/" + "SaveData.json", quarantine);
    }

     static public QuarantineData LoadFromJson()
    {
        try
        {
            string path = "./Assets/Save/SaveData.json";
            Debug.Log(path);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log(json);
                QuarantineData qd = JsonUtility.FromJson<QuarantineData>(json);
                return qd;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
        return default;
    }
}

[System.Serializable]
public class QuarantineData
{
    //Progress
    public int quarantineday;
    public int exp, level;
    public int[] upgradeProgress;

    //Time
    public int hours, minutes, seconds;

    //DayActivity
    public int[] activateDuration;
    public int[] activityCooldown;
    public bool[] activityEnabled;

    public QuarantineData()
    {
        quarantineday = 1;
        exp = 0;
        level = 0;
        upgradeProgress = new int[6] { 0, 0, 0, 0, 0, 0 };

        hours = 10;
        minutes = 0;
        seconds = 0;

        activateDuration = new int[6] { 0, 0, 0, 0, 0, 0 };
        activityCooldown = new int[6] { 0, 0, 0, 0, 0, 0 };
        activityEnabled = new bool[6] { true, true, true, true, true, true };
    }
}
