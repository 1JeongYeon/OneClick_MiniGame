using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBullet : Bullet
{
    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = 0.5f;
        bulletData.information = "현재 총알 : 골드코인총알";
        bulletData.soundEffect = "Bling Bling~";
        bulletData.maxBullet = 30;
        bulletData.fixedMaxBullet = 30;
        bulletData.damage = 0; // 돈 먹고 데미지 달면 서러우니까
        bulletData.bulletSpeed = 15f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/GoldCoinBullet");
    }

    public override void Shooting(Transform muzzle, TMPro.TMP_Text effect)
    {
        base.Shooting(muzzle, effect);
    }
}
