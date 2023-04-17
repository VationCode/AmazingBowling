using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region SingleTon
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();

                if(instance == null)
                {
                    GameObject container = new GameObject("ScoreManager");
                    instance = container.AddComponent<ScoreManager>();
                }
            }

            return instance;
        }
    }
    #endregion
    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int _score)
    {
        score = score + _score;
    }
    void Start()
    {
        if(instance != null)
        {
            if(instance != this) //�ν��Ͻ��� �ְ� ���� �ƴ϶�� ���� 2��° �̻� �����̱⿡ ����
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
