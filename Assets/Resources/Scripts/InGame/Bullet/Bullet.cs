using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public struct BulletData
{
    public float delayTime;
    public string information;
    public string soundEffect;
    public int damage;
    public float bulletSpeed;
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData = new BulletData();

    public bool shootAble = false;

    private Transform playerTrans;
    private Transform bulletTrans;
    private Rigidbody2D bulletRigidBody;
    private float rotateSpeed = 2f;
    private float dis;

    public virtual void InitSetting()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        playerTrans = FindObjectOfType<Player>().transform;

        //StartCoroutine("BulletDifficultyAdjustment");
    }

    public abstract void Hit();
    public abstract void Crushed();

    public abstract void BulletDifficultyAdjustment();
    // 삭제하고 다른 기능을 넣을 예정
    public virtual void Shooting()
    {
        if (shootAble == true)
        {
            shootAble = false;
        }
    }

    private void Update()
    {
        if (gameObject != null)
        {
            MoveBullet();
        }
    }

    private void MoveBullet()
    {

        Vector3 dir = (playerTrans.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotTarget = Quaternion.AngleAxis(-angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotateSpeed);

        bulletRigidBody.velocity = new Vector2(dir.x * bulletData.bulletSpeed, dir.y * bulletData.bulletSpeed);
    }

    /*IEnumerator BulletDifficultyAdjustment() // 상당히 여러번 호출되는 어려움이 있다.
    {
        if (GameManager.Instance.playTimes[1] % 10 == 0 && GameManager.Instance.playTimes[1] > 0 && (GameManager.Instance.playTimes[1] / 10) < 2)
        {
            bulletData.delayTime += Random.Range(-.1f, .5f);
            bulletData.bulletSpeed += 5f;

            Debug.Log(bulletData.bulletSpeed);
            Debug.Log(bulletData.delayTime);
            Debug.Log("난이도 상승!~");
            yield return null;
        }
    }*/

    // 스테이지 높아질 때 난이도를 주기 위해 총알 움직임에 변수를 넣을 코드 아직 안쓸것이다.
    private void DiffusionMissileMoveOperation(Bullet _bullet)
    {
        Debug.Log(" 유도탄 ");
        if (playerTrans == null)
        {
            return;
        }
        float waitForMovingBullet = 0;
        waitForMovingBullet += Time.deltaTime;
        if (waitForMovingBullet < 1f)
        {
            // 1초동안 천천히 앞으로 전진
            _bullet.bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * _bullet.bulletData.bulletSpeed, Space.World);
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
