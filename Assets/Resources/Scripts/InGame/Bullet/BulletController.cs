using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // 마지막으로 쏘았던 총알이 현 총알과 다를 경우에 초기화 시켜주기 위해 기억해 주기 위한 변수
    private Bullet lastBullet;

    [SerializeField] Bullet[] bullets;

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

        // 이벤트성 총알이 다 소모되었을 경우 기본 총알로 변경해 시간이 끝날때 까지 쏘게 한다.
        if (currentBullet.bulletData.maxBullet >= currentBullet.bulletData.maxBullet - 1)
        {
            RandomBulletSetting();
        }
        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() // 수정 필요
    {
        int rndIndex = Random.Range(0, bullets.Length);

        if (rndIndex == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else if (rndIndex == 1)
        {
            currentBullet = GetComponent<HPBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else if (rndIndex == 2)
        {
            currentBullet = GetComponent<GoldCoinBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

}
