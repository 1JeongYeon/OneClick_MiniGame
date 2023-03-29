using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    private void Start()
    {
        InitSetting();
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
        bulletData.information = "ÇöÀç ÃÑ¾Ë : ±âº» ÃÑ¾Ë";
        bulletData.damage = 20;
        bulletData.bulletSpeed = 10f;
        BulletDifficultyAdjustment();
    }

    public override void BulletDifficultyAdjustment()
    {
        if (GameManager.Instance.stageLevel >= 2)
        {
            bulletData.bulletSpeed += Random.Range(2 , 5);
        }
    }
}
