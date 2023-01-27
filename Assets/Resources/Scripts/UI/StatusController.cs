using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    private static StatusController _instance;
    public static StatusController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<StatusController>();
            return _instance;
        }
    }

    public int maxHp;
    public int currentHp;

    private void Start()
    {
        maxHp = currentHp = 100;
    }


    // ü���� �÷��ִ� �Լ�
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

    // ü�� ���̴� �Լ�
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
        {
            Debug.Log("Game Over");
            // UI �۾� �ʿ�
        }
    }
    

}
