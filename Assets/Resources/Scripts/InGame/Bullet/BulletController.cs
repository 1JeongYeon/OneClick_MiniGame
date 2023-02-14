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

    private int maxBulletMinus;
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
        currentBullet.Shooting(muzzle, effect);

        // 랜덤 변수 만들어서 총알 바꿔야하는데 헷갈려 죽겠음...
        if (currentBullet.bulletData.maxBullet != 0)
        {
            int rand = Random.Range(0, 100);
            if (rand <= 10)
            {
                currentBullet = GetComponent<HPBullet>(); // 10퍼센트 확률
            }
            else if (rand <= 20)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 20퍼
            }
            else
            {
                currentBullet = GetComponent<DefaultBullet>();
            }
        }

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() // 수정 필요
    {
        int rndIndex = Random.Range(0, bullets.Length + 1);

        if (rndIndex == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
        else if (rndIndex == 1)
        {
            currentBullet = GetComponent<HPBullet>(); // 10퍼센트 확률
        }
        else if (rndIndex == 2)
        {
            currentBullet = GetComponent<GoldCoinBullet>(); // 20퍼
        }
        else
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

}
