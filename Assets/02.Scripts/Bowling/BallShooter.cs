using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow cam;

    public Rigidbody ball;
    public Transform firePos;

    public Slider powerSlider;

    public AudioSource shootingAudio;

    public AudioClip fireClip;
    public AudioClip chargingClip;
    public float minForce = 15f;
    public float maxForce = 30f;

    public float chargingTime = 0.75f;

    private float currentForce;

    private float chargeSpeed;

    private bool isFired;

    private void OnEnable()
    {
        //라운드 넘길때마나 매번 초기화를 해야하기에 여기서 초기화
        currentForce = minForce;
        powerSlider.value = minForce;
        isFired = false;
    }

    private void Start()
    {
        //거 = 속 * 시
        //속 = 거/시 //초당 거리에 따른 속도
        chargeSpeed = (maxForce - minForce) / chargingTime; //누르고 있는 동안의 차징속도
    }
    private void Update()
    {
        if (isFired == true) return;

        powerSlider.value = minForce;

        if(currentForce >= maxForce && !isFired) //최대 차징 넘겼을 때 강제 발사
        {
            currentForce = maxForce;
            Fire();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            currentForce = minForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if(Input.GetButton("Fire1") && !isFired)
        {
            currentForce += (chargeSpeed * Time.deltaTime);
            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !isFired)
        {
            Fire();
        }

    }

    private void Fire()
    {
        isFired = true;
        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);

        ballInstance.velocity = currentForce * firePos.forward; //에드포스가아닌 속력으로 바로 전환한 이유는 가속도를 적용하지 않기위해

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }

}
