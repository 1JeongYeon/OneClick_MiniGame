using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Image currentCharacterImage;

    [SerializeField]
    public Image[] images_Gauge;


    // ������ ü�¸� �����ϰ� ���� ������ ��� �߰��� ��.
    public const int HP = 0;

    private void Update()
    {
        GaugeUpdate();
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)StatusController.Instance.currentHp / StatusController.Instance.maxHp;
    }

}
