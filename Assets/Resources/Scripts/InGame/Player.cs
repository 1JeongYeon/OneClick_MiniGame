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

    // player �׼�, �浹 ����
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

    // �̷ο� �Ѿ�, �طο� �Ѿ�, �ƹ� ������� �Ѿ�(��) 3�� ���� ����
    private Bullet[] bullets;

    private void Start()
    {
        // bullet ������ ���� ������ �ǰ� �پ���� ���� ���� ü���� ȸ������ �����ؾ��ϱ� ������ �迭�� �޾ƿ´�.
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

        // Invoke �Լ��� �������� �ƴҶ� isAttack�� true�϶��� �Լ� ����
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
        // ���� ��� 3���� �ؼ�
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

    // ���� �Ⱦ��� �Լ�
    public void PlayerHit()
    {
        isHit = true;
        if (playerChracter.sprite == playerActionData.Stand)
        {
            playerChracter.sprite = playerActionData.Hit;
        }
    }
}
