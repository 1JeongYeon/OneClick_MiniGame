using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
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
        StatusController.Instance.DecreaseHP(bulletData.damage);
    }

    public override void InitSetting()
    {
        base.InitSetting();
        bulletData.delayTime = .5f;
        bulletData.information = "���� �Ѿ� : �⺻ �Ѿ�";
        bulletData.soundEffect = "BANG!";
       /* bulletData.maxBullet = -1;
        bulletData.fixedMaxBullet = -2;*/
        bulletData.damage = 20;
        bulletData.bulletSpeed = 10f;
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
