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

    // 체력을 올려주는 총알을 먹었을 때
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

    // 총알에 맞았을 때
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
        {
            Debug.Log("Game Over");
            // UI 작업 필요
        }
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / maxHp;
    }

}
