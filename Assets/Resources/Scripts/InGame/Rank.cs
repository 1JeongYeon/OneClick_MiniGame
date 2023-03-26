using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    private int[] bestScore = new int[] { };

    public GameObject[] rankFrames;

    public void ScoreSet(int currentScore)
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);

        int tempScore = 0;

        for (int i = 0; i < bestScore.Length; i++)
        {
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");

            while (bestScore[i] < currentScore)
            {
                // 자리 바꿔줌
                tempScore = bestScore[i];
                bestScore[i] = currentScore;
                // 랭킹 저장
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                // 다음 반복을 위한 준비
                currentScore = tempScore;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
        }
    }
}
