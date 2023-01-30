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
        // Z�� ȸ�� ����
        bulletRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public abstract void InitSetting(TMP_Text info);

    public virtual void Shooting(Transform muzzle, TMP_Text soundText)
    {
        

        if (shootAble == true)
        {
            var bullet = Instantiate(bulletData.bullet);
            bullet.transform.position = muzzle.position;

            // �÷��̾��� ������ ���� (normalized : ���� ���� ���̰� 1�� ���� ����. ������ �ӵ�)
            Vector3 dir = (playerTrans.transform.position - bullet.transform.position).normalized;

            float vectorX = dir.x * bulletData.bulletSpeed;
            float vectorY = dir.y * bulletData.bulletSpeed;

            // �ش� �������� �ӵ� ����
            bulletRigidbody.velocity = new Vector2(vectorX, vectorY);
            // x���� �Ѿ�� ���� ��Ű��
            bullet.GetComponent<SpriteRenderer>().flipX = (vectorX < 0);

            var fireEffect = Instantiate(soundText);
            fireEffect.transform.position = muzzle.position + new Vector3(0, 3f, 0);
            fireEffect.text = bulletData.soundEffect;

            shootAble = false;

            // �Ѿ� �ִ�ġ�� �߿����� ��� �ؾ� ��. ���带 �ð����� ���� �ƴϸ� �� �Ѿ��� ������ �� �������� �� �������� ��� ����
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
