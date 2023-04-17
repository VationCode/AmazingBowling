// 폭탄으로 파괴시는 오브젝트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle;
    public float hp = 10f;

  
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            ParticleSystem instanceParticleObj = Instantiate(explosionParticle, transform.position, transform.rotation);

            AudioSource explosionAudio = instanceParticleObj.GetComponent<AudioSource>();
            explosionAudio.Play();

            Gamemanager.instance.AddScore(score);
            
            Destroy(instanceParticleObj.gameObject,instanceParticleObj.duration); //생성된 파티클 제거
            gameObject.SetActive(false); //prop 오브젝트는 삭제가 아닌 꺼두고 다시 재활용(오브젝트 풀링과 비슷)
        }
    }
}
