using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stage1 카메라 컨트롤러
// Player의 이동에 따라 움직이는 카메라

public class CameraController1 : MonoBehaviour
{
    GameObject player; // Player 오브젝트를 넣을 변수
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // "Player" 라는 이름의 오브젝트(플레이어 오브젝트)를 찾아 player에 넣어준다.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position; // player의 위치를 playerPos에 넣는다.

        // 플레이어 따라다님
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z); // 카메라의 위치를 playerPos(player의 위치)의 x, playerPos의 y, 카메라 본인의 z로 한다.

        // 카메라 이동 제한
        if (transform.position.y < 0) // 카메라의 y좌표가 0보다 작을 경우 
        {
            transform.position = new Vector3(playerPos.x, 0, transform.position.z); // 카메라의 y좌표를 0으로 하여 카메라의 y좌표가 0보다 아래로 내려가지 않도록 한다.
        }
        if(transform.position.y > 15) // 카메라의 y좌표가 15보다 클 경우
        {
            transform.position = new Vector3(playerPos.x, 15, transform.position.z); // 카메라의 y좌표를 15로 하여 카메라의 y좌표가 15보다 위로 올라가지 않도록 한다.
        }
        if(transform.position.x < -1) // 카메라의 x좌표가 -1보다 작을 경우
        {
            transform.position = new Vector3(-1, transform.position.y, transform.position.z); // 카메라의 x좌표를 -1로 하여 카메라의 x좌표가 -1보다 왼쪽으로 가지 않도록 한다.
        }
        if(transform.position.x > 81) // 카메라의 x좌표가 81보다 클 경우
        {
            transform.position = new Vector3(81, transform.position.y, transform.position.z); // 카메라의 x좌표를 81로 하여 카메라의 x좌표가 81보다 오른쪽으로 가지 않도록 한다.
        }
    }
}
