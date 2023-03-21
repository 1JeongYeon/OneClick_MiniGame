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
        muteBtn.image.color = Color.red;
        muteBtn.onClick.AddListener(() =>
        { 
            isMute = !isMute;

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
