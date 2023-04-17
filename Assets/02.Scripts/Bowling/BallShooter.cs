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
        //���� �ѱ涧���� �Ź� �ʱ�ȭ�� �ؾ��ϱ⿡ ���⼭ �ʱ�ȭ
        currentForce = minForce;
        powerSlider.value = minForce;
        isFired = false;
    }

    private void Start()
    {
        //�� = �� * ��
        //�� = ��/�� //�ʴ� �Ÿ��� ���� �ӵ�
        chargeSpeed = (maxForce - minForce) / chargingTime; //������ �ִ� ������ ��¡�ӵ�
    }
    private void Update()
    {
        if (isFired == true) return;

        powerSlider.value = minForce;

        if(currentForce >= maxForce && !isFired) //�ִ� ��¡ �Ѱ��� �� ���� �߻�
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

        ballInstance.velocity = currentForce * firePos.forward; //�����������ƴ� �ӷ����� �ٷ� ��ȯ�� ������ ���ӵ��� �������� �ʱ�����

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }

}
