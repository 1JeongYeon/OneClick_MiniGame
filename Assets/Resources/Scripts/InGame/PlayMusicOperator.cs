using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOperator : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string bgmName;
        public AudioClip audio;
    }

    private static PlayMusicOperator playMusic;

    public static PlayMusicOperator Instance
    {
        get
        {
            if (playMusic == null) playMusic = FindObjectOfType<PlayMusicOperator>();
            return playMusic;
        }
    }
    // Inspector에 표시할 배경음악 목록
    public BgmType[] BGMList;

    private AudioSource BGM;
    private string NowBGMname = "";

    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        if (BGMList.Length > 0)
        {
            // 시작음악 설정
            PlayBGM(BGMList[9].bgmName);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(string name)
    {
        if (NowBGMname.Equals(name))
        {
            return;
        }

        for (int i = 0; i < BGMList.Length; ++i)
        {
            if (BGMList[i].bgmName.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
        }
    }
    public void StopBGM()
    {
        BGM.Stop();
    }

    public void PauseBGM()
    {
        BGM.Pause();
    }

    public void UnPauseBGM()
    {
        BGM.UnPause();
    }
}
