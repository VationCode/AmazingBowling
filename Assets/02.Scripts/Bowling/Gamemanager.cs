using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class Gamemanager : MonoBehaviour
{
    #region SingleTon
    public static Gamemanager instance;
    #endregion

    public UnityEvent onRest;

    public GameObject readyPannel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI messageText;

    public bool isRoundActive = false;

    public ShooterRotator shooterRotator;
    public CamFollow cam;
    int score;
    private void Awake()
    {
        instance = this;
        UpdateBestScore();
        UpdateUI();
    }

    void Start()
    {
        StartCoroutine("RoundRoutine");
    }


    public void AddScore(int newScore)
    {
        score += newScore;
    }
    
    void UpdateBestScore()
    {
        if(GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore",score);
        }
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }
    // Update is called once per frame
    
    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "Best Score : " + GetBestScore();
        //UpdateBestScore();
    }

    public void OnBallDestory()
    {
        UpdateUI();
        isRoundActive = false;
    }
    public void Reset()
    {
        score = 0;
        UpdateUI();
        StartCoroutine("RoundRoutine");
        //라운드를 다시 처음부터 시작
    }

    IEnumerator RoundRoutine()
    {
        //Ready
        onRest.Invoke();
        readyPannel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;

        yield return new WaitForSeconds(3f);

        //Play
        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while(isRoundActive == true)
        {
            yield return null;
        }

        //End
        readyPannel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait FOr Next Round...";

        yield return new WaitForSeconds(3f);
        Reset();

    }
}
