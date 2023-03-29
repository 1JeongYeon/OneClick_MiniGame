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
        // ĳ�� index�� ������ (���� ���� ��)
        characterIndex = ((0 == characterIndex) ? characters.Length : characterIndex) - 1;
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        tmpCharacterName.text = characterName;
        CharacterPose();
        // �ش� �ε����� ĳ���Ͱ� ���ŵǾ����� ��Ȱ��ȭ
        if (isCharacterPurchased[characterIndex] == true)
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
        if (isCharacterPurchased[characterIndex] == true)
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
        DontDestroyOnLoad(gameObject); // �ش� ĳ�� ������ ������ �ֱ� ����.
        SceneController.Instance.OpenScene("InGame");
    }
    
    void Start()
    {
        // ĳ�� ���������� ���� bool �迭
        isCharacterPurchased = new bool[characters.Length];
        // ���������� �ҷ��� �ش�.
        for (int i = 2; i < characters.Length; i++)
        {
            isCharacterPurchased[i] = System.Convert.ToBoolean(PlayerPrefs.GetInt(i + "Purchased"));
        }
        // 1,2 ��° ĳ���� ����� �� �ְ� ��
        isCharacterPurchased[0] = true;
        isCharacterPurchased[1] = true;
        tmpCoin.text = GameManager.Instance.coin.ToString();

        CharacterPose();
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;

        PlayMusicOperator.Instance.PlayBGM("character_choose");
        // ���Ź�ư�� �⺻������ ��Ȱ��ȭ
        characterPurchaseButton.gameObject.SetActive(false);
        characterPurchaseButton.onClick.AddListener(() =>
        {
            // ĳ������ ���ڸ�ŭ for��, 1,2��° ĳ���� ���� for��
            for (int i = 2; i < characters.Length; i++)
            {// bool �迭�� �ش� ĳ������ ���� false�϶���
                if (i == characterIndex && isCharacterPurchased[i] == false)
                {   // ���� ����� ��
                    if (GameManager.Instance.coin >= characterPrice)
                    {
                        GameManager.Instance.coin -= characterPrice;
                        isCharacterPurchased[i] = true;
                        tmpCoin.text = GameManager.Instance.coin.ToString();
                        // ���� ���� (bool��) ���� Sysetm.Convert.ToBoolean()�� �̿��Ѵ�. // 1�� ���� �Ǹ�, �ش� value�� ���� ����
                        PlayerPrefs.SetInt(i + "Purchased", System.Convert.ToInt16(isCharacterPurchased[i]));
                        characterPurchaseButton.gameObject.SetActive(false);
                    }// ���� ������ �� �� ���� UI�� Ȱ��ȭ�Ѵ�.
                    else if (GameManager.Instance.coin < characterPrice)
                    {
                        moreMoneyUI.gameObject.SetActive(true);
                    }
                }
            }
        });
    }
}
