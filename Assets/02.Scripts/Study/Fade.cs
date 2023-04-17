using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;
    void Start()
    {
        //StartCoroutine("FadeIn"); //인위적으로 멈출 수 있음StopCoroutine
        StartCoroutine(FadeIn()); //멈출 수 없음, 성능이 조금 더 좋음
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 코루틴은 대기시간을 가진다
    // 비동기
    IEnumerator FadeIn()
    {
        Color startColor = fadeImage.color;
        for (int i = 0; i < 100; i++)
        {
            startColor.a = startColor.a - 0.01f;
            fadeImage.color = startColor;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
