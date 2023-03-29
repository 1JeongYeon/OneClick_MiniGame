using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // 10등까지만 보여줄 것
    public GameObject[] rankFrames;
    public int highScoreCount = 0;
    private void Start()
    {
        // 총량을 정해준다.
        GameManager.Instance.highScore.Capacity = rankFrames.Length;
        // +1을 해주는 이유는 기능 실행전 count의 수는 1 적기 때문
        highScoreCount = PlayerPrefs.GetInt("SavedHighScoreCount") + 1;
        for (int i = 0; i < highScoreCount; i++)
        {   // 랭크 불러오기
             GameManager.Instance.highScore.Add(PlayerPrefs.GetInt(i + "SavedHighScore"));
        }
        GameManager.Instance.highScore = GameManager.Instance.highScore.Distinct().ToList();
        ScoreFrameGenerate(GameManager.Instance.score);
    }
    // 유니티에서 InGame Scene이 시작할 때 즉, Rank가 활성화 될때마다 함수 호출
    public void ScoreFrameGenerate(int _currentScore)
    {
        _currentScore = PlayerPrefs.GetInt("CurrentScore");
        // scene 이동시 리스트 데이터 삭제 방지 dondestroy 되는 게임매니저에 리스트를 참조한다.
        GameManager.Instance.highScore = GameManager.Instance.highScore.Distinct().ToList(); // 중복제거
        GameManager.Instance.highScore.Add(_currentScore);
        GameManager.Instance.highScore.Sort();
        GameManager.Instance.highScore.Reverse(); // 내림차순으로 정렬
        if (GameManager.Instance.highScore.Count >= rankFrames.Length)
        {
            // rankFrames.Length 가 총 11개이다 11번째 rankFrame은 데이터 임시 보관용으로 gameObject.SetActive = false, 젤 점수 낮은건 11번째로 들어가고 삭제되게 함.
            GameManager.Instance.highScore.RemoveAt(10);
        }
        // 11개중 10개만 활성화 비활성화 한다.
        for (int i = 0; i < rankFrames.Length - 1; i++)
        {
            rankFrames[i].SetActive(i < GameManager.Instance.highScore.Count);
            rankFrames[i].GetComponentInChildren<TMPro.TMP_Text>().text = i < GameManager.Instance.highScore.Count ? GameManager.Instance.highScore[i].ToString() : "";
        }
        // 리스트 생성은 ingame씬 들어갈 때 이루어짐
        // 게임이 껐다 켜젔을 때 랭킹은 남아있어야 하므로 저장한다.
        for (int i = 0; i < GameManager.Instance.highScore.Count; i++)
        {
            PlayerPrefs.SetInt(i + "SavedHighScore", GameManager.Instance.highScore[i]);
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
}
