using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Ȯ�� �ý��� On
public static class ChanceMaker
{
    public static int GetRandom(int[] seed)
    {
        int randSum = seed.Sum(); // �ѷ�
        int rand = Random.Range(0, randSum + 1);
        int sum = 0;
        int returnIndex = 0;
        for (int i = 0; i < seed.Length; i++)
        {
            sum += seed[i];
            if (sum >= rand)
            {
                returnIndex = i;
                break;
            }
        }
        return returnIndex;
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public static GameManager Instance
    {
        get
        {
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
            return gameManager;
        }
    }
    public static bool isPause = false; // �Ͻ� ���� �޴� â Ȱ��ȭ
    
    public bool isAlive = true;
    public int score = 0;
    public int coin = 0;
    public int[] playTimes = { 0, 0, 0 }; // ��,��,�и���
    public int stageLevel = 0; 

    public List<int> highScore = new List<int>();

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
    }
}
