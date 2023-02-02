using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public struct BulletData
{
    public float delayTime;
    public int maxBullet;
    public string information;
    public string soundEffect;
    public int damage;
    public float bulletSpeed;
    public GameObject bullet;
    
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private bool shootAble = false;
    private float delayTime = 0f;
    private Transform playerTrans;
    private Transform bulletTrans;
    private float dis;

    private void Start()
    {
        delayTime = bulletData.delayTime;

        playerTrans = FindObjectOfType<Player>().transform;
    }
    public abstract void InitSetting(TMP_Text info);

    public virtual void Shooting(Transform muzzle, TMP_Text soundText)
    {
        if (shootAble == true)
        {
            var bullet = Instantiate(bulletData.bullet);
            bullet.transform.position = muzzle.position; // 시작점

            bulletTrans = bullet.transform;
            dis = Vector3.Distance(bulletTrans.position, playerTrans.position);
            bulletData.bullet.transform.rotation = Quaternion.LookRotation(transform.position - playerTrans.position);

            DiffusionMissileMoveOperation(bullet.GetComponent<Bullet>());

            var fireEffect = Instantiate(soundText);
            fireEffect.transform.position = muzzle.position + new Vector3(0, 3f, 0);
            fireEffect.text = bulletData.soundEffect;

            shootAble = false;

            // 총알 최대치가 중요한지 고민 해야 함. 라운드를 시간으로 할지 아니면 총 총알의 갯수를 다 소진했을 때 종료할지 고민 ㄱㄱ
            bulletData.maxBullet--;
        }

        if (shootAble == false)
        {
            delayTime += Time.deltaTime;
            if (delayTime >= bulletData.delayTime)
            {
                DestroyImmediate(soundText);
                shootAble = true;
                delayTime = 0f;
            }
        }
    }

    // instantiate되는 총알이 이 정보를 담고 있어야 하는데 그러질 못해서 null이 뜨는중
    private void DiffusionMissileMoveOperation(Bullet _bullet)
    {
        Debug.Log(" 유도탄 ");


        delayTime += Time.deltaTime;
        if (delayTime < 1f)
        {
            // 1초동안 천천히 앞으로 전진
            _bullet.bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * _bullet.bulletData.bulletSpeed, Space.Self);
        }
        else
        {
            // 1초 이후 타겟방향으로 lerp위치이동
            _bullet.bulletData.bulletSpeed += Time.deltaTime;
            float t = _bullet.bulletData.bulletSpeed / dis;

            bulletTrans.position = Vector3.LerpUnclamped(bulletTrans.position, playerTrans.position, t);
        }
        // 매프레임마다 타겟방향으로 포탄이 방향을바꿉니다
        //타겟위치 - 포탄위치 = 포탄이 타겟한테서의 방향
        Vector3 directionV3 = playerTrans.position - bulletTrans.position;
        Quaternion qua = Quaternion.LookRotation(directionV3);
        bulletTrans.rotation = Quaternion.Slerp(bulletTrans.rotation, qua, Time.deltaTime * 2.5f);
    }
}
