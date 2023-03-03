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
    // �����ϰ� �ٸ� ����� ���� ����
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

    /*IEnumerator BulletDifficultyAdjustment() // ����� ������ ȣ��Ǵ� ������� �ִ�.
    {
        if (GameManager.Instance.playTimes[1] % 10 == 0 && GameManager.Instance.playTimes[1] > 0 && (GameManager.Instance.playTimes[1] / 10) < 2)
        {
            bulletData.delayTime += Random.Range(-.1f, .5f);
            bulletData.bulletSpeed += 5f;

            Debug.Log(bulletData.bulletSpeed);
            Debug.Log(bulletData.delayTime);
            Debug.Log("���̵� ���!~");
            yield return null;
        }
    }*/

    // �������� ������ �� ���̵��� �ֱ� ���� �Ѿ� �����ӿ� ������ ���� �ڵ� ���� �Ⱦ����̴�.
    private void DiffusionMissileMoveOperation(Bullet _bullet)
    {
        Debug.Log(" ����ź ");
        if (playerTrans == null)
        {
            return;
        }
        float waitForMovingBullet = 0;
        waitForMovingBullet += Time.deltaTime;
        if (waitForMovingBullet < 1f)
        {
            // 1�ʵ��� õõ�� ������ ����
            _bullet.bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * _bullet.bulletData.bulletSpeed, Space.World);
        }
        else
        {
            // 1�� ���� Ÿ�ٹ������� lerp��ġ�̵�
            _bullet.bulletData.bulletSpeed += Time.deltaTime;
            float t = _bullet.bulletData.bulletSpeed / dis;

            bulletTrans.position = Vector3.LerpUnclamped(bulletTrans.position, playerTrans.position, t);
        }
        // �������Ӹ��� Ÿ�ٹ������� ��ź�� �������ٲߴϴ�
        //Ÿ����ġ - ��ź��ġ = ��ź�� Ÿ�����׼��� ����
        Vector3 directionV3 = playerTrans.position - bulletTrans.position;
        Quaternion qua = Quaternion.LookRotation(directionV3);
        bulletTrans.rotation = Quaternion.Slerp(bulletTrans.rotation, qua, Time.deltaTime * 2.5f);
    }


}
