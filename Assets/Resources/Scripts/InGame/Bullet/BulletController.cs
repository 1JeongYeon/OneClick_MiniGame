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

    private int maxBulletMinus;
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
        currentBullet.Shooting(muzzle, effect);

        // ���� ���� ���� �Ѿ� �ٲ���ϴµ� �򰥷� �װ���...
        if (currentBullet.bulletData.maxBullet != 0)
        {
            int rand = Random.Range(0, 100);
            if (rand <= 10)
            {
                currentBullet = GetComponent<HPBullet>(); // 10�ۼ�Ʈ Ȯ��
            }
            else if (rand <= 20)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 20��
            }
            else
            {
                currentBullet = GetComponent<DefaultBullet>();
            }
        }

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() // ���� �ʿ�
    {
        int rndIndex = Random.Range(0, bullets.Length + 1);

        if (rndIndex == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
        else if (rndIndex == 1)
        {
            currentBullet = GetComponent<HPBullet>(); // 10�ۼ�Ʈ Ȯ��
        }
        else if (rndIndex == 2)
        {
            currentBullet = GetComponent<GoldCoinBullet>(); // 20��
        }
        else
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

}
