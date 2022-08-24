using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayOverSM : MonoBehaviour
{
    public Text dayText;
    public Image background;
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
        float timeCheck = 0;
        Vector3 centerVector = new Vector3(0, -7, 0);
        float radius = 7;
        float degree = -90, degree1 = -90;
        float tempTime = 0;

        yield return new WaitForSeconds(1);
        while(timeCheck < 1.5f)
        {
            degree += Time.deltaTime * 180 / 3;
            float rad = Mathf.Deg2Rad * (degree);
            float x = radius * Mathf.Sin(rad);
            float y = radius * Mathf.Cos(rad);
            moon.transform.position = centerVector + new Vector3(x, y);

            if(timeCheck >= 1.5f)
            {
                degree1 += Time.deltaTime * 180 / 3;
                rad = Mathf.Deg2Rad * (degree1);
                x = radius * Mathf.Sin(rad);
                y = radius * Mathf.Cos(rad);
                sun.transform.position = centerVector + new Vector3(x, y);

                tempTime = Time.deltaTime / 1.5f;
                dayText.color -= new Color(tempTime, tempTime, tempTime, 0);
                background.color += new Color(tempTime, tempTime, tempTime, 0);
            }
            timeCheck += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        timeCheck = 0;
        while(timeCheck < 1.5f)
        {
            degree += Time.deltaTime * 180 / 3;
            float rad = Mathf.Deg2Rad * (degree);
            float x = radius * Mathf.Sin(rad);
            float y = radius * Mathf.Cos(rad);
            moon.transform.position = centerVector + new Vector3(x, y);

            degree1 += Time.deltaTime * 180 / 3;
            rad = Mathf.Deg2Rad * (degree1);
            x = radius * Mathf.Sin(rad);
            y = radius * Mathf.Cos(rad);
            sun.transform.position = centerVector + new Vector3(x, y);

            tempTime = Time.deltaTime / 1.5f;
            dayText.color -= new Color(tempTime, tempTime, tempTime, 0);
            background.color += new Color(tempTime, tempTime, tempTime, 0);
            timeCheck += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        dayText.text = "Day - " + day;

        yield return new WaitForSeconds(1);
        StartCoroutine( gm.QuarantineIn(3));
    }
}
