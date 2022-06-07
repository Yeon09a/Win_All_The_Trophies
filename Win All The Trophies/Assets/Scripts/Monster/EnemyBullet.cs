using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 Slime과 Trunk의 총알(EnemyBullet)

public class EnemyBullet : MonoBehaviour
{
    // 강체 간의 충돌 검사
    private void OnTriggerEnter2D(Collider2D collision) // Trigger을 사용하여 강체 간의 겹침을 허용한다., 충돌한 순간
    {
        if (collision.name.Contains("player") || collision.name.Contains("Tilemap")) // Player(주인공) 또는 Tilemap(지형/맵)과 충돌하였을 때
        {
            Destroy(gameObject); // gameObject 즉, EnemyBullet을 제거한다.
        }
    }
}
