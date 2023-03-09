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
    private float time = 0f;
    private float delay = 0.5f;

    void Update()
    {
        // �Ѿ� ���� �Լ� ����
        time += Time.deltaTime;
        if (time >= delay)
        {
            delay = 0.5f; // delay �ʱ�ȭ
            
            DelayDifficultyAdjustment(); // delay ���̵� ����
            RandomBulletSetting(); // ���� �Ѿ� ����
            time = 0f;
            Debug.Log(delay);
        }
    }

    private void RandomBulletSetting() 
    {
        
        int index = ChanceMaker.GetRandom(new int[] { 140, 50, 10}); // default, coin, heal ����ġ
        currentBullet = bullets[index];
        //Bullet ����
        Instantiate(currentBullet, muzzle.position, Quaternion.identity);
    }

    private void DelayDifficultyAdjustment()
    {
        
        if (GameManager.Instance.playTimes[1] >= 10)
        {
            delay += GameManager.Instance.stageLevel * Random.Range(-0.2f, 0.2f); // level�� ������ �ʸ��� �ö󰣴�.
        }
    }
}
