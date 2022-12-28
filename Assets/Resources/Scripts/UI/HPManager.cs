using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    private static HPManager _instance;
    public static HPManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<HPManager>();
            return _instance;
        }
    }
    [SerializeField]
    public Image[] images_Gauge;

    public const int HP = 0;

    public int maxHp;
    public int currentHp;

    private void Update()
    {
        GaugeUpdate();
    }

    // ü���� �÷��ִ� �Ѿ��� �Ծ��� ��
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < maxHp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = maxHp;
        }
    }

    // �Ѿ˿� �¾��� ��
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
        {
            Debug.Log("Game Over");
            // UI �۾� �ʿ�
        }
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / maxHp;
    }

}
