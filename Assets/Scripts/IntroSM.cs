using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSM : MonoBehaviour
{
    public Image part1, part2;
    public List<Sprite> introSpriteList;
    void Start()
    {
        StartCoroutine(IntroMovie());
    }

    IEnumerator IntroMovie()
    {
        yield return new WaitForSeconds(1);
        for(int i = 0; i < introSpriteList.Count; i++)
        {
            if (i % 2 == 0)
            {
                part1.sprite = introSpriteList[i];
                FadeManager.FadeIn(part1, 1);
                yield return new WaitForSeconds(1);
            }
            else
            {
                part2.sprite = introSpriteList[i];
                FadeManager.FadeIn(part2, 1);
                yield return new WaitForSeconds(1);

                FadeManager.FadeOut(part1, 1);
                FadeManager.FadeOut(part2, 1);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
