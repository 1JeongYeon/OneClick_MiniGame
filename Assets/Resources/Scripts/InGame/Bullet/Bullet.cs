using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public struct BulletData
{
    public float delayTime;
    public int maxBullet;
    public string information;
    public string soundEffect;
    public int damage;
    public float bulletSpeed;
    public GameObject bullet;
    
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private bool shootAble = false;
    private float delayTime = 0f;

    private GameObject playerTrans;
    Rigidbody2D bulletRigidbody;
    private void Start()
    {
        playerTrans = FindObjectOfType<Player>().gameObject;
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.gravityScale = 0f;
        // Z축 회전 무시
        bulletRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public abstract void InitSetting(TMP_Text info);

    public virtual void Shooting(Transform muzzle, TMP_Text soundText)
    {
        

        if (shootAble == true)
        {
            var bullet = Instantiate(bulletData.bullet);
            bullet.transform.position = muzzle.position;

            // 플레이어의 방향을 구함 (normalized : 방향 유지 길이가 1인 벡터 생성. 일정한 속도)
            Vector3 dir = (playerTrans.transform.position - bullet.transform.position).normalized;

            float vectorX = dir.x * bulletData.bulletSpeed;
            float vectorY = dir.y * bulletData.bulletSpeed;

            // 해당 방향으로 속도 세팅
            bulletRigidbody.velocity = new Vector2(vectorX, vectorY);
            // x축을 넘어가면 반전 시키기
            bullet.GetComponent<SpriteRenderer>().flipX = (vectorX < 0);

            var fireEffect = Instantiate(soundText);
            fireEffect.transform.position = muzzle.position + new Vector3(0, 3f, 0);
            fireEffect.text = bulletData.soundEffect;

            shootAble = false;

            // 총알 최대치가 중요한지 고민 해야 함. 라운드를 시간으로 할지 아니면 총 총알의 갯수를 다 소진했을 때 종료할지 고민 ㄱㄱ
            bulletData.maxBullet--;
        }

        if (shootAble == false)
        {
            delayTime += Time.deltaTime;
            if (delayTime >= bulletData.delayTime)
            {
                shootAble = true;
                delayTime = 0f;
            }
        }
    }
}
