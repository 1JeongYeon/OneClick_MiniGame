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
    private int characterPrice = 50;

    public bool[] isCharacterPurchased;

    //[SerializeField] private Button goButton; ����Ƽ���� ����
    [SerializeField] private TMPro.TMP_Text tmpCharacterName;
    [SerializeField] private TMPro.TMP_Text tmpCoin;
    [SerializeField] private Button characterPurchaseButton;

    [SerializeField] private GameObject moreMoneyUI;

    public void Left()
    {
        characterIndex = ((0 == characterIndex) ? characters.Length : characterIndex) - 1;
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        tmpCharacterName.text = characterName;
        CharacterPose();
        if (characterIndex == 0 || characterIndex == 1)
        {
            characterPurchaseButton.gameObject.SetActive(false);
        }
        else
        {
            characterPurchaseButton.gameObject.SetActive(true);
        }
    }

    public void Right()
    {
        characterIndex = ((characters.Length - 1) == characterIndex) ?  0 : (characterIndex + 1);
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        tmpCharacterName.text = characterName;
        CharacterPose();
        if (characterIndex == 0 || characterIndex == 1)
        {
            characterPurchaseButton.gameObject.SetActive(false);
        }
        else
        {
            characterPurchaseButton.gameObject.SetActive(true);
        }
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
        isCharacterPurchased = new bool[characters.Length];
        isCharacterPurchased[0] = true;
        isCharacterPurchased[1] = true;
        tmpCoin.text = GameManager.Instance.coin.ToString();
        CharacterPose();
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        PlayMusicOperator.Instance.PlayBGM("character_choose");
        characterPurchaseButton.gameObject.SetActive(false);
        characterPurchaseButton.onClick.AddListener(() =>
        {
            // ������ �ִ� ���� �� Ŭ��
            if (GameManager.Instance.coin >= characterPrice)
            {// ĳ������ ���ڸ�ŭ for��
                for (int i = 0; i < characters.Length; i++)
                {// bool �迭�� �ش� ĳ������ ���� false�϶���
                    if (i == characterIndex && isCharacterPurchased[i] == false)
                    {
                        GameManager.Instance.coin -= characterPrice;
                        isCharacterPurchased[i] = true;
                        characterPurchaseButton.gameObject.SetActive(false);
                    }
                }
            }
        });
    }
}
