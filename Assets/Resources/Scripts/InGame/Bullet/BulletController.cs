using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // 마지막으로 쏘았던 총알이 현 총알과 다를 경우에 초기화 시켜주기 위해 기억해 주기 위한 변수
    private Bullet lastBullet;

    private BulletData bulletData;

    public Transform muzzle;
    public TMP_Text effect;
    public TMP_Text info;

    private Transform playerTrans;
    private Transform bulletTrans;
    private float dis;
    private float delay;

    void Start()
    {
        dis = Vector3.Distance(bulletTrans.position, playerTrans.position);
        bulletData.bullet.transform.rotation = Quaternion.LookRotation(transform.position - playerTrans.position);
        playerTrans = FindObjectOfType<Player>().transform;
        bulletTrans = bulletData.bullet.transform;
        delay = bulletData.delayTime;
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
        DiffusionMissileMoveOperation();

        // 이벤트성 총알이 다 소모되었을 경우 기본 총알로 변경해 시간이 끝날때 까지 쏘게 한다.
        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void DiffusionMissileMoveOperation()
    {
        delay += Time.deltaTime;
        if (delay < 1f)
        {
            // 1초동안 천천히 앞으로 전진
            bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * bulletData.bulletSpeed, Space.Self);
        }
        else
        {
            // 1초 이후 타겟방향으로 lerp위치이동 합니다
            bulletData.bulletSpeed += Time.deltaTime;
            float t = bulletData.bulletSpeed / dis;

            bulletTrans.position = Vector3.LerpUnclamped(bulletTrans.position, playerTrans.position, t);
        }
        // 매프레임마다 타겟방향으로 포탄이 방향을바꿉니다
        //타겟위치 - 포탄위치 = 포탄이 타겟한테서의 방향
        Vector3 directionV3 = playerTrans.position - bulletTrans.position;
        Quaternion qua = Quaternion.LookRotation(directionV3);
        bulletTrans.rotation = Quaternion.Slerp(bulletTrans.rotation, qua, Time.deltaTime * 2f);
    }

}
