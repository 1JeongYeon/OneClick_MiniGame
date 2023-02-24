using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // 마지막으로 쏘았던 총알이 현 총알과 다를 경우에 초기화 시켜주기 위해 기억해 주기 위한 변수
    public Bullet lastBullet;

    // 랜덤변수 넣기 위한 배열
    [SerializeField] Bullet[] bullets;

    [SerializeField] protected Transform muzzle;
    public TMP_Text effect;
    private float delay;

    void Start()
    {
        
    }

    void Update()
    {/*
        // 총알 정보 초기화 해줌
        if (lastBullet != currentBullet)
        {
            lastBullet = currentBullet;
            currentBullet.InitSetting();
        }*/

        // 총알 생성 함수 실행
        delay += Time.deltaTime;
        if (delay >= 0.5f)
        {
            RandomBulletSetting();
            
            delay = 0f;
        }
    }

    private void RandomBulletSetting() 
    {
        int index = ChanceMaker.GetRandom(new int[] { 30, 30, 80});

        currentBullet = bullets[index]; 

        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
    }
}
