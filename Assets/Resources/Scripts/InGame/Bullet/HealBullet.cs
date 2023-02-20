using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBullet : Bullet
{
    public override void Hit()
    {
        Player.isHit = true;
        StatusController.Instance.IncreaseHP(-30);
        GameManager.Instance.score += 5;
    }

    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = 1f;
        bulletData.information = "현재 총알 : 체력회복총알";
        bulletData.soundEffect = "Yummy";
        bulletData.maxBullet = 3;
        bulletData.fixedMaxBullet = 3;
        bulletData.damage = -30; // 체력 회복 위함
        bulletData.bulletSpeed = 20f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/HealBullet");
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
