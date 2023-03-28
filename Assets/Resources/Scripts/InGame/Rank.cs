using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // 10등까지만 보여줄 것
    public GameObject[] rankFrames;

    private void Start()
    {
        GameManager.Instance.highScore.Capacity = rankFrames.Length;
    }
    // 유니티에서 랭킹 버튼 누를 시 실행
    public void ScoreFrameGenerate(int _currentScore)
    {
        // Pause 클래스에서 게임오버후 화면 전환 시 저장함
        _currentScore = PlayerPrefs.GetInt("CurrentScore");
        // scene 이동시 리스트 데이터 삭제 방지 dondestroy 되는 게임매니저에 리스트를 참조한다.
        GameManager.Instance.highScore.Add(_currentScore);
        Debug.Log(GameManager.Instance.highScore.Count + " 베스트 스코어 리스트 카운트");
        GameManager.Instance.highScore.Sort();
        GameManager.Instance.highScore.Reverse(); // 내림차순으로 정렬
        // 기록들이 리스트에 잘 저장되지만 ui가 설정이 안됨 그 전에는 됐었음;; 
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
        Time.timeScale = 1f; // 시간 흐름
    }
    public void TimeOff()
    {
        Time.timeScale = 0f; // 시간의 흐름 설정. 0배속. 즉 시간을 멈춤.
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
                // 자리 바꿔줌
                tempScore = bestScore[i];
                bestScore[i] = currentScore;
                // 랭킹 저장
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                // 다음 반복을 위한 준비
                currentScore = tempScore;
            }
            Debug.Log(bestScore.Count + " bestscore 배열");
        }
        // 랭킹에 맞춰서 점수 저장
        for (int i = 0; i < bestScore.Count; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            
        }
    }*/
}
