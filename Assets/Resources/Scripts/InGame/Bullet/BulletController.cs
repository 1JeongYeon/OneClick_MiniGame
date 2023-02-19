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
        currentBullet.Shooting(muzzle, effect);

        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }
    // �Ѿ��� �߻�Ǹ� �ش� �Ѿ��� �����̽ð� �Ŀ� ���� �Ѿ��� �߻�Ǿ�� �Ѵ�.
    // �Ѿ��� �߻�� �� �������� �⺻�Ѿ� ü��ȸ���Ѿ� �����Ѿ� ������ �ϳ��� getcomponent�Ѵ�.

    private void RandomBulletSetting() 
    {
        bool heal = ChanceMaker.GetThisChanceResult_Percentage(5);
        bool coin = ChanceMaker.GetThisChanceResult_Percentage(20);
        if (currentBullet != null)
        {
            currentBullet = GetComponent<DefaultBullet>();
            if (heal)
            {
                currentBullet = GetComponent<HealBullet>(); // 5�ۼ�Ʈ Ȯ��
            }
            if (coin)
            {
                currentBullet = GetComponent<GoldCoinBullet>(); // 20�ۼ�Ʈ Ȯ��
            }
        }
    }
}
