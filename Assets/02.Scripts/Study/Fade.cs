using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;
    void Start()
    {
        //StartCoroutine("FadeIn"); //���������� ���� �� ����StopCoroutine
        StartCoroutine(FadeIn()); //���� �� ����, ������ ���� �� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �ڷ�ƾ�� ���ð��� ������
    // �񵿱�
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
