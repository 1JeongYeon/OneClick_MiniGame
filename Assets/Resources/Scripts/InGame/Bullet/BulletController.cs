using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // 마지막으로 쏘았던 총알이 현 총알과 다를 경우에 초기화 시켜주기 위해 기억해 주기 위한 변수
    private Bullet lastBullet;

    // 랜덤변수 넣기 위한 배열
    [SerializeField] Bullet[] bullets;

    public Transform muzzle;
    public TMP_Text effect;
    public TMP_Text info;

    private int maxBulletFixed;
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

        // 총알 생성 함수 실행
        RandomBulletSetting();
        currentBullet.Shooting(muzzle, effect);

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }
    // 총알이 발사되면 해당 총알의 딜레이시간 후에 다음 총알이 발사되어야 한다.
    // 총알이 발사된 후 랜덤으로 기본총알 체력회복총알 코인총알 세개중 하나로 getcomponent한다.

    private void RandomBulletSetting() 
    {
        bool heal = ChanceMaker.GetThisChanceResult_Percentage(5);
        bool coin = ChanceMaker.GetThisChanceResult_Percentage(20);
        if (currentBullet != null)
        {
            currentBullet = GetComponent<DefaultBullet>();
            if (heal)
            {
                currentBullet = GetComponent<HealBullet>(); // 5퍼센트 확률
            }
            if (coin)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 20퍼센트 확률
            }
        }
    }
}
