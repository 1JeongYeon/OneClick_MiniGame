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

    public static bool isHit = false;
    public static bool isAttack = false;

    [SerializeField]
    private Button playerActionButton;

    private InGameUIManager uIManager;
    private CharacterChoose characterData;
    public PlayerActionData playerActionData;

    public TMPro.TMP_Text info;

    // 이로운 총알, 해로운 총알, 아무 상관없는 총알(돈) 3개 들어올 것임
    // bullet 종류에 따라 맞으면 피가 줄어들지 돈을 벌지 체력을 회복할지 결정해야하기 때문에 배열로 받아온다.+
    public Bullet hittedBullet;

    private void Start()
    {
        characterData = FindObjectOfType<CharacterChoose>();
        uIManager = FindObjectOfType<InGameUIManager>();

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
        // 공격 모션 3개로 해서, 나중에 플레이어의 위치값에 따라 상 중 하 공격으로 나눌 것임 난이도 높아질수록 플레이어 위치도 위아래로 바꿀것이기 때문
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                hittedBullet = other.gameObject.GetComponent<Bullet>();
                hittedBullet.Hit();
                uIManager.PlayerHitEffect(other.gameObject);
                /*// 20이라고 쓰지말고 enter하는 other의 bulletdata.damage를 가져와야함... 가져오는데 계속 0이 떠서 임시로 함...

                StatusController.Instance.DecreaseHP(20);
                other.gameObject.GetComponent<Bullet>().Hit();
                Debug.Log(other.gameObject.GetComponent<DefaultBullet>().bulletData.damage); // 0이 나오는데 왜일까

                isHit = true;
                uIManager.PlayerHitEffect(other.gameObject);
            }
            else if (other.gameObject.GetComponent<GoldCoinBullet>())
            {
                // 데미지를 입히지 않음
                isHit = true;
                uIManager.PlayerHitEffect(other.gameObject);
                GameManager.Instance.coin += 1;
                GameManager.Instance.score += 3;
            }
            else if (other.gameObject.GetComponent<HealBullet>())
            {
                // BulletData에 -로 지정해놓았기에 -를 한번 더 써서 +처리 시킴
                //StatusController.Instance.IncreaseHP(-other.gameObject.GetComponent<HealBullet>().bulletData.damage);
                // 위와 동일한 상황  enter하는 other의 bulletdata.damage를 가져와야 함
                StatusController.Instance.IncreaseHP(30);
                isHit = true;
                GameManager.Instance.score += 5;*/
            }
            Destroy(other.gameObject);
        }
    }
}
