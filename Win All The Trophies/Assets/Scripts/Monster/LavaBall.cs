using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 용암 불(LavaBall)

public class LavaBall : MonoBehaviour
{
    float MSpeed = 8.0f; // LavaBall이 움직이는 속도
    bool beUp = false; // LavaBall의 위치를 구별하기 위한 변수로, LavaBall이 위에 있는 경우(LavaBall이 아래를 향해있는 경우) true, 아래에 있을 경우(LavaBall이 위를 향해있는 경우) false이다.

    // Update is called once per frame
    void Update()
    {
        if (beUp == false) // LavaBall이 아래에 있을 때(beUp이 false일 때)
        {
            transform.Translate(Vector2.up * MSpeed * Time.deltaTime); // 위(Vector2.up : (0, 1))로 MSpeed 만큼 움직인다. Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
        else // LavaBall이 위에 있을 때(beUp이 true일 때)
        {
            transform.Translate(Vector2.down * MSpeed * Time.deltaTime); // 아래(Vector2.down : (0, -1))로 MSpeed 만큼 움직인다. Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger을 사용하여 강체 간의 겹침을 허용한다.(Player와 겹쳐져 막히지 않고 지나갈 수 있도록 Trigger 허용)
    {
        // LavaBall의 이동방향에 따라 이미지 반전
        if (collision.name.Contains("boundary")) // LavaBall이 boundary(몬스터의 이동을 제어해주기 위한 오브젝트)와 충돌하였을 때
        {
            if (beUp == false) // beUp이 false일 때
            {
                beUp = true; // beUp을 true로 바꾼다.
                transform.localScale = new Vector3(1, -1, 1); // LavaBall의 이미지를 반전하여 아래를 향하도록 한다.
            }
            else // beUp이 true일 때
            {
                beUp = false; // beUp을 false로 바꾼다.
                transform.localScale = new Vector3(1, 1, 1); // LavaBall의 이미지를 반전하여 위를 향하도록 한다.
            }
        }
    }
}
