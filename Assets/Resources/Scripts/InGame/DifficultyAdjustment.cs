using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjustment : MonoBehaviour
{
    [SerializeField] protected GameObject cannon;
    [SerializeField] protected BoxCollider2D range;
    protected float runningTime = 3f;

    private void Start()
    {
        GameManager.Instance.stageLevel = 0;
        InvokeRepeating("LevelSetting", 0f, 10f);
    }

    private void Update()
    {
        if (GameManager.Instance.stageLevel == 1)
        {
            StartCoroutine(RandomMoving(runningTime));
        }
    }

    private void LevelSetting()
    {
        GameManager.Instance.stageLevel++;

        if (GameManager.Instance.stageLevel >= 4)
        {
            GameManager.Instance.stageLevel = 4;
        }
        Debug.Log("Level UP");
        Debug.Log(GameManager.Instance.stageLevel);
    }

    private Vector3 ReturnRandomPos()
    {
        Vector3 originPosition = range.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = range.bounds.size.x;
        float range_Y = range.bounds.size.y;
/*
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);*/

        range_X = Random.Range(0, range_X);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 resultPosition = originPosition + RandomPostion;
        return resultPosition;
    }

    IEnumerator RandomMoving(float duration)
    {
        Debug.Log("4레벨 랜덤 무빙");
        /*
                cannon.transform.position = Vector3.Lerp(cannon.transform.position, ReturnRandomPos(), duration);
                yield return null;*/
        float runTime = 0f;
        Vector3 v = ReturnRandomPos();
        while (runTime < duration)
        {
            runTime += Time.deltaTime;
            if (runTime > duration)
            {
                cannon.transform.position = Vector3.Lerp(cannon.transform.position, v, runTime / duration);
                yield return null;
            }
        }
    }
}
