using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 공격 컨트롤러(BulletController)

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet 프리팹을 넣을 변수로 public으로 설정한다.
    GameObject player; // Player 오브젝트를 넣을 변수
    Rigidbody2D bulletRigid2D; // Bullet 프리팹의 Rigidbody2D 컴포넌트가 들어갈 변수
    Rigidbody2D playerRigid2D; // Player의 Rigidbody2D 컴포넌트가 들어갈 변수
    float BulletForce; // Bullet이 발사되는 힘

    public AudioClip bulletClip; // 공격을 발사할 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.

    AudioSource bulletSrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // "Player" 라는 이름의 오브젝트(플레이어 오브젝트)를 찾아 player에 넣어준다.
        playerRigid2D = player.GetComponent<Rigidbody2D>(); // player의 Rigidbody2D 컴포넌트를 얻어 playerRigid2D에 넣는다.
        bulletSrc = GetComponent<AudioSource>(); // BulletController의 AudioSource 컴포넌트를 얻어 bulletSrc에 넣는다.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletPos = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z); // 공격(새로 생성될 bullet)이 생성될 위치
        
        if(Input.GetKeyDown(KeyCode.B)) // B키를 눌렀을 때
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity); // bulletPrefab을  bulletPos 위치에 회전하지 않고 생성한다. 생성한 오브젝트는 bullet 오브젝트에 넣는다.
            bulletRigid2D = bullet.GetComponent<Rigidbody2D>(); // 위에서 만든 bulletRigid2D에 bullet의 Rigidbody2D 컴포넌트를 넣는다.

            // 플레이어의 속도에 따른 공격 발사 힘
            if (playerRigid2D.velocity.x == 0) // Player가 제자리에서 Bullet을 발사하는 경우(Player의 속도가 0일 경우)
            {
                BulletForce = 600; // BulletForce을 600로 한다.(600을 대입한다.)
            }
            else // Player가 움직이면서 Bullet을 발사하는 경우(Player의 속도가 0이 아닐 경우)
            {
                BulletForce = 800; // BulletForce을 800로 한다.(800을 대입한다.)
            }
            
            // 플레이어의 방향에 따른 공격 발사 방향
            if(player.transform.localScale.x < 0) // 플레이어가 오른쪽을 보고있을 경우(player.transform.localScale.x가 0보다 작을 경우)
            {
                bulletRigid2D.AddForce(transform.right * (-1) * BulletForce); // 오른쪽으로 BulletForce 만큼 힘을 가해 공격을 발사한다.
            }
            else if(player.transform.localScale.x > 0) // 플레이어가 왼쪽을 보고있을 경우(player.transform.localScale.x가 0보다 클 경우)
            {
                bulletRigid2D.AddForce(transform.right * BulletForce); // 왼쪽으로 BulletForce 만큼 힘을 가해 공격을 발사한다.
            }

            bulletSrc.PlayOneShot(bulletClip, 0.5f); // 공격을 발사할 때의 사운드(bulletClip)를 0.5 볼륨으로 출력한다.

        }
    }
}
