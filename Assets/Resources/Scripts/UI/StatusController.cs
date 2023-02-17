using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    private static StatusController statusController;
    public static StatusController Instance
    {
        get
        {
            if (statusController == null)
                statusController = FindObjectOfType<StatusController>();
            return statusController;
        }
    }

    public int maxHp;
    public int currentHp;

    private void Start()
    {
        maxHp = currentHp = 100;
    }


    // 체력을 올려주는 함수
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

    // 체력 깎이는 함수
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
        {
            Debug.Log("GAME OVER");
            // UI 작업 필요
        }
    }
    

}
