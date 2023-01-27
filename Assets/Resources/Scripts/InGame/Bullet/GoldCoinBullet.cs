using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBullet : Bullet
{
    public override void InitSetting()
    {
        bulletData.delayTime = 0.3f;
        bulletData.information = "���� �Ѿ� : ��������Ѿ�";
        bulletData.soundEffect = "����";
        bulletData.maxBullet = -1;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/GoldCoin");
    }

    /*public override void Shooting()
    {
        base.Shooting();
    }*/
}
