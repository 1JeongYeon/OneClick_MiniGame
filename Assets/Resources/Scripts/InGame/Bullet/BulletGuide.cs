using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGuide : MonoBehaviour
{
    private float dis;
    private float speed;
    private float waitTime;
    public Transform playerTargetTr;
    private Transform bulletTr;

    public BulletData bulletData;
    private Bullet bullet;

    void Start()
    {

        //��ź������ �ʹݿ� ��ź�� ���������� �����ϱ�����
        //��ź�� ȸ���� ĳ������ġ���� ��ź�� ��ġ�� �������� �����ϴ�
        transform.rotation = Quaternion.LookRotation(transform.position - playerTargetTr.position);

    }


    void Update()
    {
        if (bulletTr != null)
        {
            MissleGuideOperation();
        }
    }

    void MissleGuideOperation()
    {
        Debug.Log("����");
        bulletTr = bulletData.bullet.transform;
        dis = Vector3.Distance(bulletTr.position, playerTargetTr.position);
        speed = bulletData.bulletSpeed;

        if (playerTargetTr == null) return;


        waitTime += Time.deltaTime;
        //1.5�� ���� õõ�� forward �������� �����մϴ�
        if (waitTime < 1.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(bulletTr.forward * speed, Space.World);
        }
        else
        {
            // 1.5�� ���� Ÿ�ٹ������� lerp��ġ�̵� �մϴ�

            speed += Time.deltaTime;
            float t = speed / dis;

            bulletTr.position = Vector3.LerpUnclamped(bulletTr.position, playerTargetTr.position, t);

        }


        // �������Ӹ��� Ÿ�ٹ������� ��ź�� �������ٲߴϴ�
        //Ÿ����ġ - ��ź��ġ = ��ź�� Ÿ�����׼��� ����
        Vector3 directionVec = playerTargetTr.position - bulletTr.position;
        Quaternion qua = Quaternion.LookRotation(directionVec);
        bulletTr.rotation = Quaternion.Slerp(bulletTr.rotation, qua, Time.deltaTime * speed);



    }
}
