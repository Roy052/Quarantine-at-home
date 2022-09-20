using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSM : MonoBehaviour
{

    GameManager gm;
    public Sprite[] endSprites;
    public GameObject endImage;
    public AudioClip endBGM;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.mainBGMLoader.clip = endBGM;
        StartCoroutine(gm.BGMON(gm.mainBGMLoader, 1));
        StartCoroutine(EndMovie());

    }

    IEnumerator EndMovie()
    {
        yield return new WaitForSeconds(4);
        endImage.GetComponent<SpriteRenderer>().sprite = endSprites[0];
        yield return new WaitForSeconds(4);
        endImage.GetComponent<SpriteRenderer>().sprite = endSprites[1];

        yield return new WaitForSeconds(4);

        StartCoroutine( FadeManager.FadeOut(endImage.GetComponent<SpriteRenderer>(), 2));
        yield return new WaitForSeconds(2);
        gm.ToMenu();
    }

    private void OnMouseDown()
    {
        gm.ToMenu();
    }
}
