using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    [SerializeField] private Button muteBtn;
    private bool isMute = false;
    private void Start()
    {
        muteBtn.image.color = Color.blue; // 재생 상태
        muteBtn.onClick.AddListener(() =>
        { 
            isMute = !isMute; // 누를때마다 true면 false가 되고 false면 true가 됨

            if (isMute == false)
            {
                PlayMusicOperator.Instance.PauseBGM();
                muteBtn.image.color = Color.red;
            }
            else if (isMute == true)
            {
                PlayMusicOperator.Instance.UnPauseBGM();
                muteBtn.image.color = Color.blue;
            }
        });
    }
}
