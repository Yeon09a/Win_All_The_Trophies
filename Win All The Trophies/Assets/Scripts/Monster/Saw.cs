using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 톱날(Saw)

public class Saw : MonoBehaviour
{
    bool lookLeft = true; // Saw의 방향을 구별하기 위해 만든 변수로, 왼쪽을 향해있을 경우 true이고, 오른쪽을 향해있을 경우 false이다.

    float moveSpeed = 4.0f; // Saw이 움직이는 속도

    // Update is called once per frame
    void Update()
    {
        if (lookLeft == true)  // Saw가 왼쪽을 향해있을 때(lookLeft가 true일 때)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // 왼쪽으로 moveSpeed 만큼 움직인다.(Vector2.left은 (-1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
        else // 오른쪽을 향해있을 때(lookLeft가 false일 때)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // 오른쪽으로  moveSpeed 만큼 움직인다.(Vector2.right은 (1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // boundary을 위해 Trigger을 사용한다.(boundary를 Trigger로 하지 않을 경우 Player의 움직임을 막는다.)
    {
        // Saw의 이동방향에 따라 이미지 반전
        if (collision.name.Contains("boundary")) // Saw가 boundary(몬스터의 이동을 제어해주기 위한 오브젝트)와 충돌하였을 때
        {
            if (lookLeft == true) // Saw가 왼쪽을 향해있을 때(lookLeft가 true일 때)
            {
                lookLeft = false; // lookLeft을 false로 바꾼다.
                transform.localScale = new Vector3(-5, 5, 1); // Saw의 이미지를 반전하여 오른쪽을 향하도록 한다.
            }
            else // 오른쪽을 향해있을 때(lookLeft가 false일 때)
            {
                lookLeft = true; // lookLeft을 true로 바꾼다.
                transform.localScale = new Vector3(5, 5, 1); // Saw의 이미지를 반전하여 왼쪽을 바라보도록 한다.
            }
        }
    }
}
