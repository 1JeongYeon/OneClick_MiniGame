using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public struct BulletData
{
    public float delayTime;
    public int maxBullet;
    public string information;
    public string soundEffect;
    public int damage;
    public float bulletSpeed;
    public GameObject bullet;
    
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private bool shootAble = false;
    private float delayTime = 0f;
    private Transform playerTrans;
    private Transform bulletTrans;
    private float dis;

    private void Start()
    {
        delayTime = bulletData.delayTime;

        playerTrans = FindObjectOfType<Player>().transform;
    }
    public abstract void InitSetting(TMP_Text info);

    public virtual void Shooting(Transform muzzle, TMP_Text soundText)
    {
        if (shootAble == true)
        {
            var bullet = Instantiate(bulletData.bullet);
            bullet.transform.position = muzzle.position; // ������

            bulletTrans = bullet.transform;
            dis = Vector3.Distance(bulletTrans.position, playerTrans.position);
            bulletData.bullet.transform.rotation = Quaternion.LookRotation(transform.position - playerTrans.position);

            DiffusionMissileMoveOperation(bullet.GetComponent<Bullet>());

            var fireEffect = Instantiate(soundText);
            fireEffect.transform.position = muzzle.position + new Vector3(0, 3f, 0);
            fireEffect.text = bulletData.soundEffect;

            shootAble = false;

            // �Ѿ� �ִ�ġ�� �߿����� ��� �ؾ� ��. ���带 �ð����� ���� �ƴϸ� �� �Ѿ��� ������ �� �������� �� �������� ��� ����
            bulletData.maxBullet--;
        }

        if (shootAble == false)
        {
            delayTime += Time.deltaTime;
            if (delayTime >= bulletData.delayTime)
            {
                DestroyImmediate(soundText);
                shootAble = true;
                delayTime = 0f;
            }
        }
    }

    // instantiate�Ǵ� �Ѿ��� �� ������ ��� �־�� �ϴµ� �׷��� ���ؼ� null�� �ߴ���
    private void DiffusionMissileMoveOperation(Bullet _bullet)
    {
        Debug.Log(" ����ź ");


        delayTime += Time.deltaTime;
        if (delayTime < 1f)
        {
            // 1�ʵ��� õõ�� ������ ����
            _bullet.bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * _bullet.bulletData.bulletSpeed, Space.Self);
        }
        else
        {
            // 1�� ���� Ÿ�ٹ������� lerp��ġ�̵�
            _bullet.bulletData.bulletSpeed += Time.deltaTime;
            float t = _bullet.bulletData.bulletSpeed / dis;

            bulletTrans.position = Vector3.LerpUnclamped(bulletTrans.position, playerTrans.position, t);
        }
        // �������Ӹ��� Ÿ�ٹ������� ��ź�� �������ٲߴϴ�
        //Ÿ����ġ - ��ź��ġ = ��ź�� Ÿ�����׼��� ����
        Vector3 directionV3 = playerTrans.position - bulletTrans.position;
        Quaternion qua = Quaternion.LookRotation(directionV3);
        bulletTrans.rotation = Quaternion.Slerp(bulletTrans.rotation, qua, Time.deltaTime * 2.5f);
    }
}
