using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBullet : Bullet
{
    public override void InitSetting()
    {
        bulletData.delayTime = 0.5f;
        bulletData.information = "���� �Ѿ� : ü��ȸ���Ѿ�";
        bulletData.soundEffect = "�ܲ�";
        bulletData.maxBullet = -1;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/HpItem");
    }

    /*public override void Shooting()
    {
        base.Shooting();
    }*/
}
