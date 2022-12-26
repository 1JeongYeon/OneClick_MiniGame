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
    private Collider2D playerAttackCollider;

    private bool isHit = false;
    private bool isAttack = false;

    [SerializeField]
    private Button playerActionButton;

    private CharacterChoose characterData;
    public PlayerActionData playerActionData;

    // 이로운 총알, 해로운 총알, 아무 상관없는 총알(돈) 3개 들어올 것임
    private Bullet[] bullets;

    private void Start()
    {
        // bullet 종류에 따라 맞으면 피가 줄어들지 돈을 벌지 체력을 회복할지 결정해야하기 때문에 배열로 받아온다.
        bullets = GetComponentsInChildren<Bullet>();

        characterData = FindObjectOfType<CharacterChoose>();
        playerAttackCollider.enabled = false;

        characterIndex = characterData.characterIndex;
        chracterName = characterData.characterName;

        playerActionData = characterData.characters[characterIndex].GetComponent<PlayerActionData>();
        playerChracter.sprite = playerActionData.Stand;

        playerActionButton.onClick.AddListener(() =>
        {
            PlayerAttack();
        });
    }

    private void Update()
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
                Invoke("PlayerActionReturn", .2f);
            }
        }
        
    }

    private void PlayerActionReturn()
    {
        playerChracter.sprite = playerActionData.Stand;
        playerAttackCollider.enabled = false;
        isAttack = false;
    }

    public void PlayerAttack()
    {
        // 공격 모션 3개로 해서
        int randomNum = Random.Range(0, 3);
        
        if (playerChracter.sprite == playerActionData.Stand)
        {
            if (randomNum == 0)
            {
                playerChracter.sprite = playerActionData.Attack0;
                isAttack = true;
                playerAttackCollider.enabled = true;
            }
            else if (randomNum == 1)
            {
                playerChracter.sprite = playerActionData.Attack1;
                playerAttackCollider.enabled = true;
                isAttack = true;
            }
            else if (randomNum == 2)
            {
                playerChracter.sprite = playerActionData.Attack2;
                playerAttackCollider.enabled = true;
                isAttack = true;
            }
        }
    }

    // 아직 안쓰는 함수
    public void PlayerHit()
    {
        isHit = true;
        if (playerChracter.sprite == playerActionData.Stand)
        {
            playerChracter.sprite = playerActionData.Hit;
        }
    }
}
