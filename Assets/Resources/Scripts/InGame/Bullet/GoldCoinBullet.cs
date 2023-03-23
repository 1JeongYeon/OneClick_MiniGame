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
        bulletData.information = "ÇöÀç ÃÑ¾Ë : °ñµåÄÚÀÎÃÑ¾Ë";
        bulletData.damage = 0; // µ· ¸Ô°í µ¥¹ÌÁö ´Þ¸é ¼­·¯¿ì´Ï±î 
        bulletData.bulletSpeed = 15f;
        BulletDifficultyAdjustment();
    }

    public override void Shooting()
    {
        float waitTime = (bulletData.delayTime += Time.deltaTime);
        if (waitTime >= bulletData.delayTime)
        {
            base.Shooting();
        }
    }

    public override void BulletDifficultyAdjustment()
    {
        if (GameManager.Instance.stageLevel >= 2)
        {
            bulletData.bulletSpeed += Random.Range(-5, 5);
        }
    }
}
