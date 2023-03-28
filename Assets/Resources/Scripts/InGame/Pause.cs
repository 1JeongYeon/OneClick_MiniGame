using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour // GameOver 역할도 줌
{
    [SerializeField] private GameObject pauseUI; // 일시 정지 UI 패널
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMPro.TMP_Text scoreTxt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.isPause)
            {
                CallPause();
            }
            else
            {
                ClosePause();
            }
        }

        if (GameManager.Instance.isAlive == false)
        {
            CallGameOver();
        }
    }

    public void CallGameOver()
    {
        GameManager.isPause = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        scoreTxt.text = GameManager.Instance.score.ToString();
        PlayMusicOperator.Instance.StopBGM();

        // 죽었을 때에만 점수 저장, Coin 저장
        PlayerPrefs.SetInt("CurrentScore", GameManager.Instance.score);
        PlayerPrefs.SetInt("SavedHighScoreCount", GameManager.Instance.highScore.Count);
        PlayerPrefs.SetInt("Coin", GameManager.Instance.coin);
        GameManager.Instance.isAlive = true;
    }

    public void CallPause()
    {
        GameManager.isPause = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f; // 시간의 흐름 설정. 0배속. 즉 시간을 멈춤.
    }

    private void ClosePause()
    {
        GameManager.isPause = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f; // 1배속 (정상 속도)
    }

    public void ResumeGame() // 편의상 
    {
        ClosePause();
    }

    public void QuitGame()
    {
        Application.Quit();  // 게임 종료 (에디터 상 실행이기 때문에 종료 눌러도 변화 X)
    }

    // 유니티 참조
    public void GoToMainScene()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        GameManager.Instance.score = 0;
        // DontDestotyOnLoad 중복방지
        var obj = FindObjectOfType<CharacterChoose>();
        Destroy(obj.gameObject);
        var musicObj = FindObjectOfType<PlayMusicOperator>();
        Destroy(musicObj.gameObject);
        SceneController.Instance.OpenScene("Title");
        GameManager.Instance.isAlive = true;
    }
}
