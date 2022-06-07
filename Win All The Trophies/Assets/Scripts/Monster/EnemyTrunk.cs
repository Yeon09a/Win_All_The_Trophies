using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나무 몬스터(EnemyTrunk)

public class EnemyTrunk : MonoBehaviour
{
    Animator trunkAnimator; // Animator 컴포넌트가 들어올 변수
    public GameObject trunkbullet; // EnemyTrunkBullet이 들어갈 변수. 오브젝트를 넣을 수 있도록 public으로 선언한다.
    Rigidbody2D bulletRigid2D; // shoot()로 생긴 bullet의 Rigidbody2D 컴포넌트가 들어갈 변수

    float bulletSpeed = 400f; // bullet이 발사되는 속도

    int hp = 120; // EnemyTrunk의 체력

    public AudioClip attackedClip; // 공격받았을 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    AudioSource enemySrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        trunkAnimator = GetComponent<Animator>(); // EnemyTrunk의 Animator 컴포넌트를 얻어 trunkAnimator에 넣는다.
        enemySrc = GetComponent<AudioSource>(); // EnemyTrunk의 AudioSource 컴포넌트를 얻어 enemySrc에 넣는다.
    }

    // Update is called once per frame
    void Update()
    {
        // EnemyTrunk 죽음
        if (hp == 0) // EnemyTrunk의 체력이 0이 되었을 때(hp가 0일 때)
        {
            trunkAnimator.SetTrigger("DisappearTrigger"); // DisappearTrigger로 바꿔주어 TrunkDisasppear 애니메이션(EnemyTrunk가 사라지는 애니메이션)이 출력되도록 한다.
            Destroy(gameObject, 0.18f); // 0.18초 뒤에 gameObject 즉, EnemyTrunk를 제거한다.
        }
    }

    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger을 사용하여 강체 간의 겹침을 허용한다. (Player와 겹쳐져 막히지 않고 지나갈 수 있도록 Trigger 허용), 충돌한 순간
    {
        // 주인공의 공격(Bullet)을 맞았을 때
        if (collision.name.Contains("Bullet")) // EnemyTrunk가 Bullet(Player의 공격)과 충돌하였을 때
        {
            hp -= 10; // 체력이 10 깎인다. (hp에서 10을 뺀 후 다시 hp에 대입한다.)

            trunkAnimator.SetTrigger("AttackedTrigger"); // AttackedTrigger로 바꿔주어 TrunkBeAttacked 애니메이션(EnemyTrunk가 공격받는 애니메이션)이 출력되도록 한다.
            enemySrc.PlayOneShot(attackedClip, 0.2f); // 공격받았을 때의 사운드(attackedClip)를 0.2 볼륨으로 출력한다.
        }
    }

    // 총알(EnemyTrunkBullet이) 발사
    private void Shoot()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y - 0.17f, 1); // 총알이 생성될 위치
        GameObject bullet = Instantiate(trunkbullet, bulletPos, Quaternion.identity); // trunkbullet을 bulletPos 위치에 회전하지 않고 생성한다. 생성한 오브젝트는 bullet 오브젝트에 넣는다.
        bulletRigid2D = bullet.GetComponent<Rigidbody2D>(); // 위에서 만든 bulletRigid2D에 bullet의 Rigidbody2D 컴포넌트를 넣는다.
        bulletRigid2D.AddForce(transform.right * (-1) * bulletSpeed); // 총알(bullet)을 bulletSpeed 만큼 왼쪽으로 발사시킨다.
        Destroy(bullet, 0.8f); // 0.8초 뒤에 bullet을 제거한다.
    }


}
