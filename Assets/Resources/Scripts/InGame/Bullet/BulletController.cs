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

    void Update()
    {
        // 총알 생성 함수 실행
        time += Time.deltaTime;
        if (time >= delay)
        {
            delay = 0.5f; // delay 초기화
            
            DelayDifficultyAdjustment(); // delay 난이도 설정
            RandomBulletSetting(); // 랜덤 총알 생성
            time = 0f;
            Debug.Log(delay);
        }
    }

    private void RandomBulletSetting() 
    {
        
        int index = ChanceMaker.GetRandom(new int[] { 140, 50, 10}); // default, coin, heal 가중치
        currentBullet = bullets[index];
        //Bullet 생성
        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
    }

    private void DelayDifficultyAdjustment()
    {
        
        if (GameManager.Instance.playTimes[1] >= 10)
        {
            delay += GameManager.Instance.stageLevel * Random.Range(-0.2f, 0.2f); // level은 지정한 초마다 올라간다.
        }
    }
}
