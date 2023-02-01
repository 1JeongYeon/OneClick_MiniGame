using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // 마지막으로 쏘았던 총알이 현 총알과 다를 경우에 초기화 시켜주기 위해 기억해 주기 위한 변수
    private Bullet lastBullet;

    public Transform muzzle;
    public TMP_Text effect;
    public TMP_Text info;
    
    void Start()
    {
        lastBullet = currentBullet;
        currentBullet.InitSetting(info);
    }

    void Update()
    {
        // 총알 정보 초기화 해줌
        if (lastBullet != currentBullet)
        {
            lastBullet = currentBullet;
            currentBullet.InitSetting(info);
        }

        currentBullet.Shooting(muzzle, effect);
        //DiffusionMissileMoveOperation();

        // 이벤트성 총알이 다 소모되었을 경우 기본 총알로 변경해 시간이 끝날때 까지 쏘게 한다.
        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    

}
