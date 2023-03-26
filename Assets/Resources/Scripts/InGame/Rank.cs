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
                // �ڸ� �ٲ���
                tempScore = bestScore[i];
                bestScore[i] = currentScore;
                // ��ŷ ����
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                // ���� �ݺ��� ���� �غ�
                currentScore = tempScore;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
        }
    }
}
