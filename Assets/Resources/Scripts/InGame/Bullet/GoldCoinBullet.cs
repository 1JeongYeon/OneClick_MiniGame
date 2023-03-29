using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBullet : Bullet
{
    private void Start()
    {
        InitSetting();
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
        bulletData.damage = 0; // �� �԰� ������ �޸� ������ϱ� 
        bulletData.bulletSpeed = 15f;
        BulletDifficultyAdjustment();
    }

    public override void BulletDifficultyAdjustment()
    {
        if (GameManager.Instance.stageLevel >= 2)
        {
            bulletData.bulletSpeed += Random.Range(-5, 5);
        }
    }
}
