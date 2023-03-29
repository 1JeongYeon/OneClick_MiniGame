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
            {   // 0.1초동안 공격 모션
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
        // 공격 모션 3개로 랜덤으로 나오게 함
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
                // 디자인패턴으로 나누어진 bullet의 고유 기능 호출
                other.gameObject.GetComponent<Bullet>().Hit();

                uIManager.PlayerHitEffect(other.gameObject);

                if (other.gameObject.GetComponent<DefaultBullet>())
                {   // 기본총알에 맞으면 맞는 소리 재생
                    playerHitAudio.Play();
                }
            }
            Destroy(other.gameObject);
        }
    }
}
