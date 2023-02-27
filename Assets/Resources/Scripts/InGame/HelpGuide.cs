using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpGuide : MonoBehaviour
{
    [SerializeField] private GameObject baseUI;
    [SerializeField] private Button guideBtn;

    private void Start()
    {
        guideBtn.onClick.AddListener(()=> 
        {
            if (!GameManager.isPause)
            {
                Call();
            }
            else
            {
                Close();
            }
        });
    }

    public void Call()
    {
        GameManager.isPause = true;
        baseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Close()
    {
        GameManager.isPause = false;
        baseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
