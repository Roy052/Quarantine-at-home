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
    void Start()
    {
        StartCoroutine(IntroMovie());
        
    }

    IEnumerator IntroMovie()
    {
        gm.mainBGMLoader.clip = introBGM;
        StartCoroutine(gm.BGMON(gm.mainBGMLoader, 1));
        yield return new WaitForSeconds(1);
        for(int i = 0; i < introSpriteList.Count; i++)
        {
            if (i % 2 == 0)
            {
                part1.sprite = introSpriteList[i];
                StartCoroutine( FadeManager.FadeIn(part1, 2));
                yield return new WaitForSeconds(2);
            }
            else
            {
                part2.sprite = introSpriteList[i];
                StartCoroutine(FadeManager.FadeIn(part2, 2));
                yield return new WaitForSeconds(2);

                StartCoroutine(FadeManager.FadeOut(part1, 2));
                StartCoroutine(FadeManager.FadeOut(part2, 2));
                yield return new WaitForSeconds(2.1f);
            }
        }
        StartCoroutine(gm.BGMOFF(gm.mainBGMLoader, 1));
        yield return new WaitForSeconds(1);
        gm.ToMenu();
    }
    private void OnMouseDown()
    {
        gm.mainBGMLoader.Stop();
        gm.ToMenu();
    }
}
