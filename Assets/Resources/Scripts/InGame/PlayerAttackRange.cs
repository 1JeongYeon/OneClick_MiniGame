using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    [SerializeField] GameObject attackEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            
            if (collision.gameObject.GetComponent<DefaultBullet>())
            {
                attackEffect.transform.position = collision.transform.position;
                attackEffect.SetActive(true);
                GameManager.Instance.score++;
            }
            Destroy(collision.gameObject);
        }
    }
}
