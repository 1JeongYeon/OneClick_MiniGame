using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    [SerializeField] GameObject attackEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.GetComponent<DefaultBullet>())
            {
                var attackEffect = Instantiate(attackEffectPrefab, collision.transform.position, Quaternion.identity);
                GameManager.Instance.score++;
                Destroy(attackEffect, .2f);
            }
            else if (collision.gameObject.GetComponent<GoldCoinBullet>() || collision.gameObject.GetComponent<HealBullet>())
            {
                StatusController.Instance.DecreaseHP(10);
                Debug.Log("패널티 적용 -10 HP");
            }
            Destroy(collision.gameObject);
        }
    }
}
