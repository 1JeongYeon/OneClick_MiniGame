using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // ���������� ��Ҵ� �Ѿ��� �� �Ѿ˰� �ٸ� ��쿡 �ʱ�ȭ �����ֱ� ���� ����� �ֱ� ���� ����
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
        // �Ѿ� ���� �ʱ�ȭ ����
        if (lastBullet != currentBullet)
        {
            lastBullet = currentBullet;
            currentBullet.InitSetting(info);
        }

        currentBullet.Shooting(muzzle, effect);
        DiffusionMissileMoveOperation();

        // �̺�Ʈ�� �Ѿ��� �� �Ҹ�Ǿ��� ��� �⺻ �Ѿ˷� ������ �ð��� ������ ���� ��� �Ѵ�.
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
            // 1�ʵ��� õõ�� ������ ����
            bulletData.bulletSpeed = Time.deltaTime;
            transform.Translate(bulletTrans.forward * bulletData.bulletSpeed, Space.Self);
        }
        else
        {
            // 1�� ���� Ÿ�ٹ������� lerp��ġ�̵� �մϴ�
            bulletData.bulletSpeed += Time.deltaTime;
            float t = bulletData.bulletSpeed / dis;

            bulletTrans.position = Vector3.LerpUnclamped(bulletTrans.position, playerTrans.position, t);
        }
        // �������Ӹ��� Ÿ�ٹ������� ��ź�� �������ٲߴϴ�
        //Ÿ����ġ - ��ź��ġ = ��ź�� Ÿ�����׼��� ����
        Vector3 directionV3 = playerTrans.position - bulletTrans.position;
        Quaternion qua = Quaternion.LookRotation(directionV3);
        bulletTrans.rotation = Quaternion.Slerp(bulletTrans.rotation, qua, Time.deltaTime * 2f);
    }

}
