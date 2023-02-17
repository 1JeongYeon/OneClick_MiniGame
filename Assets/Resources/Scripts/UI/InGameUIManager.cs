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

    // ������ ü�¸� �����ϰ� ���� ������ ��� �߰��� ��.
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
                currentCharacterImage.sprite = player.playerActionData.PortraitLose; // �¾Ƽ� �����ϴ� ���
            }
            else if (hittedBullet.gameObject.GetComponent<GoldCoinBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Annoying; // �̹��� �����ؾ���
            }
            else if (hittedBullet.gameObject.GetComponent<HealBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Win; // �̹��� �����ؾ���
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
