using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    static public void SaveIntoJson(int day, int exp, int level, int[] upgrade, int hours, int minutes, int seconds, int[,] activityDuration, int[,] activityCooldown, bool[] activityEnabled)
    {
        QuarantineData quarantineData = new QuarantineData();
        quarantineData.quarantineday = day;
        quarantineData.exp = exp;
        quarantineData.level = level;
        quarantineData.upgradeProgress = upgrade;
        quarantineData.hours = hours;
        quarantineData.minutes = minutes;
        quarantineData.seconds = seconds;
        quarantineData.activateDuration = activityDuration;
        quarantineData.activityCooldown = activityCooldown;
        quarantineData.activityEnabled = activityEnabled;
        string quarantine = JsonUtility.ToJson(quarantineData);
        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", quarantine);
    }

     static public QuarantineData LoadFromJson()
    {
        try
        {
            string path = Application.persistentDataPath + "/SaveData.json";
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
    public int quarantineday = -1;
    public int exp, level;
    public int[] upgradeProgress = new int[6];

    //Time
    public int hours, minutes, seconds;

    //DayActivity
    public int[,] activateDuration = new int[6, 3];
    public int[,] activityCooldown = new int[6, 3];
    public bool[] activityEnabled = new bool[6];
}
