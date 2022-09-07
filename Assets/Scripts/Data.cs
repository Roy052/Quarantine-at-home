using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    //music
    readonly public static string[] musicList = { "" };

    //card
    readonly public static string[] cardNameList = { "Double Tap"};
    readonly public static string[] cardEffectList = { "One click treated as double click." };

    //gap
    readonly public static int[] expGaps = { 100, 200, 500, 1000, 1500, 3000, 5000 };

    //Activity
    readonly public static string[] activityName = { "Watching Videos", "Playing Games", "Cleaning", "", "", "" };
    readonly public static int[] activityTimeValue = { 3, 5, 1, 0, 0, 0 };
    readonly public static int[] activityClickValue = { 2, 10, 15, 0, 0, 0 };
    readonly public static float[] activityDuration = { 2.5f, 1, 2, 0, 0, 0 };
    readonly public static float[] activityCooldown = { 1, 0.5f, 24, 0, 0, 0 };

    //Upgrade
    readonly public static int[,] upgradeValue = { { 10, 20, 30 }, { 50, 100, 150 }, { 100, 200, 300 }, 
        { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
}
