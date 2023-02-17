using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isPlaying;
    public int score = 0;
    public int coin = 0;
    public float playTime = 0f;
}
