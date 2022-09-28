using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    public GameObject levelUpEffect;
    private void Start()
    {
        levelUpEffect.SetActive(false);
    }

    private void OnMouseEnter()
    {
        levelUpEffect.SetActive(true);
    }

    private void OnMouseExit()
    {
        levelUpEffect.SetActive(false);
    }
}
