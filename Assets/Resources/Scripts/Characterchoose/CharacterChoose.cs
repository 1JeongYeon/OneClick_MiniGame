using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoose : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject characterImage;
    public int characterIndex = 0;
    public string characterName = "";

    [SerializeField] private Button goButton;

    public void Left()
    {
        characterIndex = ((0 == characterIndex) ? characters.Length : characterIndex) - 1;
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        CharacterPose();
    }

    public void Right()
    {
        characterIndex = ((characters.Length - 1) == characterIndex) ?  0 : (characterIndex + 1);
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        CharacterPose();
    }

    public void CharacterPose()
    {
        characterImage.GetComponent<Image>().sprite = characters[characterIndex].GetComponent<PlayerActionData>().Annoying;
    }

    public void GO()
    {
        DontDestroyOnLoad(gameObject);
        SceneController.Instance.OpenScene("InGame");
    }

    void Start()
    {
        CharacterPose();
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
    }
}
