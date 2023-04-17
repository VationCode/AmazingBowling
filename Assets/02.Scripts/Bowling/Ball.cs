using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //���̾�� �������궧 ���
    public LayerMask whatIsProp; //�±״� 1:1�� ���������� ���̾�� ���� ������ ����
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f; //���߹ݰ���� ���ǵ鿡�� ���ֱ�

    public float lifeTime = 10f; //�������� �ı�
    public float explosionRadius = 20f; //���߹ݰ�

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

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius); //��, ����, �ݰ�
            //������ٵ��� AddExplosionForce�� ������ ������ �������ָ� ���� ������ �����߹��� ��ŭ �������ִ��� ����ؼ� ��ŭ ƨ�ܳ��������� �ڵ���������

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalulateDamage(colliders[i].transform.position);
            targetProp.TakeDamage(damage); //prop ������ ����
        }
        explosionParticle.transform.parent = null; //�� �������� ��ƼŬ�� �������⿡ ���� �θ�κ��� ����
        
        explosionParticle.Play();
        explosionAudio.Play();

        Gamemanager.instance.OnBallDestory();

        Destroy(explosionParticle.gameObject,explosionParticle.duration); // explosionParticle.duration ��ƼŬ ��Ÿ�� �ð�
        Destroy(gameObject);
    }

    //������ ������ �������� ������
    //������ ������ ���
    private float CalulateDamage(Vector3 targetPosition)
    {
        //���κ��� Ÿ���� ��� �ִ��� Ȯ���ϰ�(�Ÿ� ���)
        //���� �ݰ濡������ ������ �󸶳� �� �ִ��� ���
        //(������ - Ÿ�ٰŸ�)/������ = ���������� �ۼ�������
        Vector3 explosionToTarget = targetPosition - transform.position; // ���������� Ÿ�������� ����� �Ÿ� (��->�� ����)

        float distance = explosionToTarget.magnitude; //��Ÿ��󽺷� ���͸� ���̷� ��� a^2 +b^2 = c^2 

        float edgeToCenterDistance = explosionRadius - distance; //���� ������ ���������� �󸶳� ������ ������ (��->�� ����)

        float percentage = edgeToCenterDistance / explosionRadius; //�������� �� �Ÿ� / �ݰ�

        float damage = maxDamage * percentage;

        damage = Mathf.Max(0, damage); 
        //�������� ��ġ(�߽���)�ʹ� �ٸ��� �ݰ� ���� �κп��� �ݶ��̴��� �ݰ濡 ��ġ�� ���
        //���� ������� �ϸ� �������� -���Ǳ⿡(ü���� ������ ȸ��) �̸� �����ϱ� ���ؼ�
        //�ƽ����� ������ 0 �Ǵ� ��������(0���� ū���� ������ ��������, 0���� ���� �� ������ 0����)

        return damage;
    }
}
