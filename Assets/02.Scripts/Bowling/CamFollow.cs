using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle, Ready, Tracking
    }

    private State state //�Լ���� ������Ƽ ���� ������ �⺻������ ����ó�� ����Ͽ� �����ϰ� ���̱� ���� 
    {
        set
        {
            switch(value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    public Transform target;

    public float smoothTime = 0.2f;

    private Vector3 movingVelocity;
    private Vector3 targetPosition;

    private Vector3 lastMovingVelocity;

    private Camera cam;
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position,targetPosition, ref lastMovingVelocity, smoothTime);//������ġ, Ÿ����ġ, ���� �ð��� �����ϸ� �������ϰ� ���� ��ȯ ����� �� 

        transform.position = smoothPosition ;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
