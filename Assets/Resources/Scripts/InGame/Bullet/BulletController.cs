using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // ���������� ��Ҵ� �Ѿ��� �� �Ѿ˰� �ٸ� ��쿡 �ʱ�ȭ �����ֱ� ���� ����� �ֱ� ���� ����
    private Bullet lastBullet;

    // �������� �ֱ� ���� �迭
    [SerializeField] Bullet[] bullets;

    public Transform muzzle;
    public TMP_Text effect;
    public TMP_Text info;

    private int maxBulletFixed;
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

        // �Ѿ� ���� �Լ� ����
        RandomBulletSetting();

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() 
    {
        int rndIndex = Random.Range(0, 100);

        if (currentBullet != null)
        {
            if (rndIndex <= 5 && rndIndex >= 0)
            {
                currentBullet = GetComponent<HPBullet>(); // 5�ۼ�Ʈ Ȯ��
                currentBullet.Shooting(muzzle, effect);
                return;
            }
            if (rndIndex <= 25 && rndIndex >= 6)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 25�ۼ�Ʈ Ȯ��
                currentBullet.Shooting(muzzle, effect);
                return;
            }
             // ������ Ȯ���� �⺻�Ѿ˷�
                currentBullet = GetComponent<DefaultBullet>();
                currentBullet.Shooting(muzzle, effect);
        }
    }
}
