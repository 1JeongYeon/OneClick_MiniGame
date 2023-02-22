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
            Destroy(collision.gameObject);
        }
    }
}
