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
        //currentBullet.Shooting(muzzle, effect);

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }
    // �Ѿ��� �߻�Ǹ� �ش� �Ѿ��� �����̽ð� �Ŀ� ���� �Ѿ��� �߻�Ǿ�� �Ѵ�.
    // �Ѿ��� �߻�� �� �������� �⺻�Ѿ� ü��ȸ���Ѿ� �����Ѿ� ������ �ϳ��� getcomponent�Ѵ�.

    private void RandomBulletSetting() 
    {
        int rndIndex = Random.Range(0, 100);

        if (currentBullet != null)
        {
            if (rndIndex <= 5 && rndIndex >= 0)
            {
                currentBullet = GetComponent<HPBullet>(); // 5�ۼ�Ʈ Ȯ��
            }
            else if (rndIndex <= 25 && rndIndex >= 6)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 20�ۼ�Ʈ Ȯ��
            }
            else
            {
                // ������ Ȯ���� �⺻�Ѿ˷�
                currentBullet = GetComponent<DefaultBullet>();
            }
        }
        currentBullet.Shooting(muzzle, effect);
    }

    private void RandomBullet()
    {
        if (currentBullet.shootAble == false)
        {
            int rndIndex = Random.Range(1,21);

            switch (rndIndex)
            {
                case <= 1:
                    currentBullet = GetComponent<HPBullet>();
                    break;
                case <= 4:
                    currentBullet = GetComponent<GoldCoinBullet>();
                    break;
                default:
                    currentBullet = GetComponent<DefaultBullet>();
                    break;
            }
            currentBullet.Shooting(muzzle, effect);
        }
    }
}
