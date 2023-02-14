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

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() 
    {
        int rndIndex = Random.Range(0, 100);

        if (currentBullet != null)
        {
            if (rndIndex <= 5 && rndIndex >= 0)
            {
                currentBullet = GetComponent<HPBullet>(); // 5퍼센트 확률
                currentBullet.Shooting(muzzle, effect);
                return;
            }
            if (rndIndex <= 25 && rndIndex >= 6)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 25퍼센트 확률
                currentBullet.Shooting(muzzle, effect);
                return;
            }
             // 나머지 확률은 기본총알로
                currentBullet = GetComponent<DefaultBullet>();
                currentBullet.Shooting(muzzle, effect);
        }
    }
}
