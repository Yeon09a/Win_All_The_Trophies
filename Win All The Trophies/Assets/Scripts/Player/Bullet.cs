using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 공격(Bullet)
public class Bullet : MonoBehaviour
{
    GameObject player; // Player 오브젝트를 넣을 변수
    Rigidbody2D playerRigid2D; // Player의 Rigidbody2D 컴포넌트가 들어갈 변수

    float disTime; // Bullet이 몇초 후에 제거될 것인지에 대한 변수

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // "Player" 라는 이름의 오브젝트(플레이어 오브젝트)를 찾아 player에 넣어준다.
        playerRigid2D = player.GetComponent<Rigidbody2D>(); // player의 Rigidbody2D 컴포넌트를 얻어 playerRigid2D에 넣는다.
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 속도에 따른 공격이 사라지는 시간
        if (playerRigid2D.velocity.x == 0) // Player가 제자리에 있을 경우(Player의 속도가 0일 경우)
        {
            disTime = 0.4f; // disTime을 0.4로 한다.(0.4을 대입한다.)
        }
        else // Player가 움직이는 경우(Player의 속도가 0이 아닐 경우)
        {
            disTime = 0.6f; // disTime을 0.6로 한다.(0.6을 대입한다.)
        }
        Destroy(gameObject, disTime); // disTime초 뒤에 Bullet을 제거한다.
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger 허용, 충돌한 순간
    {
        if(collision.name.Contains("Enemy") || collision.name.Contains("Tilemap")) // Bullet이 Enemy 또는 Tilemapr과 충돌하였을 때
        {
            Destroy(gameObject); // Bullet을 제거한다.
        }
    }
    
}
