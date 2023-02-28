using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    public Bullet currentBullet;

    // �������� �ֱ� ���� �迭
    [SerializeField] Bullet[] bullets;

    [SerializeField] protected Transform muzzle;
    public TMP_Text effect;
    private float delay;

    void Update()
    {
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
        int index = ChanceMaker.GetRandom(new int[] { 140, 50, 10}); // default, coin, heal ����ġ

        currentBullet = bullets[index]; 
        //Bullet ����
        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
    }
}
