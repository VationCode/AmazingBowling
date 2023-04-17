using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //레이어는 물리연산때 사용
    public LayerMask whatIsProp; //태그는 1:1만 가능하지만 레이어는 다중 선택이 가능
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f; //폭발반경안의 물건들에게 힘주기

    public float lifeTime = 10f; //볼생성후 파괴
    public float explosionRadius = 20f; //폭발반경

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders=  Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp) ;

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius); //힘, 지점, 반경
            //리지드바디의 AddExplosionForce는 폭발의 원점을 지정해주면 내가 스스로 그폭발물과 얼만큼 떨어져있는지 계산해서 얼만큼 튕겨나가야할지 자동으로적용

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalulateDamage(colliders[i].transform.position);
            targetProp.TakeDamage(damage); //prop 데미지 전달
        }
        explosionParticle.transform.parent = null; //볼 없어지면 파티클도 없어지기에 볼인 부모로부터 독립
        
        explosionParticle.Play();
        explosionAudio.Play();

        Gamemanager.instance.OnBallDestory();

        Destroy(explosionParticle.gameObject,explosionParticle.duration); // explosionParticle.duration 파티클 끄타는 시간
        Destroy(gameObject);
    }

    //안으로 들어갈수록 데미지가 높아짐
    //피해줄 데미지 계산
    private float CalulateDamage(Vector3 targetPosition)
    {
        //나로부터 타겟이 어디에 있는지 확인하고(거리 계산)
        //이후 반경에서부터 안으로 얼마나 들어가 있는지 계산
        //(반지름 - 타겟거리)/반지름 = 안쪽으로의 퍼센테이지
        Vector3 explosionToTarget = targetPosition - transform.position; // 나에서부터 타겟으로의 방향과 거리 (안->밖 방향)

        float distance = explosionToTarget.magnitude; //피타고라스로 벡터를 길이로 계산 a^2 +b^2 = c^2 

        float edgeToCenterDistance = explosionRadius - distance; //원의 반지름 끝에서부터 얼마나 안으로 들어갔는지 (밖->안 방향)

        float percentage = edgeToCenterDistance / explosionRadius; //안쪽으로 들어간 거리 / 반경

        float damage = maxDamage * percentage;

        damage = Mathf.Max(0, damage); 
        //오브젝의 위치(중심점)와는 다르게 반경 밖의 부분에서 콜라이더가 반경에 걸치는 경우
        //위의 계산으로 하면 데미지가 -가되기에(체력이 오히려 회복) 이를 방지하기 위해서
        //맥스값은 무조건 0 또는 데미지로(0보다 큰값이 들어오면 데미지로, 0보다 작은 값 들어오면 0으로)

        return damage;
    }
}
