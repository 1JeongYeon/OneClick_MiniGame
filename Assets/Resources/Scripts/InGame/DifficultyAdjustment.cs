using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjustment : MonoBehaviour
{
    // 구조 다시 짜야함
    private void Start()
    {
        if (GameManager.Instance.playTimes[1] <= 10)
        {
            gameObject.SetActive(false);
        }
        if (GameManager.Instance.playTimes[1] >= 10)
        {
            InvokeRepeating("LevelSetting", 0f, 5f);
        }
    }

    private void LevelSetting()
    {
         GameManager.Instance.stageLevel++;
         Debug.Log("Level UP");
         Debug.Log(GameManager.Instance.stageLevel);
    }
}
