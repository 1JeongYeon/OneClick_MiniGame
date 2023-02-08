using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // ���������� ��Ҵ� �Ѿ��� �� �Ѿ˰� �ٸ� ��쿡 �ʱ�ȭ �����ֱ� ���� ����� �ֱ� ���� ����
    private Bullet lastBullet;

    [SerializeField] Bullet[] bullets;

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

        // �̺�Ʈ�� �Ѿ��� �� �Ҹ�Ǿ��� ��� �⺻ �Ѿ˷� ������ �ð��� ������ ���� ��� �Ѵ�.
        if (currentBullet.bulletData.maxBullet >= currentBullet.bulletData.maxBullet - 1)
        {
            RandomBulletSetting();
        }
        if (currentBullet.bulletData.maxBullet == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

    private void RandomBulletSetting() // ���� �ʿ�
    {
        int rndIndex = Random.Range(0, bullets.Length);

        if (rndIndex == 0)
        {
            currentBullet = GetComponent<DefaultBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else if (rndIndex == 1)
        {
            currentBullet = GetComponent<HPBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else if (rndIndex == 2)
        {
            currentBullet = GetComponent<GoldCoinBullet>();
            rndIndex = Random.Range(0, bullets.Length);
        }
        else
        {
            currentBullet = GetComponent<DefaultBullet>();
        }
    }

}
