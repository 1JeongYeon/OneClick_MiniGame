using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    private int[] bestScore = new int[] { };
    private int[] bestTime = new int[] { };

    public void ScoreSet(int currentScore, int currentTime)
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.SetInt("CurrentTime", currentTime);

        int score = 0;
        int time = 0;

        for (int i = 0; i < bestScore.Length; i++)
        {
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            bestTime[i] = PlayerPrefs.GetInt(i + "BestTime");

            while (bestScore[i] < currentScore)
            {
                // �ڸ� �ٲ���
                score = bestScore[i];
                time = bestTime[i];
                bestScore[i] = currentScore;
                bestTime[i] = currentTime;
                // ��ŷ ����
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                PlayerPrefs.SetInt(i + "BestTime", currentTime);
                // ���� �ݺ��� ���� �غ�
                currentScore = score;
                currentTime = time;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetInt(i + "BestTime", bestTime[i]);
        }
    }
}
