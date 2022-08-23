using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    
    public static IEnumerator FadeIn(SpriteRenderer spriteRenderer, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = spriteRenderer.color;
        tempColor.a = 0;
        spriteRenderer.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a += Time.deltaTime / time;
            spriteRenderer.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeOut(SpriteRenderer spriteRenderer, float time)
    {
        float timeCheck = 0;
        Color tempColor;
        tempColor = spriteRenderer.color;
        tempColor.a = 1;
        spriteRenderer.color = tempColor;
        while (timeCheck < time)
        {
            timeCheck += Time.deltaTime;
            tempColor.a -= Time.deltaTime / time;
            spriteRenderer.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

    //public static IEnumerator FadeInText
}
