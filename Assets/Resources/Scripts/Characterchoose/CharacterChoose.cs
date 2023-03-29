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

    //[SerializeField] private Button goButton; 유니티에서 실행
    [SerializeField] private TMPro.TMP_Text tmpCharacterName;
    [SerializeField] private TMPro.TMP_Text tmpCoin;
    [SerializeField] private Button characterPurchaseButton;

    [SerializeField] private GameObject moreMoneyUI;

    public void Left()
    {
        // 캐릭 index를 정해줌 (왼쪽 기준 식)
        characterIndex = ((0 == characterIndex) ? characters.Length : characterIndex) - 1;
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;
        tmpCharacterName.text = characterName;
        CharacterPose();
        // 해당 인덱스의 캐릭터가 구매되었으면 비활성화
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
        DontDestroyOnLoad(gameObject); // 해당 캐릭 정보를 전달해 주기 위함.
        SceneController.Instance.OpenScene("InGame");
    }
    
    void Start()
    {
        // 캐릭 구매정보를 담은 bool 배열
        isCharacterPurchased = new bool[characters.Length];
        // 구매정보를 불러와 준다.
        for (int i = 2; i < characters.Length; i++)
        {
            isCharacterPurchased[i] = System.Convert.ToBoolean(PlayerPrefs.GetInt(i + "Purchased"));
        }
        // 1,2 번째 캐릭은 사용할 수 있게 함
        isCharacterPurchased[0] = true;
        isCharacterPurchased[1] = true;
        tmpCoin.text = GameManager.Instance.coin.ToString();

        CharacterPose();
        characterName = characters[characterIndex].GetComponent<PlayerActionData>().Name;

        PlayMusicOperator.Instance.PlayBGM("character_choose");
        // 구매버튼은 기본적으로 비활성화
        characterPurchaseButton.gameObject.SetActive(false);
        characterPurchaseButton.onClick.AddListener(() =>
        {
            // 캐릭터의 숫자만큼 for문, 1,2번째 캐릭은 빼고 for문
            for (int i = 2; i < characters.Length; i++)
            {// bool 배열안 해당 캐릭터의 값이 false일때만
                if (i == characterIndex && isCharacterPurchased[i] == false)
                {   // 돈이 충분할 때
                    if (GameManager.Instance.coin >= characterPrice)
                    {
                        GameManager.Instance.coin -= characterPrice;
                        isCharacterPurchased[i] = true;
                        tmpCoin.text = GameManager.Instance.coin.ToString();
                        // 구매 정보 (bool값) 저장 Sysetm.Convert.ToBoolean()을 이용한다. // 1이 들어가게 되며, 해당 value는 참이 들어옴
                        PlayerPrefs.SetInt(i + "Purchased", System.Convert.ToInt16(isCharacterPurchased[i]));
                        characterPurchaseButton.gameObject.SetActive(false);
                    }// 돈이 부족할 때 돈 부족 UI를 활성화한다.
                    else if (GameManager.Instance.coin < characterPrice)
                    {
                        moreMoneyUI.gameObject.SetActive(true);
                    }
                }
            }
        });
    }
}
