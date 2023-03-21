using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMoving : MonoBehaviour
{
    public int move_delay;	// ���� �̵������� ������ �ð�
    public int move_time;	// �̵� �ð�

    private float speed_x;	// x�� ���� �̵� �ӵ�
    private float speed_y;	// y�� ���� �̵� �ӵ�
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
                StartCoroutine(Wander());   // �ڷ�ƾ ����
            }
            if (isWalking)
            {
                Move();
            }
        }
    }

    void Move()
    {
        transform.Translate(speed_x, speed_y, speed_y);	// �̵�
    }

    IEnumerator Wander()
    {
        move_delay = 2;
        move_time = 3;

        // Translate�� �̵��� �� Object�� �ڷ���Ʈ �ϴ� ���� �����ϱ� ���� Time.deltaTime�� ������
        speed_x = Random.Range(-1.5f, 1.5f) * Time.deltaTime;
        speed_y = Random.Range(-1.5f, 1.5f) * Time.deltaTime;

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        // anim.SetBool("isWalk", true);	// �̵� �ִϸ��̼� ����

        yield return new WaitForSeconds(move_time);

        isWalking = false;
        // anim.SetBool("isWalk", false);	// �̵� �ִϸ��̼� ����

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

