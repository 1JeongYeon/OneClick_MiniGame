using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // player
    public GameObject[] playerChracters;
    private int characterIndex = 0;
    public string chracterName = "";

    // player 액션, 충돌 관련
    [SerializeField]
    private Collider2D playerCollider;
    [SerializeField]
    private Collider2D playerAttackCollider;
    [SerializeField]
    private SpriteRenderer playerAction;

    [SerializeField]
    private Button playerActionButton;

    public PlayerActionData playerActionData;

    // 이로운 총알, 해로운 총알, 아무 상관없는 총알(돈) 3개 들어올 것임
    private Bullet[] bullets;

    private void Start()
    {
        // bullet 종류에 따라 맞으면 피가 줄어들지 돈을 벌지 체력을 회복할지 결정해야하기 때문에 배열로 받아온다.
        bullets = GetComponentsInChildren<Bullet>();
        playerAttackCollider.enabled = false;

        characterIndex = 3;
        playerActionData = playerChracters[characterIndex].GetComponent<PlayerActionData>();
        playerActionButton.onClick.AddListener(()=>
        {
            PlayerAttack(playerActionData);
        });
    }

    public void PlayerAttack(PlayerActionData _playerActionData)
    {
        Debug.Log("Player Attack 함수");
        playerActionData = _playerActionData;
        int randomNum = Random.Range(0, 2);
        float activeDelay = .3f;
        float time = 0f;
        if (playerAction.sprite.name == "Stand")
        {
            if (randomNum == 0)
            {
                playerAction.sprite = playerActionData.Attack0;
                playerAttackCollider.enabled = true;
                time += Time.deltaTime;
                if (time >= activeDelay)
                {
                    playerAction.sprite = playerActionData.Stand;
                    playerAttackCollider.enabled = false;
                }
            }
            else if (randomNum == 1)
            {
                playerAction.sprite = playerActionData.Attack1;
                playerAttackCollider.enabled = true;
                if (time >= activeDelay)
                {
                    playerAction.sprite = playerActionData.Stand;
                    playerAttackCollider.enabled = false;
                }
            }
            else
            {
                playerAction.sprite = playerActionData.Attack2;
                playerAttackCollider.enabled = true;
                if (time >= activeDelay)
                {
                    playerAction.sprite = playerActionData.Stand;
                    playerAttackCollider.enabled = false;
                }
            }
        }
    }
}
