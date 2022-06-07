using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 버섯 몬스터(EnemyMushroom)

public class EnemyMushroom : MonoBehaviour
{
    Rigidbody2D mushroomRigid2D; // EnemyMushroom의 강체가 들어올 변수
    Animator mushroomAnimator; // Animator 컴포넌트가 들어올 변수

    float walkSpeed = 3.0f; // EnemyMushroom이 움직이는 속도

    bool lookLeft = true; // EnemyMushroom의 방향을 구별하기 위해 만든 변수로, 왼쪽을 바라보고 있을 경우 true이고, 오른쪽을 바라보고 있을 경우 false이다.
    float attackedForce = 50.0f; // EnemyMushroom이 공격을 받았을 때 받는 힘

    int hp = 90; // EnemyMushroom의 체력

    public AudioClip attackedClip; // 공격받았을 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    AudioSource enemySrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        mushroomRigid2D = GetComponent<Rigidbody2D>(); // EnemyMushroom의 Rigidbody2D 컴포넌트를 얻어 mushroomRigid2D에 넣는다.
        mushroomAnimator = GetComponent<Animator>(); // EnemyMushroom의 Animator 컴포넌트를 얻어 mushroomAnimator에 넣는다.
        enemySrc = GetComponent<AudioSource>(); // EnemyMushroom의 AudioSource 컴포넌트를 얻어 enemySrc에 넣는다.
    }

    // Update is called once per frame
    void Update()
    {
        // EnemyMushroom의 이동
        if (lookLeft == true) // EnemyMushroom이 왼쪽을 보고있을 때(lookLeft가 true일 때)ㅋ
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime); // 왼쪽으로 walkSpeed 만큼 움직인다.(Vector2.left은 (-1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }
        else // 오른쪽을 보고있을 때(lookLeft가 false일 때)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime); // 오른쪽으로 walkSpeed 만큼 움직인다.(Vector2.right은 (1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)
        }

        // EnemyMushroom 죽음
        if (hp == 0) // EnemyMushroom의 체력이 0이 되었을 때(hp가 0일 때)
        {
            mushroomAnimator.SetTrigger("DisappearTrigger"); // DisappearTrigger로 바꿔주어 MushroomDisasppear 애니메이션(EnemyMushroom이 사라지는 애니메이션)이 출력되도록 한다.
            Destroy(gameObject, 0.18f); // 0.18초 뒤에 gameObject 즉, EnemyMushroom을 제거한다.
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger을 사용하여 강체 간의 겹침을 허용한다. (Player와 겹쳐져 막히지 않고 지나갈 수 있도록 Trigger 허용), 충돌한 순간
    {
        // 주인공의 공격(Bullet)을 맞았을 때
        if (collision.name.Contains("Bullet")) // EnemyMushroom이 Bullet(Player의 공격)과 충돌하였을 때
        {
            hp -= 10; // 체력이 10 깎인다. (hp에서 10을 뺀 후 다시 hp에 대입한다.)
            float direction = transform.position.x - collision.transform.position.x; // Bullet(collision : 충돌한 오브젝트)과 EnemyMushroom의 x좌표의 차이(0보다 작을 경우 오른쪽에서 Bullet과 충돌한 것이고, 0보다 클 경우 왼쪽에서 Bullet과 충돌한 것이다.)

            mushroomAnimator.SetTrigger("AttackedTrigger"); // AttackedTrigger로 바꿔주어 MushroomBeAttacked 애니메이션(EnemyMushroom이 공격받는 애니메이션)이 출력되도록 한다.

            if (direction <= 0) // 오른쪽에서 공격을 받았을 때
            {
                mushroomRigid2D.AddForce(new Vector2((-1) * attackedForce, 0)); // 왼쪽으로 attackedForce 만큼 힘을 가한다. (왼쪽으로 밀리도록)
            }
            else // 왼쪽에서 공격을 받았을 때(direction < 0)
            {
                mushroomRigid2D.AddForce(new Vector2(attackedForce, 0)); // 오른쪽으로 attackedForce 만큼 힘을 가한다. (오른쪽으로 밀리도록)
            }

            enemySrc.PlayOneShot(attackedClip, 0.2f);// 공격받았을 때의 사운드(attackedClip)를 0.2 볼륨으로 출력한다.
        }

        // EnemyMushroom의 이동방향에 따라 이미지 반전
        if (collision.name.Contains("boundary")) // EnemyMushroom이 boundary(몬스터의 이동을 제어해주기 위한 오브젝트)와 충돌하였을 때
        {
            if (lookLeft == true) // EnemyMushroom이 왼쪽을 보고있을 때(lookLeft가 true일 때)
            {
                lookLeft = false; // lookLeft을 false로 바꾼다.
                transform.localScale = new Vector3(-4, 4, 1); // EnemyMushroom의 이미지를 반전하여 오른쪽을 바라보도록 한다.
            }
            else // EnemyMushroom이 오른쪽을 보고있을 때(lookLeft가 false일 때)
            {
                lookLeft = true; // lookLeft을 true로 바꾼다.
                transform.localScale = new Vector3(4, 4, 1); // EnemyMushroom의 이미지를 반전하여 왼쪽을 바라보도록 한다.
            }

        }
    }


}
