using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour // GameOver ���ҵ� ��
{
    [SerializeField] private GameObject pauseUI; // �Ͻ� ���� UI �г�
    [SerializeField] private GameObject gameOverUI;


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
        var objM = FindObjectOfType<PlayMusicOperator>();
        Destroy(objM.gameObject);
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

    public void GoToMainScene()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneController.Instance.OpenScene("Title");
        // DontDestotyOnLoad �ߺ�����
        var obj = FindObjectOfType<CharacterChoose>();
        Destroy(obj.gameObject);
        GameManager.Instance.isAlive = true;
        //PlayMusicOperator.Instance.PlayBGM("title");
    }
}
