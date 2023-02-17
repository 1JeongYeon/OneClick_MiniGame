using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Image currentCharacterImage;


    [SerializeField]
    private Image[] images_Gauge;

    private Player player;

    // 아직은 체력만 구현하고 추후 게임적 요소 추가할 것.
    public const int HP = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        SetCharacterStatusUI();

    }

    private void Update()
    {
        GaugeUpdate();
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)StatusController.Instance.currentHp / StatusController.Instance.maxHp;
    }

    private void SetCharacterStatusUI()
    {
        currentCharacterImage.sprite = player.playerActionData.Portrait;
    }

    public void PlayerHitEffect(Collider2D hittedBullet)
    {
        float waitTime = 0f;
        if (player.isHit == true)
        {
            waitTime += Time.deltaTime;
            if (hittedBullet.gameObject.GetComponent<DefaultBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.PortraitLose; // 맞아서 아파하는 모습
            }
            else if (hittedBullet.gameObject.GetComponent<GoldCoinBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Annoying; // 이미지 수정해야함
            }
            else if (hittedBullet.gameObject.GetComponent<HealBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Win; // 이미지 수정해야함
            }

            if (waitTime >= 0.25f)
            {
                player.playerChracter.sprite = player.playerActionData.Stand;
                player.isHit = false;
                waitTime = 0f;
            }
        }
    }
}
