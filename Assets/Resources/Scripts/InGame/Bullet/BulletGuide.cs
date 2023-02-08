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

        //포탄생성후 초반에 포탄이 벌어지듯이 연출하기위해
        //포탄의 회전을 캐릭터위치에서 포탄의 위치의 방향으로 놓습니다
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
        Debug.Log("유도");
        bulletTr = bulletData.bullet.transform;
        dis = Vector3.Distance(bulletTr.position, playerTargetTr.position);
        speed = bulletData.bulletSpeed;

        if (playerTargetTr == null) return;


        waitTime += Time.deltaTime;
        //1.5초 동안 천천히 forward 방향으로 전진합니다
        if (waitTime < 1.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(bulletTr.forward * speed, Space.World);
        }
        else
        {
            // 1.5초 이후 타겟방향으로 lerp위치이동 합니다

            speed += Time.deltaTime;
            float t = speed / dis;

            bulletTr.position = Vector3.LerpUnclamped(bulletTr.position, playerTargetTr.position, t);

        }


        // 매프레임마다 타겟방향으로 포탄이 방향을바꿉니다
        //타겟위치 - 포탄위치 = 포탄이 타겟한테서의 방향
        Vector3 directionVec = playerTargetTr.position - bulletTr.position;
        Quaternion qua = Quaternion.LookRotation(directionVec);
        bulletTr.rotation = Quaternion.Slerp(bulletTr.rotation, qua, Time.deltaTime * speed);



    }
}
