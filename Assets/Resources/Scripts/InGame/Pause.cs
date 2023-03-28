using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour // GameOver ���ҵ� ��
{
    [SerializeField] private GameObject pauseUI; // �Ͻ� ���� UI �г�
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

        // �׾��� ������ ���� ����, Coin ����
        PlayerPrefs.SetInt("CurrentScore", GameManager.Instance.score);
        PlayerPrefs.SetInt("SavedHighScoreCount", GameManager.Instance.highScore.Count);
        PlayerPrefs.SetInt("Coin", GameManager.Instance.coin);
        GameManager.Instance.isAlive = true;
    }

    public void CallPause()
    {
        GameManager.isPause = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f; // �ð��� �帧 ����. 0���. �� �ð��� ����.
    }

    private void ClosePause()
    {
        GameManager.isPause = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f; // 1��� (���� �ӵ�)
    }

    public void ResumeGame() // ���ǻ� 
    {
        ClosePause();
    }

    public void QuitGame()
    {
        Application.Quit();  // ���� ���� (������ �� �����̱� ������ ���� ������ ��ȭ X)
    }

    // ����Ƽ ����
    public void GoToMainScene()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        GameManager.Instance.score = 0;
        // DontDestotyOnLoad �ߺ�����
        var obj = FindObjectOfType<CharacterChoose>();
        Destroy(obj.gameObject);
        var musicObj = FindObjectOfType<PlayMusicOperator>();
        Destroy(musicObj.gameObject);
        SceneController.Instance.OpenScene("Title");
        GameManager.Instance.isAlive = true;
    }
}
