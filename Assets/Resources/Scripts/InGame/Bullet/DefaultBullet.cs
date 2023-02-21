using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    public override void Crushed()
    {
        throw new System.NotImplementedException();
    }

    public override void Hit()
    {
        Player.isHit = true;
        StatusController.Instance.DecreaseHP(bulletData.damage);
    }

    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = .5f;
        bulletData.information = "ÇöÀç ÃÑ¾Ë : ±âº» ÃÑ¾Ë";
        bulletData.soundEffect = "BANG!";
       /* bulletData.maxBullet = -1;
        bulletData.fixedMaxBullet = -2;*/
        bulletData.damage = 20;
        bulletData.bulletSpeed = 10f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/DefaultBullet");
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
