using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBullet : Bullet
{
    private void Start()
    {
        InitSetting();
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
        bulletData.information = "현재 총알 : 체력회복총알";
        bulletData.damage = 30; // 체력 회복 위함
        bulletData.bulletSpeed = 20f;
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
