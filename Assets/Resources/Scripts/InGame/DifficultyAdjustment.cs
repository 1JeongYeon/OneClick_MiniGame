using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjustment : MonoBehaviour
{
    private int tmp = 0;

    private float shakeTime = 10.0f;
    private float shakeSpeed = 4.0f;
    private float shakeAmount = 4.0f;

    private Transform cam;
    private void Start()
    {
        GameManager.Instance.stageLevel = 0;
        InvokeRepeating("LevelSetting", 0f, 10f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // 난이도 조절과 bgm을 관리한다.
    private void LevelSetting()
    {
        GameManager.Instance.stageLevel++;
        tmp++;
        if (tmp > 7)// stage 7 까지 ingame bgm 이다./*PlayMusicOperator.Instance.BGMList.Length*/)
        {
            tmp = 0;
        }
        PlayMusicOperator.Instance.PlayBGM($"stage{tmp}");

        if (GameManager.Instance.stageLevel >= 4)
        {
            GameManager.Instance.stageLevel = 4;
            StartCoroutine(ShakeCam());
        }
        Debug.Log("Level UP");
        Debug.Log(GameManager.Instance.stageLevel + " 현재 레벨");
    }

    // level 4 부터 캠이 흔들리게 난이도 조절
    IEnumerator ShakeCam()
    {
        Vector3 originPos = cam.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPos + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        cam.localPosition = originPos;
    }
}
