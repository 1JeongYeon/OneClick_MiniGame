using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMoving : MonoBehaviour
{
    public int move_delay;	// 다음 이동까지의 딜레이 시간
    public int move_time;	// 이동 시간

    private float speed_x;	// x축 방향 이동 속도
    private float speed_y;	// y축 방향 이동 속도
    private bool isWandering;
    private bool isWalking;
    // Animator anim;

    void Start()
    {
        // anim = GetComponent<Animator>();

        isWandering = false;
        isWalking = false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.stageLevel >= 3)
        {
            if (!isWandering)
            {
                StartCoroutine(Wander());   // 코루틴 실행
            }
            if (isWalking)
            {
                Move();
            }
        }
    }

    void Move()
    {
        transform.Translate(speed_x, speed_y, speed_y);	// 이동
    }

    IEnumerator Wander()
    {
        move_delay = 2;
        move_time = 3;

        // Translate로 이동할 시 Object가 텔레포트 하는 것을 방지하기 위해 Time.deltaTime을 곱해줌
        speed_x = Random.Range(-1.5f, 1.5f) * Time.deltaTime;
        speed_y = Random.Range(-1.5f, 1.5f) * Time.deltaTime;

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        // anim.SetBool("isWalk", true);	// 이동 애니메이션 실행

        yield return new WaitForSeconds(move_time);

        isWalking = false;
        // anim.SetBool("isWalk", false);	// 이동 애니메이션 종료

        isWandering = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Wall : Bottom") || collision.gameObject.name.Contains("Wall : Top"))
            speed_y = -speed_y;
        else if (collision.gameObject.name.Contains("Wall : Left") || collision.gameObject.name.Contains("Wall : Right"))
            speed_x = -speed_x;
    }
}

