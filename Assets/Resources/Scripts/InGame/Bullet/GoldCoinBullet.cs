using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBullet : Bullet
{
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

    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = 0.5f;
        bulletData.information = "ÇöÀç ÃÑ¾Ë : °ñµåÄÚÀÎÃÑ¾Ë";
        bulletData.soundEffect = "Bling Bling~";
       /* bulletData.maxBullet = 30;
        bulletData.fixedMaxBullet = 30;*/
        // bulletData.damage = 0; // µ· ¸Ô°í µ¥¹ÌÁö ´Þ¸é ¼­·¯¿ì´Ï±î // °Á Áö¿ò
        bulletData.bulletSpeed = 15f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/GoldCoinBullet");
    }

    public override void Shooting(Transform muzzle, TMPro.TMP_Text effect)
    {
        float waitTime = (bulletData.delayTime += Time.deltaTime);
        if (waitTime >= bulletData.delayTime)
        {
            base.Shooting(muzzle, effect);
        }
    }
}
