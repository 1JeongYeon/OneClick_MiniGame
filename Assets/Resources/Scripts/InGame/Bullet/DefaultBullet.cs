using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = 1f;
        bulletData.information = "ÇöÀç ÃÑ¾Ë : ±âº» ÃÑ¾Ë";
        bulletData.soundEffect = "BANG!";
        bulletData.maxBullet = -1;
        bulletData.damage = 20;
        bulletData.bulletSpeed = 10f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/DefaultBullet");
    }

    public override void Shooting(Transform muzzle, TMPro.TMP_Text effect)
    {
        base.Shooting(muzzle, effect);
    }
}
