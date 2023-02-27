using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI; // 일시 정지 UI 패널

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


    public void GoToMainScene()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneController.Instance.OpenScene("Title");
        // DontDestotyOnLoad 중복방지
        var obj = FindObjectOfType<CharacterChoose>();
        Destroy(obj.gameObject);
    }
}
