using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 슬라임 몬스터(EnemySlime)

public class EnemySlime : MonoBehaviour
{
    Rigidbody2D slimeRigid2D; // EnemySlimet 강체가 들어올 변수
    Animator slimeAnimator; // Animator 컴포넌트가 들어올 변수
    public GameObject slimebullet; // EnemySlimeBullet이 들어갈 변수. 오브젝트를 넣을 수 있도록 public으로 선언한다.
    Rigidbody2D bulletRigid2D; // shoot()로 생긴 bullet의 Rigidbody2D 컴포넌트가 들어갈 변수

    bool lookLeft = true; // EnemySlime의 방향을 구별하기 위해 만든 변수로, 왼쪽을 바라보고 있을 경우 true이고, 오른쪽을 바라보고 있을 경우 false이다.
    float attackedForce = 30.0f; // EnemySlime이 공격을 받았을 때 받는 힘

    float walkSpeed = 2.0f; // EnemySlime이 움직이는 속도

    float bulletSpeed = 600f; // bullet이 발사되는 속도

    int hp = 130; // EnemySlimet의 체력

    public AudioClip attackedClip; // 공격받았을 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    AudioSource enemySrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        slimeRigid2D = GetComponent<Rigidbody2D>(); // EnemySlime의 Rigidbody2D 컴포넌트를 얻어 slimeRigid2D에 넣는다.
        slimeAnimator = GetComponent<Animator>(); // EnemySlime의 Animator 컴포넌트를 얻어 slimeAnimator에 넣는다.
        enemySrc = GetComponent<AudioSource>(); // EnemySlime의 AudioSource 컴포넌트를 얻어 enemySrc에 넣는다.
    }

    // Update is called once per frame
    void Update()
    {
        // EnemySlime의 이동
        if (lookLeft == true) // EnemySlime이 왼쪽을 보고있을 때(lookLeft가 true일 때)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime); // 왼쪽으로 walkSpeed 만큼 움직인다.(Vector2.left은 (-1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
        else // 오른쪽을 보고있을 때(lookLeft가 false일 때)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime); // 오른쪽으로 walkSpeed 만큼 움직인다.(Vector2.right은 (1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }

        // EnemySlime 죽음
        if (hp == 0) // EnemySlime의 체력이 0이 되었을 때(hp가 0일 때)
        {
            slimeAnimator.SetTrigger("DisappearTrigger"); // DisappearTrigger로 바꿔주어 SlimeDisasppear 애니메이션(EnemySlime이 사라지는 애니메이션)이 출력되도록 한다.
            Destroy(gameObject, 0.18f); // 0.18초 뒤에 gameObject 즉, EnemySlime을 제거한다.
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger을 사용하여 강체 간의 겹침을 허용한다. (Player와 겹쳐져 막히지 않고 지나갈 수 있도록 Trigger 허용), 충돌한 순간
    {
        // EnemySlime의 이동방향에 따라 이미지 반전
        if (collision.name.Contains("boundary")) // EnemySlime이 boundary(몬스터의 이동을 제어해주기 위한 오브젝트)와 충돌하였을 때
        {
            if (lookLeft == true) // EnemySlime이 왼쪽을 보고있을 때(lookLeft가 true일 때)
            {
                lookLeft = false; // lookLeft을 false로 바꾼다.
                transform.localScale = new Vector3(-5, 5, 1); // EnemySlime의 이미지를 반전하여 오른쪽을 바라보도록 한다.
            }
            else // EnemySlime이 오른쪽을 보고있을 때(lookLeft가 false일 때)
            {
                lookLeft = true; // lookLeft을 true로 바꾼다.
                transform.localScale = new Vector3(5, 5, 1); // EnemySlime의 이미지를 반전하여 왼쪽을 바라보도록 한다.
            }
        }

        // 주인공의 공격(Bullet)을 맞았을 때
        if (collision.name.Contains("Bullet")) // EnemySlime이 Bullet(Player의 공격)과 충돌하였을 때
        {
            hp -= 10; // 체력이 10 깎인다. (hp에서 10을 뺀 후 다시 hp에 대입한다.)
            float direction = transform.position.x - collision.transform.position.x; // Bullet(collision : 충돌한 오브젝트)과 EnemySlime의 x좌표의 차이(0보다 작을 경우 오른쪽에서 Bullet과 충돌한 것이고, 0보다 클 경우 왼쪽에서 Bullet과 충돌한 것이다.)

            slimeAnimator.SetTrigger("AttackedTrigger"); // AttackedTrigger로 바꿔주어 SlimeBeAttacked 애니메이션(EnemySlime이 공격받는 애니메이션)이 출력되도록 한다.
             
            if (direction <= 0) // 오른쪽에서 공격을 받았을 때
            {

                slimeRigid2D.AddForce(new Vector2((-1) * attackedForce, 0)); // 왼쪽으로 attackedForce 만큼 힘을 가한다. (왼쪽으로 밀리도록)
            }
            else // 왼쪽에서 공격을 받았을 때(direction < 0)
            {
                slimeRigid2D.AddForce(new Vector2(attackedForce, 0)); // 오른쪽으로 attackedForce 만큼 힘을 가한다. (오른쪽으로 밀리도록)
            }

            enemySrc.PlayOneShot(attackedClip, 0.2f); // 공격받았을 때의 사운드(attackedClip)를 0.2 볼륨으로 출력한다.
        }
    }

    // 총알(EnemySlimeBullet) 발사
    private void Shoot()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y - 0.17f, 1); // 총알이 생성될 위치
        GameObject bullet = Instantiate(slimebullet, bulletPos, Quaternion.identity); // slimebullet을  bulletPos 위치에 회전하지 않고 생성한다. 생성한 오브젝트는 bullet 오브젝트에 넣는다.
        bulletRigid2D = bullet.GetComponent<Rigidbody2D>(); // 위에서 만든 bulletRigid2D에 bullet의 Rigidbody2D 컴포넌트를 넣는다.

        if (lookLeft == true) // EnemySlime이 왼쪽을 보고있을 때(lookLeft가 true일 때)
        {
            bulletRigid2D.AddForce(transform.right * (-1) * bulletSpeed); // 총알(bullet)을 bulletSpeed 만큼 왼쪽으로 발사시킨다.
        }
        else // EnemySlime이 오른쪽을 보고있을 때(lookLeft가 false일 때)
        {
            bulletRigid2D.AddForce(transform.right * bulletSpeed); // 총알(bullet)을 bulletSpeed 만큼 오른쪽으로 발사시킨다.
        }
        
        Destroy(bullet, 1f); // 1초 뒤에 bullet을 제거한다.
    }


}
