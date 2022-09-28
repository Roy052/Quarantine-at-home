using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSM : MonoBehaviour
{
    public Image part1, part2;
    public List<Sprite> introSpriteList;
    public GameManager gm;
    public AudioClip introBGM;
    public bool canSkip = false;
    public Text covidText;
    void Start()
    {
        StartCoroutine(IntroMovie());
    }

    IEnumerator IntroMovie()
    {
        gm.mainBGMLoader.clip = introBGM;
        StartCoroutine(gm.BGMON(gm.mainBGMLoader, 1));

        Color tempColor = covidText.color;
        while(tempColor.a <= 1)
        {
            tempColor.a += Time.deltaTime/3;
            covidText.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(gm.MainBGMLoad());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(gm.SideBGMLoad());
        while (tempColor.a >= 0)
        {
            tempColor.a -= Time.deltaTime/3;
            covidText.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < introSpriteList.Count; i++)
        {
            if (i % 2 == 0)
            {
                part1.sprite = introSpriteList[i];
                StartCoroutine( FadeManager.FadeIn(part1, 3));
                yield return new WaitForSeconds(3);
            }
            else
            {
                part2.sprite = introSpriteList[i];
                StartCoroutine(FadeManager.FadeIn(part2, 3));
                yield return new WaitForSeconds(3);

                StartCoroutine(FadeManager.FadeOut(part1, 3));
                StartCoroutine(FadeManager.FadeOut(part2, 3));
                yield return new WaitForSeconds(3.2f);
            }
            if(i == 0)
                canSkip = true;
        }
        
        StartCoroutine(gm.BGMOFF(gm.mainBGMLoader, 3.2f));
        yield return new WaitForSeconds(3.2f);
        gm.ToMenu();
    }
    private void OnMouseDown()
    {
        gm.mainBGMLoader.Stop();
        gm.ToMenu();
    }
}
