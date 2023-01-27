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
    public GameObject bullet;
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    public bool shootAble = false;
    public float delayTime = 0f;

    public abstract void InitSetting();

    public virtual void Shooting(Transform muzzle, TMP_Text soundText)
    {
        if (shootAble == true)
        {
            var bullet = Instantiate(bulletData.bullet);
            bullet.transform.position = muzzle.position;

            var fireEffect = Instantiate(soundText);
            fireEffect.transform.position = muzzle.position + new Vector3(0, 1.5f, 0);
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
}
