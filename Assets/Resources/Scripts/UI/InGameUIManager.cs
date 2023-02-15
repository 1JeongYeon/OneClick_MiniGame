using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Image currentCharacterImage;


    [SerializeField]
    private Image[] images_Gauge;


    // 아직은 체력만 구현하고 추후 게임적 요소 추가할 것.
    public const int HP = 0;

    private void Start()
    {
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
        Player player = FindObjectOfType<Player>();
        currentCharacterImage.sprite = player.playerActionData.Portrait;
    }
}
