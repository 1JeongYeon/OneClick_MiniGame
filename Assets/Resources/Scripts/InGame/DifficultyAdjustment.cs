using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjustment : MonoBehaviour
{
    // 구조 다시 짜야할듯...
    private void Start()
    {
        InvokeRepeating("LevelSetting", 0f, 10f);
    }

    private void LevelSetting()
    {
        
         GameManager.Instance.stageLevel++;
        if (GameManager.Instance.stageLevel >= 4)
        {
            GameManager.Instance.stageLevel = 4;
        }
        Debug.Log("Level UP");
         Debug.Log(GameManager.Instance.stageLevel);
    }
}
