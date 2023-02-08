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
    private Rigidbody2D bulletRigidBody;
    private float rotateSpeed = 2f;
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
            // �Ѿ��� ����
            var bullet = Instantiate(bulletData.bullet, muzzle.position, Quaternion.identity);
            // �Ѿ� �߷� �ο�
            bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            // ����
            Vector3 dir = (playerTrans.position - bullet.transform.position).normalized;
            // ����
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotTarget = Quaternion.AngleAxis(-angle, Vector3.forward);
            bullet.transform.rotation = Quaternion.Slerp(bullet.transform.rotation, rotTarget, Time.deltaTime * rotateSpeed);
            bulletRigidBody.velocity = new Vector2(dir.x * bulletData.bulletSpeed, dir.y * bulletData.bulletSpeed); 
            

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
                shootAble = true;
                delayTime = 0f;
            }
        }
    }

    
    // �������� ������ �� ���̵��� �ֱ� ���� �Ѿ� �����ӿ� ������ ���� �ڵ� ���� �Ⱦ����̴�.
    private void DiffusionMissileMoveOperation(Bullet _bullet)
    {
        Debug.Log(" ����ź ");
        if (playerTrans == null)
        {
            return;
        }
        float waitForMovingBullet = 0;
        waitForMovingBullet += Time.deltaTime;
        if (waitForMovingBullet < 1f)
        {
            // 1�ʵ��� õõ�� ������ ����
            _bullet.bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * _bullet.bulletData.bulletSpeed, Space.World);
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
