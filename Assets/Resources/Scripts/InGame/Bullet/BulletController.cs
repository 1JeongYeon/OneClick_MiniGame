using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;
    // ���������� ��Ҵ� �Ѿ��� �� �Ѿ˰� �ٸ� ��쿡 �ʱ�ȭ �����ֱ� ���� ����� �ֱ� ���� ����
    public Bullet lastBullet;

    // �������� �ֱ� ���� �迭
    [SerializeField] Bullet[] bullets;

    [SerializeField] protected Transform muzzle;
    public TMP_Text effect;
    private float delay;

    void Start()
    {
        
    }

    void Update()
    {/*
        // �Ѿ� ���� �ʱ�ȭ ����
        if (lastBullet != currentBullet)
        {
            lastBullet = currentBullet;
            currentBullet.InitSetting();
        }*/

        // �Ѿ� ���� �Լ� ����
        delay += Time.deltaTime;
        if (delay >= 0.5f)
        {
            RandomBulletSetting();
            
            delay = 0f;
        }
    }

    private void RandomBulletSetting() 
    {
        int index = ChanceMaker.GetRandom(new int[] { 30, 30, 80});

        currentBullet = bullets[index]; 

        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
    }
}
