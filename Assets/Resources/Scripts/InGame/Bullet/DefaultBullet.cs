using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    public override void InitSetting()
    {
        bulletData.delayTime = 1f;
        bulletData.information = "���� �Ѿ� : �⺻ �Ѿ�";
        bulletData.soundEffect = "��!";
        bulletData.maxBullet = -1;
        bulletData.bullet = Resources.Load<GameObject>("Prefabs/Bullet/DefaultBullet");
    }

    /*public override void Shooting()
    {
        base.Shooting();
    }*/
}
