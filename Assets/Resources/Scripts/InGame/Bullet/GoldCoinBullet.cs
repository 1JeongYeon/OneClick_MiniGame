using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBullet : Bullet
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
        GameManager.Instance.coin += 1;
        GameManager.Instance.score += 3;
    }

    public override void InitSetting()
    {
        base.InitSetting();
        bulletData.delayTime = 0.5f;
        bulletData.information = "���� �Ѿ� : ��������Ѿ�";
        bulletData.soundEffect = "Bling Bling~";
       /* bulletData.maxBullet = 30;
        bulletData.fixedMaxBullet = 30;*/
        bulletData.damage = 0; // �� �԰� ������ �޸� ������ϱ� 
        bulletData.bulletSpeed = 15f;
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
