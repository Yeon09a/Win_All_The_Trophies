using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 못 벽(SpikeHead)

public class SpikeHead : MonoBehaviour
{
    float upSpeed = 4.0f; // SpikeHead가 위로 움직이는 속도
    float downSpeed = 20.0f; // SpikeHead가 아래로 움직이는 속도

    bool beUp = true; // SpikeHead의 위치를 구별하기 위한 변수로, SpikeHead가 위에 있는 경우 true, 아래에 있을 경우 false이다.

    // Update is called once per frame
    void Update()
    {
        if (beUp == true) // SpikeHead가 위에 있을 때(beUp이 true일 때)
        {
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime); // 아래(Vector2.down : (0, -1))로 downSpeed 만큼 움직인다. Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
        else // SpikeHead가 아래에 있을 때(beUp이 false일 때)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime); // 위(Vector2.up : (0, 1))로 upSpeed 만큼 움직인다. Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // boundary을 위해 Trigger을 사용한다.(boundary를 Trigger로 하지 않을 경우 Player의 움직임을 막는다.)
    {
        // SpikeHead의 이동방향에 따라 이미지 반전
        if (collision.name.Contains("boundary")) // SpikeHead가 boundary(몬스터의 이동을 제어해주기 위한 오브젝트)와 충돌하였을 때
        {
            if (beUp == true) // SpikeHead가 위에 존재할 때(beUp이 true일 때)
            {
                beUp = false; // beUp을 false로 바꾼다.
            }
            else // SpikeHead가 아래에 존재할 때(beUp이 false일 때)
            { 
                beUp = true; // beUp을 true로 바꾼다.
            }
        }
    }
}
