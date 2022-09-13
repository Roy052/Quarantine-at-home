using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    //music
    readonly public static string[] musicList = { "" };

    //card
    readonly public static string[] cardNameList = { "Double Tap" };
    readonly public static string[] cardEffectList = { "One click treated as double click." };

    //gap
    readonly public static int[] expGaps = { 50, 100, 200, 500, 1000, 1500, 3000, 5000 };

    //Activity
    readonly public static string[] activityName
        = { "Watching Videos", "Playing Games", "Cleaning", "Workout", "Reading Books", "Playing Music" };
    readonly public static int[,] activityTimeValue = {
        { 3, 4, 5, 5, 6 },
        { 5, 5, 6, 6, 7 },
        { 1, 2, 2, 2, 3 },
        { 1, 3, 5, 6, 7 },
        { 2, 4, 7, 11, 15 },
        { 4, 4, 5, 5, 5 } };
    readonly public static int[,] activityClickValue = { 
        { 2, 2, 3, 4, 4 }, 
        { 7, 8, 8, 9, 9},
        { 10, 10, 11, 12, 12 },
        { 15, 20, 24, 27, 29  },
        { 3, 3, 3, 3, 3 },
        { 5, 8, 11, 14, 17 } };
    readonly public static float[,] activityDuration = { 
        { 2.5f, 2.8f, 3.2f, 3.5f, 3.7f },
        { 1, 1.2f, 1.4f, 1.6f, 1.8f },
        { 2, 2, 2, 2, 2 },
        { 1, 1.2f, 1.4f, 1.6f, 2 },
        { 5, 6, 7, 8, 10 },
        { 2, 2.2f, 2.4f, 2.7f, 3 } };
    readonly public static float[,] activityCooldown = {
        { 1, 1, 1, 1, 1 },
        { 1, 0.9f, 0.8f, 0.7f, 0.6f },
        { 24, 24, 24, 24, 24 },
        { 6, 6, 6, 6, 6 },
        { 24, 24, 24, 24, 24 },
        { 6, 6, 5, 5, 4 } };

    //Upgrade
    readonly public static int[,] upgradeValue = { 
        { 10, 20, 40, 60, 80 }, 
        { 30, 50, 70, 90, 110 }, 
        { 45, 85, 135, 215, 305 }, 
        { 250, 450, 650, 1050, 1550 }, 
        { 550, 770, 1100, 1780, 3050 }, 
        { 400, 600, 800, 1200, 1600 } };
}
