using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBullet : Bullet
{
    public override void InitSetting(TMPro.TMP_Text txt)
    {
        bulletData.delayTime = 0.3f;
        bulletData.information = "���� �Ѿ� : ü��ȸ���Ѿ�";
        bulletData.soundEffect = "DOKI DOKI";
        bulletData.maxBullet = 3;
        bulletData.damage = -50; // ü�� ȸ�� ����
        bulletData.bulletSpeed = 10f;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/HpItem");
    }

    public override void Shooting(Transform muzzle, TMPro.TMP_Text effect)
    {
        base.Shooting(muzzle, effect);
    }
}
