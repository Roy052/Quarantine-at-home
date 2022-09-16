using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSM : MonoBehaviour
{

    GameManager gm;
    private void OnMouseDown()
    {
        gm.ToMenu();
    }
}
