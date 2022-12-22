using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSetting : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private void Awake()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            DontDestroyOnLoad(gameObjects[i]);
        }
    }
}
