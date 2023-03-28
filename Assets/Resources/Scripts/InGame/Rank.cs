using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // 10������� ������ ��
    public GameObject[] rankFrames;

    private void Start()
    {
        GameManager.Instance.highScore.Capacity = rankFrames.Length;
    }
    // ����Ƽ���� ��ŷ ��ư ���� �� ����
    public void ScoreFrameGenerate(int _currentScore)
    {
        // Pause Ŭ�������� ���ӿ����� ȭ�� ��ȯ �� ������
        _currentScore = PlayerPrefs.GetInt("CurrentScore");
        // scene �̵��� ����Ʈ ������ ���� ���� dondestroy �Ǵ� ���ӸŴ����� ����Ʈ�� �����Ѵ�.
        GameManager.Instance.highScore.Add(_currentScore);
        Debug.Log(GameManager.Instance.highScore.Count + " ����Ʈ ���ھ� ����Ʈ ī��Ʈ");
        GameManager.Instance.highScore.Sort();
        GameManager.Instance.highScore.Reverse(); // ������������ ����
        // ��ϵ��� ����Ʈ�� �� ��������� ui�� ������ �ȵ� �� ������ �ƾ���;; 
        for (int i = 0; i < rankFrames.Length; i++)
        {
            rankFrames[i].SetActive(i < GameManager.Instance.highScore.Count);
            rankFrames[i].GetComponentInChildren<TMPro.TMP_Text>().text = i < GameManager.Instance.highScore.Count ? GameManager.Instance.highScore[i].ToString() : "";
            if (GameManager.Instance.highScore.Count > rankFrames.Length)
            {
                GameManager.Instance.highScore.RemoveAt(rankFrames.Length + 1);
            }
        }
    }
    public void TimeOn()
    {
        Time.timeScale = 1f; // �ð� �帧
    }
    public void TimeOff()
    {
        Time.timeScale = 0f; // �ð��� �帧 ����. 0���. �� �ð��� ����.
    }
    /*public void ScoreSet(int currentScore)
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);

        int tempScore = 0;

        for (int i = 0; i < bestScore.Count; i++)
        {
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");

            while (bestScore[i] < currentScore)
            {
                //Array.Sort(bestScore);
                // �ڸ� �ٲ���
                tempScore = bestScore[i];
                bestScore[i] = currentScore;
                // ��ŷ ����
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                // ���� �ݺ��� ���� �غ�
                currentScore = tempScore;
            }
            Debug.Log(bestScore.Count + " bestscore �迭");
        }
        // ��ŷ�� ���缭 ���� ����
        for (int i = 0; i < bestScore.Count; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            
        }
    }*/
}
