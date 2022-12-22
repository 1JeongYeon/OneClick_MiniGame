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

    // player �׼�, �浹 ����
    [SerializeField]
    private Collider2D playerCollider;
    [SerializeField]
    private Collider2D playerAttackCollider;
    [SerializeField]
    private SpriteRenderer playerAction;

    [SerializeField]
    private Button playerActionButton;

    public PlayerActionData playerActionData;

    // �̷ο� �Ѿ�, �طο� �Ѿ�, �ƹ� ������� �Ѿ�(��) 3�� ���� ����
    private Bullet[] bullets;

    private void Start()
    {
        // bullet ������ ���� ������ �ǰ� �پ���� ���� ���� ü���� ȸ������ �����ؾ��ϱ� ������ �迭�� �޾ƿ´�.
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
        Debug.Log("Player Attack �Լ�");
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
