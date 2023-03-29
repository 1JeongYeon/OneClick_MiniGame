using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // 10������� ������ ��
    public GameObject[] rankFrames;
    public int highScoreCount = 0;
    private void Start()
    {
        // �ѷ��� �����ش�.
        GameManager.Instance.highScore.Capacity = rankFrames.Length;
        // +1�� ���ִ� ������ ��� ������ count�� ���� 1 ���� ����
        highScoreCount = PlayerPrefs.GetInt("SavedHighScoreCount") + 1;
        for (int i = 0; i < highScoreCount; i++)
        {   // ��ũ �ҷ�����
             GameManager.Instance.highScore.Add(PlayerPrefs.GetInt(i + "SavedHighScore"));
        }
        GameManager.Instance.highScore = GameManager.Instance.highScore.Distinct().ToList();
        ScoreFrameGenerate(GameManager.Instance.score);
    }
    // ����Ƽ���� InGame Scene�� ������ �� ��, Rank�� Ȱ��ȭ �ɶ����� �Լ� ȣ��
    public void ScoreFrameGenerate(int _currentScore)
    {
        _currentScore = PlayerPrefs.GetInt("CurrentScore");
        // scene �̵��� ����Ʈ ������ ���� ���� dondestroy �Ǵ� ���ӸŴ����� ����Ʈ�� �����Ѵ�.
        GameManager.Instance.highScore = GameManager.Instance.highScore.Distinct().ToList(); // �ߺ�����
        GameManager.Instance.highScore.Add(_currentScore);
        GameManager.Instance.highScore.Sort();
        GameManager.Instance.highScore.Reverse(); // ������������ ����
        if (GameManager.Instance.highScore.Count >= rankFrames.Length)
        {
            // rankFrames.Length �� �� 11���̴� 11��° rankFrame�� ������ �ӽ� ���������� gameObject.SetActive = false, �� ���� ������ 11��°�� ���� �����ǰ� ��.
            GameManager.Instance.highScore.RemoveAt(10);
        }
        // 11���� 10���� Ȱ��ȭ ��Ȱ��ȭ �Ѵ�.
        for (int i = 0; i < rankFrames.Length - 1; i++)
        {
            rankFrames[i].SetActive(i < GameManager.Instance.highScore.Count);
            rankFrames[i].GetComponentInChildren<TMPro.TMP_Text>().text = i < GameManager.Instance.highScore.Count ? GameManager.Instance.highScore[i].ToString() : "";
        }
        // ����Ʈ ������ ingame�� �� �� �̷����
        // ������ ���� �Ѡ��� �� ��ŷ�� �����־�� �ϹǷ� �����Ѵ�.
        for (int i = 0; i < GameManager.Instance.highScore.Count; i++)
        {
            PlayerPrefs.SetInt(i + "SavedHighScore", GameManager.Instance.highScore[i]);
        }
    }
    public void TimeOn()
    {
        Time.timeScale = 1f; // �ð� �帧
    }
    public void TimeOff()
    {
        Time.timeScale = 0f; // �ð��� �帧 ����. 0���. �� �ð��� ����.
    }
}
