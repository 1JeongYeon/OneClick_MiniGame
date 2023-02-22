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

    public TMPro.TMP_Text info;

    // 이로운 총알, 해로운 총알, 아무 상관없는 총알(돈) 3개 들어올 것임
    // bullet 종류에 따라 맞으면 피가 줄어들지 돈을 벌지 체력을 회복할지 결정해야하기 때문에 배열로 받아온다.+
    private Bullet hittedBullet;
    private Bullet lastHittedBullet;
    private BulletController bulletController;

    private void Start()
    {
        bulletController = FindObjectOfType<BulletController>();
        characterData = FindObjectOfType<CharacterChoose>();
        uIManager = FindObjectOfType<InGameUIManager>();

        playerAttack.SetActive(false);

        characterIndex = characterData.characterIndex;
        chracterName = characterData.characterName;

        playerActionData = characterData.characters[characterIndex].GetComponent<PlayerActionData>();
        playerChracter.sprite = playerActionData.Stand;

        playerActionButton.onClick.AddListener(() =>
        {
            PlayerAttack();
        });
    }

    private void BulletSetting()
    {
        hittedBullet = bulletController.currentBullet;
        lastHittedBullet = bulletController.lastBullet;
        lastHittedBullet = hittedBullet;
        hittedBullet.InitSetting(info);
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

    private void OnTriggerEnter2D(Collider2D other) // 데미지가 다르게 들어옴 쏜 총알이랑 맞은 총알이랑 같아야하는데 다르게 가져오는것 같음
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                if (lastHittedBullet != hittedBullet)
                {
                    lastHittedBullet = hittedBullet;
                    hittedBullet.InitSetting(info);
                }
                BulletSetting();
                /*if (other.gameObject.GetComponent<Bullet>().bulletData.bullet != hittedBullet.bulletData.bullet)
                {
                    hittedBullet = other.GetComponent<Bullet>();
                }*/
                hittedBullet.Hit();
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
