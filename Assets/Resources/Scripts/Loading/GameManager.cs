using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 확률 시스템 On
public static class ChanceMaker
{
    public static bool GetThisChanceResult(float Chance)
    {
        if (Chance < 0.0000001f)
        {
            Chance = 0.0000001f;
        }

        bool Success = false;
        int RandAccuracy = 10000000;
        float RandHitRange = Chance * RandAccuracy;
        int Rand = Random.Range(1, RandAccuracy + 1);
        if (Rand <= RandHitRange)
        {
            Success = true;
        }
        return Success;
    }

    public static bool GetThisChanceResult_Percentage(float Percentage_Chance)
    {
        if (Percentage_Chance < 0.0000001f)
        {
            Percentage_Chance = 0.0000001f;
        }

        Percentage_Chance = Percentage_Chance / 100;

        bool Success = false;
        int RandAccuracy = 10000000;
        float RandHitRange = Percentage_Chance * RandAccuracy;
        int Rand = Random.Range(1, RandAccuracy + 1);
        if (Rand <= RandHitRange)
        {
            Success = true;
        }
        return Success;
    }

    public static int GetRandom(int[] seed)
    {
        int randSum = seed.Sum(); // 총량
        int rand = Random.Range(0, randSum + 1);
        int sum = 0;
        int returnIndex = 0;
        for (int i = 0; i < seed.Length; i++)
        {
            sum += seed[i];
            if (sum >= rand)
            {
                returnIndex = i;
                break;
            }
        }
        //Debug.Log($"{rand}, {returnIndex}, {randSum}");
        return returnIndex;
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public static GameManager Instance
    {
        get
        {
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
            return gameManager;
        }
    }
    public static bool isPause = false; // 일시 정지 메뉴 창 활성화

    public bool isAlive = true;
    public int score = 0;
    public int coin = 0;
    public int[] playTimes = { 0, 0, 0 }; // 분,초,밀리초
    public int stageLevel = 1; 

    Bullet bullet;
    void FixedUpdate()
    {
        if (playTimes[1] == 10 ) // 작업중~
        {
            PlayMusicOperator.Instance.PlayBGM("stage2");
            InvokeRepeating("StageLevelSetting", 0f, 3f); // 0초후 처음 호출하고 3초마다 호출 // 순간 너무 많이 호출되어 버림 한번만 시작해야하는데 그러면 start에서 시작한느게 바람직함 그럴라면 gamemanager에서 벗어나야함 딜레마에 빠짐 ㅠ
        }
    }

    private void StageLevelSetting()
    {
        if (playTimes[1] > 10)
        {
            return;
        }
        stageLevel++;
    }
}
