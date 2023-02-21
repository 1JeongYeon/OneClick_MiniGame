using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Image currentCharacterImage;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Image[] images_Gauge;

    private Player player;

    // ������ ü�¸� �����ϰ� ���� ������ ��� �߰��� ��.
    public const int HP = 0;

    public bool isTimerActivated = true;

    float time = 0f;
    public TMP_Text[] textTimes; // �ð��� ǥ���� text

    private void Start()
    {
        player = FindObjectOfType<Player>();
        SetCharacterStatusUI();
    }

    private void Update()
    {
        GaugeUpdate();
        TimeSetting();
        if (scoreText.text != GameManager.Instance.score.ToString())
        {
            scoreText.text = GameManager.Instance.score.ToString();
        }
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)StatusController.Instance.currentHp / StatusController.Instance.maxHp;
    }

    private void SetCharacterStatusUI()
    {
        currentCharacterImage.sprite = player.playerActionData.Portrait;
    }

    private void TimeSetting()
    {
        if (isTimerActivated)
        {
            time += Time.deltaTime;
            textTimes[0].text = ((int)time / 60 % 60).ToString();
            textTimes[1].text = ((int)time % 60).ToString();
            textTimes[2].text = string.Format("{0:.00}", (time % 1));
            textTimes[2].text = textTimes[2].text.Replace(".", "");
        }
    }

    public void TimerEvent()
    { 
        if (!isTimerActivated)
        {
            SetTimerOn();
        }
        else
        {
            SetTimerOff();
        }
    }
    public void SetTimerOn()
    { 
        isTimerActivated = true;
    }

    public void SetTimerOff()
    { 
        isTimerActivated = false;
    }
    

    public void PlayerHitEffect(GameObject hittedBullet)
    {
        float waitTime = 0f;
        if (Player.isHit == true)
        {
            waitTime += Time.deltaTime;
            if (hittedBullet.gameObject.GetComponent<DefaultBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.PortraitLose; // �¾Ƽ� �����ϴ� ���
            }
            else if (hittedBullet.gameObject.GetComponent<GoldCoinBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Annoying; // �̹��� ��ü�ؾ���
                coinText.text = GameManager.Instance.coin.ToString();
                scoreText.text = GameManager.Instance.score.ToString();
            }
            else if (hittedBullet.gameObject.GetComponent<HealBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Win; // �̹��� ��ü�ؾ���
                scoreText.text = GameManager.Instance.score.ToString();
            }

            if (waitTime >= 0.2f)
            {
                player.playerChracter.sprite = player.playerActionData.Stand;
                Player.isHit = false;
                waitTime = 0f;
            }
        }
    }
}
