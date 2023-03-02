using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;

    // 랜덤변수 넣기 위한 배열
    [SerializeField] Bullet[] bullets;

    [SerializeField] protected Transform muzzle;
    public TMP_Text effect;
    private float time = 0f;
    private float delay = 0.5f;

    private void Start()
    {
    }
    void Update()
    {
        // 총알 생성 함수 실행
        time += Time.deltaTime;
        if (time >= delay)
        {
            RandomBulletSetting();

            time = 0f;
        }
    }

    private void RandomBulletSetting() 
    {
        int index = ChanceMaker.GetRandom(new int[] { 140, 50, 10}); // default, coin, heal 가중치
        currentBullet = bullets[index];
        
        
        //Bullet 생성
        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
        delay = 0.5f;
    }

    private  void BulletDifficultyAdjustment()
    {
    }
}
