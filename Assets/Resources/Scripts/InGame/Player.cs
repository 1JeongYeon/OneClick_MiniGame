using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // player
    public SpriteRenderer playerChracter;
    public int characterIndex = 0;
    public string chracterName = "";

    // player 액션, 충돌 관련
    [SerializeField]
    private Collider2D playerCollider;
    [SerializeField]
    private GameObject playerAttack;
    [SerializeField]
    private AudioSource playerHitAudio;

    public static bool isHit = false;
    public static bool isAttack = false;

    [SerializeField]
    private Button playerActionButton;

    private InGameUIManager uIManager;
    private CharacterChoose characterData;
    public PlayerActionData playerActionData;


    private void Start()
    {
        characterData = FindObjectOfType<CharacterChoose>();
        uIManager = FindObjectOfType<InGameUIManager>();

        playerAttack.SetActive(false);

        characterIndex = characterData.characterIndex;
        chracterName = characterData.characterName;

        playerActionData = characterData.characters[characterIndex].GetComponent<PlayerActionData>();
        playerChracter.sprite = playerActionData.Stand;

        GameManager.Instance.isAlive = true;
        playerActionButton.onClick.AddListener(() =>
        {
            PlayerAttack();
        });
    }

    

    private void Update()
    {
        TryPlayerAttack();
        
    }

    private void TryPlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }

        // Invoke 함수가 실행중이 아닐때 isAttack이 true일때만 함수 실행
        if (IsInvoking() == false)
        {
            if (isAttack == true)
            {
                Invoke("PlayerActionReturn", .1f);
            }
        }
    }

    private void PlayerActionReturn()
    {
        playerChracter.sprite = playerActionData.Stand;
        playerAttack.SetActive(false);
        isAttack = false;
    }

    public void PlayerAttack()
    {
        // 공격 모션 3개로 해서, 나중에 플레이어의 위치값에 따라 상 중 하 공격으로 나눌 것임 난이도 높아질수록 플레이어 위치도 위아래로 바꿀것이기 때문
        int randomNum = Random.Range(0, 3);
        
        if (playerChracter.sprite == playerActionData.Stand)
        {
            if (randomNum == 0)
            {
                playerChracter.sprite = playerActionData.Attack0;
            }
            else if (randomNum == 1)
            {
                playerChracter.sprite = playerActionData.Attack1;
            }
            else if (randomNum == 2)
            {
                playerChracter.sprite = playerActionData.Attack2;
            }
            isAttack = true;
            playerAttack.SetActive(true);
        }
    }

    // 총알에 맞았을 때 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                other.gameObject.GetComponent<Bullet>().Hit();
                uIManager.PlayerHitEffect(other.gameObject);

                if (other.gameObject.GetComponent<DefaultBullet>())
                {
                    playerHitAudio.Play();
                }
            }
            Destroy(other.gameObject);
        }
    }
}
