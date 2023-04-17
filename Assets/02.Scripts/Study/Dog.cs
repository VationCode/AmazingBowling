using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public string nickName;
    public float weight;
    public static int count = 0;

    public void Awake()
    {
        count = count+1;
    }
    public void Bark()
    {
        Debug.Log(nickName + " : Bark");
    }

    private void Start()
    {
        Bark();
    }

    public static void ShowAnimalType()
    {
        Debug.Log("�̰��� ���Դϴ�.");
    }
}
