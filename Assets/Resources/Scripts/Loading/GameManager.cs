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
    void Update()
    {
        if (playTimes[1] == 10) // 작업중~
        {
            PlayMusicOperator.Instance.PlayBGM("stage2");
            StageLevelSetting();
        }
    }

    IEnumerator StageLevelSetting()
    {
        stageLevel++;
        yield return new WaitForSeconds(10f);
    }
}
