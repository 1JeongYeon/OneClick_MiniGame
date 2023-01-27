using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;

    public Transform muzzle;
    void Start()
    {
        currentBullet.InitSetting();
    }

    void Update()
    {
        //
        //currentBullet.Shooting();   
    }
}
