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
    [SerializeField] private Image losePose;

    private Player player;

    // 아직은 체력만 구현하고 추후 게임적 요소 추가할 것.
    public const int HP = 0;

    public bool isTimerActivated = true;

    float time = 0f;
    public TMP_Text[] textTimes; // 시간을 표시할 text

    private void Start()
    {
        player = FindObjectOfType<Player>();
        SetCharacterStatusUI();
       // PlayMusicOperator.Instance.PlayBGM("stage1");
        losePose.sprite = player.playerActionData.Lose;
        coinText.text = GameManager.Instance.coin.ToString();
    }

    private void FixedUpdate()
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
        // 인게임과 게임매니저에 현 플레이 타임 정보 전달
        if (isTimerActivated)
        {
            GameManager.Instance.isAlive = true;
            time += Time.deltaTime;
            textTimes[0].text = ((int)time / 60 % 60).ToString();
            textTimes[1].text = ((int)time % 60).ToString();
            textTimes[2].text = string.Format("{0:.00}", (time % 1));
            textTimes[2].text = textTimes[2].text.Replace(".", "");
        }
        for (int i = 0; i < textTimes.Length; i++)
        {
            GameManager.Instance.playTimes[i] = int.Parse(textTimes[i].text);
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
        // 현 캐릭 체력이 닳을 때 체력바 프레임 안 캐릭터의 모션 변화
        if (Player.isHit == true)
        {
            if (hittedBullet.gameObject.GetComponent<DefaultBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.PortraitLose; // 맞아서 아파하는 모습
            }
            else if (hittedBullet.gameObject.GetComponent<GoldCoinBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Annoying; // 이미지 교체해야함
                coinText.text = GameManager.Instance.coin.ToString();
                scoreText.text = GameManager.Instance.score.ToString();
            }
            else if (hittedBullet.gameObject.GetComponent<HealBullet>())
            {
                currentCharacterImage.sprite = player.playerActionData.Win; // 이미지 교체해야함
                scoreText.text = GameManager.Instance.score.ToString();
            }
            Invoke("PlayerHitEffectRefresh", .2f);
        }
    }

    private void PlayerHitEffectRefresh()
    {
        currentCharacterImage.sprite = player.playerActionData.Portrait;
        Player.isHit = false;
    }
}
