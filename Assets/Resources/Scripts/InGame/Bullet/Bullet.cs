using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{
    public float delayTime;
    public int maxBullet;
    public string information;
    public string soundEffect;
    public GameObject bullet;
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    public abstract void InitSetting();

    public virtual void Shooting()
    {
        // 어떻게 할건지 구상중
    }
}
