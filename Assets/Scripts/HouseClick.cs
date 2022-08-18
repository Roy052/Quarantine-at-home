using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseClick : MonoBehaviour
{
    public HouseSM houseSM;
    bool limitCheck = true;
    float limitTimeCheck = 0;

    private void Update()
    {
        limitTimeCheck += Time.deltaTime;
        if(limitCheck == false && limitTimeCheck >= 0.1f)
        {
            limitCheck = true;
        }
    }
    private void OnMouseDown()
    {
        if (limitCheck)
        {
            houseSM.Clicked();
        }
    }
}
