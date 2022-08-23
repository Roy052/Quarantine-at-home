using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayOverSM : MonoBehaviour
{
    public Text dayText;
    public GameObject moon, sun;

    GameManager gm;
    int day = 0;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        day = gm.quarantineday;
        dayText.text = "Day - " + (day - 1);
        StartCoroutine(DayOverEffect());
    }

    IEnumerator DayOverEffect()
    {
        StartCoroutine(FadeManager.FadeOut(moon.GetComponent<SpriteRenderer>(), 3));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeManager.FadeIn(sun.GetComponent<SpriteRenderer>(), 3));

        yield return new WaitForSeconds(3);
        dayText.text = "Day - " + day;

        yield return new WaitForSeconds(1);
        gm.QuarantineIn();
    }
}
