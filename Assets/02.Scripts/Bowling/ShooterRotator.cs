using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    public enum RotateState
    {
        Idle, //0
        Vertical, //1
        Horizontal,
        Ready,
    }
    private RotateState state = RotateState.Idle;


    private int currentState = 0;

    public float horizontalRotateSpeed = 360f;
    public float verticalRotateSpeed = 360f;

    public BallShooter ballShooter;

    private void Start()
    {

    }

    private void Update()
    {
        switch (state)
        {
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true; //발사 준비가 되었을 때 켜기로
                }
                break;
            case RotateState.Ready:
                break;
        }

    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        state = RotateState.Idle;
        ballShooter.enabled = false;
    }
}
