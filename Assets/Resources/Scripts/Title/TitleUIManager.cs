using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private Button gameStart;
    [SerializeField] private Button gameQuit;

    
    private void Start()
    {
        gameStart.onClick.AddListener(() =>
        {
            SceneController.Instance.OpenScene("ChracterChoose");
        });

        gameQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
