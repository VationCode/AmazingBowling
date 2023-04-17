// ��ź���� �ı��ô� ������Ʈ
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
            
            Destroy(instanceParticleObj.gameObject,instanceParticleObj.duration); //������ ��ƼŬ ����
            gameObject.SetActive(false); //prop ������Ʈ�� ������ �ƴ� ���ΰ� �ٽ� ��Ȱ��(������Ʈ Ǯ���� ���)
        }
    }
}
