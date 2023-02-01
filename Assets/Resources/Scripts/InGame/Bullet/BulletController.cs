using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // ���������� ��Ҵ� �Ѿ��� �� �Ѿ˰� �ٸ� ��쿡 �ʱ�ȭ �����ֱ� ���� ����� �ֱ� ���� ����
    private Bullet lastBullet;

    public Transform muzzle;
    public TMP_Text effect;
    public TMP_Text info;
    
    void Start()
    {
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
        //DiffusionMissileMoveOperation();

        // �̺�Ʈ�� �Ѿ��� �� �Ҹ�Ǿ��� ��� �⺻ �Ѿ˷� ������ �ð��� ������ ���� ��� �Ѵ�.
        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    

}
