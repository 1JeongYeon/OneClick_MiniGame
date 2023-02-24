using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBullet : Bullet
{
    private void Start()
    {
        InitSetting();
        Shooting();
    }
    public override void Crushed()
    {
        throw new System.NotImplementedException();
    }

    public override void Hit()
    {
        Player.isHit = true;
        StatusController.Instance.IncreaseHP(bulletData.damage);
        GameManager.Instance.score += 5;
    }

    public override void InitSetting()
    {
        base.InitSetting();
        bulletData.delayTime = 1f;
        bulletData.information = "���� �Ѿ� : ü��ȸ���Ѿ�";
        bulletData.soundEffect = "Yummy";
       /* bulletData.maxBullet = 3;
        bulletData.damage = 30; // ü�� ȸ�� ����
        bulletData.fixedMaxBullet = 3;*/
        bulletData.bulletSpeed = 20f;
    }

    public override void Shooting()
    {
        float waitTime = (bulletData.delayTime += Time.deltaTime);
        if (waitTime >= bulletData.delayTime)
        {
            base.Shooting();
        }
    }
}
